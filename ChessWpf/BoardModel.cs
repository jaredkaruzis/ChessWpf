using ChessEngine;
using ChessWpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ChessWpf; 

public class BoardModel : IBoardModel {

    public Board CurrentGame { get; set; }

    public string WhitePlayerName { get; set; }
    public string BlackPlayerName { get; set; }

    public AI.LogicType OpponentLogic { get; set; }

    public Color OpponentColor { get; set; }

    public EventHandler<RefreshBoardEventArgs> RefreshBoardEventHandler { get; set; }
    public EventHandler<GameOverEventArgs> GameOverEventHandler { get; set; }

    public BoardModel() {}

    public void StartNewGame(Color PlayerColor = Color.White) {
        OpponentColor = PlayerColor == Color.Black ? Color.White : Color.Black;
        OpponentLogic = AI.LogicType.Minmax;
        CurrentGame = new Board();
        Refresh();
    }

    public bool SubmitMove(string move) {
        var success = CurrentGame.SubmitMove(move);
        Refresh();
        return success;
    }

    public bool SubmitMove(Square origin, Square destination) {
        var success = CurrentGame.SubmitMove(origin, destination);
        Refresh();
        return success;
    }

    public Color CurrentTurn() { 
        if (CurrentGame == null) return Color.NoColor;
        else return CurrentGame.CurrentTurn;
    }

    private void Refresh() {
        var args = new RefreshBoardEventArgs() {
            Squares = FlattenSquares(),
        };
        Application.Current.Dispatcher.Invoke(new Action(() => RefreshBoardEventHandler(this, args)));

        if (CurrentGame.GameOver) {
            var e = new GameOverEventArgs() {
                Winner = CurrentGame.Winner,
                Message = CurrentGame.GameOverMessage,
            };
            Application.Current.Dispatcher.Invoke(new Action(() => GameOverEventHandler(this, e)));
        } 
        else if (CurrentGame.CurrentTurn == OpponentColor) {
            //var task = new Task(MoveAI);
            //task.Start();
        }
    }

    private void MoveAI() {
        switch (OpponentLogic) {
            case AI.LogicType.Minmax:
                var move = AI.MinmaxMove(CurrentGame);
                SubmitMove(move[0], move[1]);
                break;
            default:
                move = AI.RandomMove(CurrentGame);
                SubmitMove(move[0], move[1]);
                break;
        }
    }

    private List<Square> FlattenSquares() {
        var squares = new List<Square>();
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                squares.Add(CurrentGame.Squares[j, i]);
            }
        }
        return squares;
    }
}
