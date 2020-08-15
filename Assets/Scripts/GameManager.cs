using System;
using ChessBoard;
using ChessPieces;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public ChessBoard.ChessBoard chessBoard;
    public ChessPieceManager chessPieceManager;
    
    public static bool activeTeam = true; //true == White //false == black

    public delegate void OnActiveTeamChangedHandler(bool activeTeam);
    public static event OnActiveTeamChangedHandler OnActiveTeamChanged;

    private void Awake()
    {
        Instance = this;
        chessBoard.Initialise();
        chessPieceManager.Initialise();
    }
    
    private void Update()
    {
        chessBoard.ChessBoardUpdate();
    }

    private void OnEnable()
    {
        chessBoard.Enabled();
    }
    
    private void OnDisable()
    {
        chessBoard.Disabled();
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


