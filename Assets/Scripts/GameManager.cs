using System;
using ChessBoard;
using ChessPieces;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Board board;
    public ChessPieceManager chessPieceManager;
    public Transform selectionIcon;
    
    public static bool activeTeam = true; //true == White //false == black

    public delegate void OnActiveTeamChangedHandler(bool activeTeam);
    public static event OnActiveTeamChangedHandler OnActiveTeamChanged;

    private void Awake()
    {
        instance = this;
        board.Initialise();
        chessPieceManager.Initialise();
    }
    
    private void Update()
    {
        board.ChessBoardUpdate();
    }

    private void OnEnable()
    {
        board.Enabled();
    }
    
    private void OnDisable()
    {
        board.Disabled();
    }

    public static void ChangeActiveTeam()
    {
        activeTeam = !activeTeam;
        OnActiveTeamChanged?.Invoke(activeTeam);
    }

    public static void TakePiece(BoardSegment vitimPosition, Action callBack = null)
    {
        vitimPosition.occupation.Value.PieceAttacked();
        callBack?.Invoke();
    }
}


