using System;
using System.Linq;
using ChessPieces;
using UnityEngine;

namespace ChessBoard
{
    [Serializable]
    public class ChessBoard
    {
        private ChessPieceManager chessPieceManager;
        public BoardSegment[] segments;
        public static BoardSegment currentSegmentSelection;
        public static BoardSegment previousSegmentSelection;

        public static ChessPiece chessPieceSelection;
        public static int amountOfSegments = 0;
        public const int Rows = 8;
        public const int Columns = 8;
        

        public void Initialise()
        {
            chessPieceManager = GameManager.Instance.chessPieceManager;
                
            InitialiseSegments();
            
            previousSegmentSelection = GameManager.Instance.chessBoard.segments[12]; //DELETE???
            currentSegmentSelection = GameManager.Instance.chessBoard.segments[12];
            
            SetPiecesInitialPositions();
            chessPieceSelection = chessPieceManager.pawnsWhite[0];

            InitialiseChessPieceMoves();
        }

        public void Enabled()
        {
            GameManager.OnActiveTeamChanged += ResetPieceSelected;
            GameManager.OnActiveTeamChanged += FlipSegmentList;
        }

        public void Disabled()
        {
            GameManager.OnActiveTeamChanged -= ResetPieceSelected;
            GameManager.OnActiveTeamChanged -= FlipSegmentList;
        }
        
        private void InitialiseChessPieceMoves()
        {
            for (var i = 0; i < chessPieceManager.pawnsBlack.Length; i++)
            {
                chessPieceManager.pawnsWhite[i].Initialise(segments[i + 8]);
                chessPieceManager.pawnsBlack[i].Initialise(segments[i + 48]);
            }
        }

        private void SetPiecesInitialPositions()
        {
            foreach (var segment in segments)
            {
                if (segment.row == 1)
                {
                    segment.OccupyThisSegment(chessPieceManager.pawnsWhite[segment.column]);
                }
                if (segment.row == 6)
                {
                    segment.OccupyThisSegment(chessPieceManager.pawnsBlack[segment.column]);
                }
            }
        }
        
        public void InitialiseSegments()
        {
            foreach (var v in segments)
            {
                v.Initialise();
            }
        }
        
        public static void ChessBoardSegmentButtonPressed(BoardSegment newSelection)
        {
            previousSegmentSelection = currentSegmentSelection;
            previousSegmentSelection.SetSelection(false);

            currentSegmentSelection = newSelection;
        }
        
        private static void ResetPieceSelected(bool activeTeam)
        {
            ChessPieceSelected(null);
        }
        
        //Flip the board around rather than working out separate move conditions for white and black pieces.
        private void FlipSegmentList(bool activeTeam)
        {
            //Holy fucking shit sticks this actually worked... and worked well...
            var boardSegments = segments.Reverse().ToArray();
            segments = boardSegments;
            
            amountOfSegments = 0;
            
            foreach (var v in segments)
            {
                v.ReInitialisePostBoardFlip();
            }
        }

        public static void ChessPieceSelected(ChessPiece newPiece)
        {
            if (chessPieceSelection != null)
            {
                chessPieceSelection.SetSelected(false);
            }
            chessPieceSelection = newPiece;
        }

        public void ChessBoardUpdate()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (chessPieceSelection == null)
                {
                    Debug.Log("Select Something");
                    return;
                }
                chessPieceSelection.AttemptToMove(currentSegmentSelection);
            }
        }
        
        public BoardSegment ReturnNorthSegment(BoardSegment originalSegment)
        {
            return segments[originalSegment.segmentIndex + Columns];
        }

        public BoardSegment ReturnEastSegment(BoardSegment originalSegment)
        {
            return segments[originalSegment.segmentIndex + 1];
        }

        public BoardSegment ReturnSouthSegment(BoardSegment originalSegment)
        {
            return segments[originalSegment.segmentIndex - Columns];
        }

        public BoardSegment ReturnWestSegment(BoardSegment originalSegment)
        {
            return segments[originalSegment.segmentIndex - 1];
        }

        public BoardSegment ReturnNorthEastSegment(BoardSegment originalSegment)
        {
            return segments[originalSegment.segmentIndex + Columns - 1];
        }

        public BoardSegment ReturnNorthWestSegment(BoardSegment originalSegment)
        {
            return segments[originalSegment.segmentIndex + Columns + 1];
        }

        public BoardSegment ReturnSouthEastSegment(BoardSegment originalSegment)
        {
            return segments[originalSegment.segmentIndex - Columns + 1];
        }

        public BoardSegment ReturnSouthWestSegment(BoardSegment originalSegment)
        {
            return segments[originalSegment.segmentIndex - Columns - 1];
        }
    }
}

