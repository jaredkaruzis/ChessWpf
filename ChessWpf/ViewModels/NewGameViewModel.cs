using ChessEngine;
using System;
using System.Dynamic;
using System.ComponentModel;
using Prism.Mvvm;
using Prism.Commands;

namespace ChessWpf;

public class NewGameViewModel : BindableBase, INotifyPropertyChanged, INewGameViewModel {

    private IBoardModel _boardModelManager;
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

    public string PlayerName { get; set; }
    public Color PlayerColor { get; set; }
    public int AiLevel { get; set; }
    public dynamic Commands { get; } = new ExpandoObject();

    public NewGameViewModel(IBoardModel boardModel, INewGameManager newGameManager) {
        _boardModelManager = boardModel;
        _newGameManager = newGameManager;

        _newGameManager.OpenNewGameMenuEvent += Awake;

        Commands.StartNewGame = new DelegateCommand(StartNewGame);
        Commands.SelectWhite = new DelegateCommand(SelectWhite);
        Commands.SelectBlack = new DelegateCommand(SelectBlack);

        IsStartingNewGame = false;
    }

    public void Awake(object sender, EventArgs e) {
        IsStartingNewGame = true;
    }

    public void StartNewGame() {
        _boardModelManager.StartNewGame(PlayerColor, AiLevel, PlayerName);
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
