using System.Dynamic;
using Prism.Mvvm;
using Prism.Commands;

namespace ChessWpf;

public class MenuViewModel : BindableBase, IMenuViewModel {

    private readonly INewGameManager _newGameManager;
    private readonly IExportManager _exportManager;
    private readonly IImportManager _importManager;

    public dynamic Commands { get; } = new ExpandoObject();


    public MenuViewModel(
            INewGameManager newGameManager, 
            IExportManager exportManager,
            IImportManager importManager
    ){
        _newGameManager = newGameManager;
        _exportManager = exportManager;
        _importManager = importManager;

        Commands.StartNewGame = new DelegateCommand(StartNewGame);
        Commands.StartExportGame = new DelegateCommand(StartExportGame);
        Commands.StartImportGame = new DelegateCommand(StartImportGame);
    }

    public void StartNewGame() {
        _newGameManager.OpenNewGameMenu();
    }

    public void StartExportGame() {
        _exportManager.Export();
    }

    public void StartImportGame() {
        var board = _importManager.ImportPGN();
    }
}
