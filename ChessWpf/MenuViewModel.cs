using Prism.Commands;
using Prism.Mvvm;
using System.Dynamic;

namespace ChessWpf;

public class MenuViewModel : BindableBase, IMenuViewModel
{
    private IBoardModel _boardModelManager;

    public dynamic Commands { get; } = new ExpandoObject();

    public MenuViewModel(IBoardModel boardModel) {
        _boardModelManager = boardModel;
        Commands.StartNewGame = new DelegateCommand(StartNewGame);
    }

    public void StartNewGame() {
        _boardModelManager.StartNewGame(ChessEngine.Color.White);
    }
}
