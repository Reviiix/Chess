using System;
using System.Collections.Generic;
using System.Linq;
using ChessBoard;
using UnityEngine;
using UnityEngine.UI;

namespace ChessPieces
{
    [Serializable]
    public class ChessPiece 
    {
        [HideInInspector]
        public bool whiteOrBlackTeam; //true == White //false == black
        
        public KeyValuePair<Directions, int>[] movesDirectionAndDistance;
        private BoardSegment currentBoardSegement;
        private BoardSegment previousPosition;
        [SerializeField]
        private RawImage image;

        public void Initialise(BoardSegment startingSegment)
        {
            SetMovesCapabilities();

            currentBoardSegement = startingSegment;
        }

        public void PieceAttacked()
        {
            image.enabled = false;
        }

        protected virtual void SetMovesCapabilities()
        {
            throw new NotImplementedException("This is the base class for chess pieces not an actual piece, override this method and set specific conditions for your piece");
        }
    

        public void AttemptToMove(BoardSegment newPosition)
        {
            if (!IsLegitimateMove(newPosition))
            {
                Debug.Log(this + " cant move to that position from here.");
                return;
            }
            if (newPosition.occupation.Key)
            {
                if (newPosition.occupation.Value.whiteOrBlackTeam != whiteOrBlackTeam)
                {
                    GameManager.TakePiece(newPosition);
                    MovePiece(newPosition);
                }
                Debug.Log("Space occupation.");
                return;
            }

            MovePiece(newPosition);
        }

        private void MovePiece(BoardSegment newPosition)
        {
            image.transform.position = newPosition.segment.transform.position;

            previousPosition = currentBoardSegement;
            currentBoardSegement = newPosition;

            currentBoardSegement.OccupyThisSegment(this);
            previousPosition.OccupyThisSegment(null);

            GameManager.ChangeActiveTeam();
        }
    

        private bool IsLegitimateMove(BoardSegment newPosition)
        {
            var potentialMoves = ReturnAllPotentialPositions(currentBoardSegement, movesDirectionAndDistance);

            return potentialMoves.Any(move => newPosition == move);
        }
    

        public void SetSelected(bool state)
        {
            if (state)
            {
                image.color = Color.blue;
                return;
            }

            image.color = Color.white;
        }
    
        protected virtual BoardSegment[] ReturnAllPotentialPositions(BoardSegment originalSegment, KeyValuePair<Directions, int>[] moveSet)
        {
            Debug.LogError("This is the base class for chess pieces not an actual piece, override this method and set specific conditions for your piece");
            return null;
        }

    }
}
