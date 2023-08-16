using ChessWpf.ViewModels;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Dynamic;
using System.Windows;

namespace ChessWpf;

public class MenuViewModel : BindableBase, IMenuViewModel {

    private readonly INewGameManager _newGameManager;

    public dynamic Commands { get; } = new ExpandoObject();


    public MenuViewModel(INewGameManager newGameManager) {
        _newGameManager = newGameManager;

        Commands.StartNewGame = new DelegateCommand(StartNewGame);
        Commands.StartExportGame = new DelegateCommand(StartExportGame);
        Commands.StartImportGame = new DelegateCommand(StartImportGame);
    }

    public void StartNewGame() {
        _newGameManager.OpenNewGameMenu();
    }

    public void StartExportGame() {

    }

    public void StartImportGame() {

    }
}
