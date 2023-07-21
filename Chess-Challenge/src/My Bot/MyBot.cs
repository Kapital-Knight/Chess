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
    public Move Think(Board board, Timer timer)
    {
        Move[] moves = board.GetLegalMoves();
        Move bestMove = moves[0];
        int bestMoveScore = int.MinValue;
        for (int i = 0; i < moves.Length; ++i)
        {
            Move move = moves[i];
            int pawnRankGain = (move.MovePieceType == PieceType.Pawn)?((board.IsWhiteToMove)?1:-1) * (move.TargetSquare.Rank - move.StartSquare.Rank):0;
            int promotionGain = ((int)move.PromotionPieceType - 1);
            int captureGain = (int)move.CapturePieceType;
            // Type: None = 0, Pawn = 1, Knight = 2, Bishop = 3, Rook = 4, Queen = 5, King = 6
            int moveScore = 0
                + 1 * pawnRankGain 
                + 1 * captureGain 
                + 2 * promotionGain;
            if (moveScore > bestMoveScore)
            {
                bestMoveScore = moveScore;
                bestMove = moves[i];
            }
        }

        return bestMove;
    }
}