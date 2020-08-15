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
            movesList = new KeyValuePair<Directions, int>[3];
            movesList[0] = new KeyValuePair<Directions, int>(Directions.North, 1);
            movesList[1] = new KeyValuePair<Directions, int>(Directions.NorthEast, 1);
            movesList[2] = new KeyValuePair<Directions, int>(Directions.NorthWest, 1);
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
                        if (originalSegment.row >= ChessBoard.Board.Rows-1)
                        {
                            break;
                        }
                        returnVariable.Add(GameManager.instance.board.ReturnNorthSegment(originalSegment));
                        break;
                    case Directions.NorthEast:
                        if (originalSegment.column <= 0)
                        {
                            break;
                        }
                        returnVariable.Add(GameManager.instance.board.ReturnNorthEastSegment(originalSegment));
                        break;
                    case Directions.NorthWest:
                        if (originalSegment.column >= ChessBoard.Board.Columns-1)
                        {
                            break;
                        }
                        returnVariable.Add(GameManager.instance.board.ReturnNorthWestSegment(originalSegment));
                        break;
                }
            }
            return returnVariable.ToArray();
        }
    }
}
