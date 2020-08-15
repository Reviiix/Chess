using System;
using UnityEngine;

namespace ChessPieces
{
    [Serializable]
    public class ChessPieceManager
    {
        [Header("WhitePieces")]
        public Pawn[] pawnsWhite;
        
        [Header("BlackPieces")]
        public Pawn[] pawnsBlack;

        public void Initialise()
        {
            SetWhiteTeam();
            SetBlackTeam();
        }

        private void SetWhiteTeam()
        {
            foreach (var pawn in pawnsWhite)
            {
                pawn.team = true;
            }
        }
        
        private void SetBlackTeam()
        {
            foreach (var pawn in pawnsBlack)
            {
                pawn.team = false;
            }
        }
    }
    
    public enum Directions
    {
        North,
        NorthEast,
        East,
        Southeast,
        South,
        Southwest,
        West,
        NorthWest
    }
}
