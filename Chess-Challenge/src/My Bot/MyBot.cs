using ChessChallenge.API;
using System;
using System.Collections.Generic;

public class MyBot : IChessBot
{
    /* Version stats
     * 
     * Initial Commit (moves[0]): +0, =21, -111
     * First capture + promote: +1, =26, -72
     * Weighted composite score (rank, promotion, and capture): +4, =112, -18
     */

    // Piece values. None 0, Pawn 1, Knight 3, Bishop 3, Rook 5, Queen 9, King 100
    int[] pieceValues = { 0, 1, 3, 3, 5, 9, 100 };
    public Move Think(Board board, Timer timer)
    {
        Move[] moves = board.GetLegalMoves();

        Move bestMove = moves[0];
        int bestMoveScore = int.MinValue;
        

        for (int i = 0; i < moves.Length; ++i)
        {
            Move move = moves[i];

            // range: 1 - 9
            int movePieceValue = pieceValues[(int)move.MovePieceType];

            // range: 0 - 2
            int pawnRankGain = Convert.ToInt32(move.MovePieceType == PieceType.Pawn) * Math.Abs(move.TargetSquare.Rank - move.StartSquare.Rank);
            // range: 0 - 8
            int promotionGain = pieceValues[(int)move.PromotionPieceType] - 1;
            // range: 0 - 9
            int captureGain = pieceValues[(int)move.CapturePieceType];
            // range: 0 - 1 (1 if the target square is protected by the enemy's pieces)
            int targetIsThreatened = Convert.ToInt32(board.SquareIsAttackedByOpponent(move.TargetSquare));


            int moveScore
                = 5 * pawnRankGain // 0 - 10
                + 45 * promotionGain // 0 - 360        <-- Change coefficient to 40, I think
                + 40 * captureGain // 0 - 360
                - 40 * movePieceValue * targetIsThreatened; // 0 - 360
            if (moveScore >= bestMoveScore)
            {
                bestMoveScore = moveScore;
                bestMove = moves[i];
            }
        }

        return bestMove;
    }

    //private Dictionary<Square, List<Move>> GenerateMovesToSqaures(Move[] moves)
    //{
    //    // Each square that can be moved to is paired with a list of all the moves that target that square
    //    Dictionary<Square, List<Move>> movesToSquares = new Dictionary<Square, List<Move>>();
    //    foreach (Move move in moves)
    //    {
    //        // If this target square is not yet in the Dictionary, add it
    //        if (!movesToSquares.ContainsKey(move.TargetSquare))
    //        {
    //            movesToSquares.Add(move.TargetSquare, new List<Move>());
    //        }
    //        // Add this move to it's target sqaure's list
    //        movesToSquares[move.TargetSquare].Add(move);
    //    }
    //    PrintMovesToSquares(movesToSquares);
    //}


    //private void PrintMovesToSquares(Dictionary<Square, List<Move>> movesToSquares)
    //{
    //    foreach (Square square in movesToSquares.Keys)
    //    {
    //        List<Move> moves = movesToSquares[square];
    //        Console.WriteLine("Target: {0}  |   Moves #: {1}", square.Name, moves.Count);
    //        foreach (Move move in moves)
    //        {
    //            Console.WriteLine("{0} -> {1}", move.StartSquare.Name, square.Name);
    //        }
    //    }
    //}
}