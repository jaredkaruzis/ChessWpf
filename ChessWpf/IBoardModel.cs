using ChessEngine;
using ChessWpf.ViewModels;
using System;

namespace ChessWpf;

public interface IBoardModel {

    public void StartNewGame(Color PlayerColor = Color.White);
    public void StartNewGame(Color PlayerColor, int AiLevel, string PlayerName);
    public bool SubmitMove(string move);
    public bool SubmitMove(Square origin, Square destination);
    public Color CurrentTurn();

    public EventHandler<RefreshBoardEventArgs> RefreshBoardEventHandler { get; set; }
    public EventHandler<GameOverEventArgs> GameOverEventHandler { get; set; }

}