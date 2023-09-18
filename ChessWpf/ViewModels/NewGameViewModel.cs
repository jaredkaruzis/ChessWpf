using ChessEngine;
using System;
using System.Dynamic;
using System.ComponentModel;
using Prism.Mvvm;
using Prism.Commands;

namespace ChessWpf;

public class NewGameViewModel : BindableBase, INotifyPropertyChanged, INewGameViewModel {

    private IBoardManager _boardModelManager;
    private INewGameManager _newGameManager;

    private bool _isStartingNewGame;
    public bool IsStartingNewGame {
        get => _isStartingNewGame;
        set => SetProperty(ref _isStartingNewGame, value);
    }

    private bool _playerIsWhite;
    public bool PlayerIsWhite {
        get => _playerIsWhite;
        set => SetProperty(ref _playerIsWhite, value);
    }

    private bool _playerIsBlack;
    public bool PlayerIsBlack {
        get => _playerIsBlack; 
        set => SetProperty(ref _playerIsBlack, value); 
    }

    private bool _useAI;
    public bool UseAI {
        get => _useAI;
        set => SetProperty(ref _useAI, value);
    }

    public Color PlayerColor { get; set; }
    public int AiLevel { get; set; }
    public dynamic Commands { get; } = new ExpandoObject();

    public NewGameViewModel(IBoardManager boardModel, INewGameManager newGameManager) {
        _boardModelManager = boardModel;
        _newGameManager = newGameManager;

        _newGameManager.OpenNewGameMenuEvent += Awake;

        Commands.StartNewGame = new DelegateCommand(StartNewGame);
        Commands.SelectWhite = new DelegateCommand(SelectWhite);
        Commands.SelectBlack = new DelegateCommand(SelectBlack);

        IsStartingNewGame = true;
        SelectWhite();
    }

    public void Awake(object sender, EventArgs e) {
        IsStartingNewGame = true;
        SelectWhite();
    }

    public void StartNewGame() {
        _boardModelManager.StartNewGame(PlayerColor, UseAI ? AiLevel : 0);
        IsStartingNewGame = false;
    }

    public void SelectWhite() {
        PlayerColor = Color.White;
        PlayerIsWhite = true;
        PlayerIsBlack = false;
    }
    
    public void SelectBlack() {
        PlayerColor = Color.Black;
        PlayerIsWhite = false;
        PlayerIsBlack = true;
    }
}
