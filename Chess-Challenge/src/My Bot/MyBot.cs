using ChessChallenge.API;
using System;

public class MyBot : IChessBot
{
    /* Version stats
     * 
     * Initial Commit (moves[0]): +0, =21, -111
     * First capture + promote: +1, =26, -72
     * Weighted composite score (rank, promotion, and capture): +4, =112, -18
     */

    // Piece values. None 0, Pawn 1, Knight 3, Bishop 3, Rook 5, Queen 9, King 100
    int[] pieceValue = { 0, 1, 3, 3, 5, 9, 100 }; 
    public Move Think(Board board, Timer timer)
    {
        Move[] moves = board.GetLegalMoves();
        Move bestMove = moves[0];
        int bestMoveScore = int.MinValue;
        for (int i = 0; i < moves.Length; ++i)
        {
            Move move = moves[i];
            // range: 0 - 2
            int pawnRankGain = (move.MovePieceType == PieceType.Pawn)?((board.IsWhiteToMove)?1:-1) * (move.TargetSquare.Rank - move.StartSquare.Rank):0;
            // range: 0 - 4
            int promotionGain = pieceValue[(int)move.PromotionPieceType] - 1;
            // range: 0 - 6
            int captureGain = pieceValue[(int)move.CapturePieceType];
            // Next, punish moving into a pawn's range and reward moving out of it
            
            int moveScore
                = 10 * pawnRankGain
                + 20 * promotionGain
                + 10 * captureGain;
            if (moveScore > bestMoveScore)
            {
                bestMoveScore = moveScore;
                bestMove = moves[i];
            }
        }

        return bestMove;
    }
}