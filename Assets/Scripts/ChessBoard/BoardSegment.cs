using System;
using System.Collections.Generic;
using ChessPieces;
using UnityEngine;
using UnityEngine.UI;

namespace ChessBoard
{
    [Serializable]
    public class BoardSegment 
    {
        [HideInInspector]
        public KeyValuePair<bool, ChessPiece> occupation;
        private bool selected;
        [HideInInspector]
        public int segmentIndex;
        [HideInInspector]
        public int column;
        [HideInInspector]
        public int row;
        [SerializeField]
        public Image segment;

        public void Initialise()
        {
            segment.GetComponent<Button>().onClick.AddListener(ButtonPressed);
            SetIndices();
        }
        
        public void ReInitialisePostBoardFlip()
        {
            SetIndices();
        }

        private void SetIndices()
        {
            SetColumnAndRowAssumingNormalChessBoard();
            ChessBoard.amountOfSegments++;
        }

        private void SetColumnAndRowAssumingNormalChessBoard()
        {
            var amountOfSegments = ChessBoard.amountOfSegments;
            segmentIndex = amountOfSegments;
            
            if (amountOfSegments > 0 && amountOfSegments <= 7)
            {
                row = 0;
            }
            if (amountOfSegments > 7 && amountOfSegments <= 15)
            {
                row = 1;
            }
            if (amountOfSegments > 15 && amountOfSegments <= 23)
            {
                row = 2;
            }
            if (amountOfSegments > 23 && amountOfSegments <= 31)
            {
                row = 3;
            }
            if (amountOfSegments > 31 && amountOfSegments <= 39)
            {
                row = 4;
            }
            if (amountOfSegments > 39 && amountOfSegments <= 47)
            {
                row = 5;
            }
            if (amountOfSegments > 47 && amountOfSegments <= 55)
            {
                row = 6;
            }
            if (amountOfSegments > 56 && amountOfSegments <= 64)
            {
                row = 7;
            }
            column = amountOfSegments - (ChessBoard.Columns*row);
        }

        private void ButtonPressed()
        {
            if (occupation.Key)
            {
                if (GameManager.activeTeam != occupation.Value.whiteOrBlackTeam)
                {
                    SetSelection(true);
                    ChessBoard.ChessBoardSegmentButtonPressed(this);
                    return;
                }
                
                ChessBoard.ChessPieceSelected(occupation.Value);
                occupation.Value.SetSelected(true);
                return;
            }
            SetSelection(true);
            ChessBoard.ChessBoardSegmentButtonPressed(this);
        }

        public void SetSelection(bool state)
        {
            selected = state;
            SetSelectionGraphics(selected);
        }
        
        private void SetSelectionGraphics(bool state)
        {
            if (state)
            {
                segment.color = Color.red;
                return;
            }
            segment.color = Color.white;
        }

        public void OccupyThisSegment(ChessPiece piece)
        {
            var state = piece != null;
            occupation = new KeyValuePair<bool, ChessPiece>(state, piece);
        }
    }
}
