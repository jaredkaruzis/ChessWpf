using ChessEngine;

namespace ChessWpf; 

public interface INewGameViewModel {

    public bool IsStartingNewGame { get; set; }
    public Color PlayerColor { get; set; }
    public string PlayerName { get; set; }
    public int AiLevel { get; set; }

    public bool PlayerIsWhite { get; set; }
    public bool PlayerIsBlack { get; set; }

    public void StartNewGame();
    public void SelectWhite();
    public void SelectBlack();
}