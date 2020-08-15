using System;
using System.Collections.Generic;
using ChessBoard;

namespace ChessPieces
{
    [Serializable]
    public class Pawn : ChessPiece
    {
        protected override void SetMovesCapabilities()
        {
            movesDirectionAndDistance = new KeyValuePair<Directions, int>[3];
            movesDirectionAndDistance[0] = new KeyValuePair<Directions, int>(Directions.North, 1);
            movesDirectionAndDistance[1] = new KeyValuePair<Directions, int>(Directions.NorthEast, 1);
            movesDirectionAndDistance[2] = new KeyValuePair<Directions, int>(Directions.NorthWest, 1);
        }
        
        
        protected override BoardSegment[] ReturnAllPotentialPositions(BoardSegment originalSegment, KeyValuePair<Directions, int>[] moveSet)
        {
            var returnVariable = new List<BoardSegment>();
            
            foreach (var move in moveSet)
            {
                // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
                switch(move.Key) //Maybe refactor this check into one that's performed in the base class??
                {
                    case Directions.North:
                        if (originalSegment.row >= ChessBoard.ChessBoard.Rows-1)
                        {
                            break;
                        }
                        returnVariable.Add(GameManager.Instance.chessBoard.ReturnNorthSegment(originalSegment));
                        break;
                    case Directions.NorthEast:
                        if (originalSegment.column <= 0)
                        {
                            break;
                        }
                        returnVariable.Add(GameManager.Instance.chessBoard.ReturnNorthEastSegment(originalSegment));
                        break;
                    case Directions.NorthWest:
                        if (originalSegment.column >= ChessBoard.ChessBoard.Columns-1)
                        {
                            break;
                        }
                        returnVariable.Add(GameManager.Instance.chessBoard.ReturnNorthWestSegment(originalSegment));
                        break;
                }
            }
            return returnVariable.ToArray();
        }
    }
}
