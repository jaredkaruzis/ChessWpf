using Prism.Commands;
using Prism.Mvvm;
using System.Dynamic;

namespace ChessWpf;

public class GameOverViewModel : BindableBase, IGameOverViewModel{

    private readonly IBoardManager _boardModelManager;
    private readonly INewGameManager _newGameManager;

    public dynamic Commands { get; } = new ExpandoObject();

    private bool _isGameOver;
    public bool IsGameOver {
        get => _isGameOver;
        set => SetProperty(ref _isGameOver, value);
    }

    private string _winnerName;
    public string WinnerName {
        get => _winnerName;
        set => SetProperty(ref _winnerName, value);
    }

    private string _winMessage;
    public string WinMessage {
        get => _winMessage;
        set => SetProperty(ref _winMessage, value); 
    }

    public GameOverViewModel(IBoardManager boardModelManager, INewGameManager newGameManager) {
        _boardModelManager = boardModelManager;
        _newGameManager = newGameManager;

        Commands.StartNewGame = new DelegateCommand(StartNewGame);

        _boardModelManager.GameOverEventHandler += HandleGameOver;
    }

    private void StartNewGame() {
        IsGameOver = false;
        WinMessage = string.Empty;
        WinnerName = string.Empty;
        _newGameManager.OpenNewGameMenu();
    }

    private void HandleGameOver(object sender, GameOverEventArgs e) {
        IsGameOver = true;
        WinnerName = e.Winner == ChessEngine.Color.White ? "White wins!" : e.Winner == ChessEngine.Color.Black ? "Black wins!" : "Draw!";
        WinMessage = e.Message;
    }
}
