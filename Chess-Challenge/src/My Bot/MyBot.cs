using ChessChallenge.API;
using System;

public class MyBot : IChessBot
{
    /* Version stats
     * 
     * Initial Commit (moves[0]): +0, =21, -111
     * First capture + promote: +1, =26, -72
     */
    public Move Think(Board board, Timer timer)
    {
        Move[] moves = board.GetLegalMoves();
        Move bestMove = moves[0];
        int bestMoveScore = int.MinValue;
        for (int i = 0; i < moves.Length; ++i)
        {
            int moveScore = 0 + Convert.ToInt32(moves[i].IsCapture) + Convert.ToInt32(moves[i].IsPromotion);
            if (moveScore > bestMoveScore)
            {
                bestMove = moves[i];
            }
        }

        return bestMove;
    }
}