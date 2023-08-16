﻿using ChessEngine;
using Prism.Commands;
using Prism.Mvvm;
using System.ComponentModel;
using System.Dynamic;

namespace ChessWpf; 
public class NewGameViewModel : BindableBase, INotifyPropertyChanged, INewGameViewModel {

    public IBoardModel _boardModelManager;

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

    public NewGameViewModel(IBoardModel boardModel) {
        _boardModelManager= boardModel;

        Commands.StartNewGame = new DelegateCommand(StartNewGame);
        Commands.SelectWhite = new DelegateCommand(SelectWhite);
        Commands.SelectBlack = new DelegateCommand(SelectBlack);
        Commands.Awake = new DelegateCommand(Awake);

        IsStartingNewGame = true;
    }

    public void Awake() {
        IsStartingNewGame = false;
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
