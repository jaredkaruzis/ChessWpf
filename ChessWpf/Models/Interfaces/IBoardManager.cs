using ChessEngine;
using System;

namespace ChessWpf;

public interface IBoardManager {
    public Board CurrentGame { get; set; }
    public void StartNewGame(Color PlayerColor = Color.White);
    public void StartNewGame(Color PlayerColor, int AiLevel);
    public bool SubmitMove(string move);
    public bool SubmitMove(Square origin, Square destination, PieceType promotionType = PieceType.Empty);
    public Color CurrentTurn();

    public EventHandler<RefreshBoardEventArgs> RefreshBoardEventHandler { get; set; }
    public EventHandler<RefreshMoveListEventArgs> RefreshMoveListEventHandler { get; set; }
    public EventHandler<GameOverEventArgs> GameOverEventHandler { get; set; }
    public EventHandler<PromotePieceEventArgs> PromotePieceEventHandler { get; set; }

}