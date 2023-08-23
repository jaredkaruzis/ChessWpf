using ChessEngine;
using System;
using System.Windows;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ChessWpf; 

public class BoardModel : IBoardModel {

    public Board CurrentGame { get; set; }

    public string WhitePlayerName { get; set; }
    public string BlackPlayerName { get; set; }

    public int OpponenetLevel { get; set; }     // level 1 = easy, level 2 = medium, level 3 = hard

    public Color OpponentColor { get; set; }

    public EventHandler<RefreshBoardEventArgs> RefreshBoardEventHandler { get; set; }
    public EventHandler<GameOverEventArgs> GameOverEventHandler { get; set; }

    public BoardModel() { }

    public void StartNewGame(Color PlayerColor = Color.White) {
        OpponentColor = PlayerColor == Color.Black ? Color.White : Color.Black;
        OpponenetLevel = 2;
        WhitePlayerName = "Player";
        CurrentGame = new Board();
        Refresh();
    }

    public void StartNewGame(Color PlayerColor, int AiLevel, string playerName) {
        OpponentColor = PlayerColor == Color.Black ? Color.White : Color.Black;
        OpponenetLevel = AiLevel;
        if (PlayerColor == Color.White) {
            WhitePlayerName = playerName;
            BlackPlayerName = "AI";
        }
        else {
            WhitePlayerName = "AI";
            BlackPlayerName = playerName;
        }
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
        RefreshBoard();

        if (CurrentGame.GameOver) {
            NotifyGameOver();
        } 
        else if (CurrentGame.CurrentTurn == OpponentColor) {
            var aiTask = new Task(MoveAI);
            aiTask.Start();
        }
    }

    private void RefreshBoard() {
        var args = new RefreshBoardEventArgs() {
            Squares = FlattenSquares(),
        };
        Application.Current.Dispatcher.Invoke(new Action(() => RefreshBoardEventHandler(this, args)));
    }

    private void NotifyGameOver() {
        var e = new GameOverEventArgs() {
            Winner = CurrentGame.Winner,
            Message = CurrentGame.GameOverMessage,
        };
        Application.Current.Dispatcher.Invoke(new Action(() => GameOverEventHandler(this, e)));
    }

    private void MoveAI() {
        var move = AI.MinmaxMove(CurrentGame, OpponenetLevel);
        SubmitMove(move[0], move[1]);
    }

    private List<SquareModel> FlattenSquares() {
        var squares = new List<SquareModel>();
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                if (OpponentColor == Color.Black) {
                    var square = new SquareModel(CurrentGame.Squares[j, i]);
                    squares.Add(square);
                }
                else if (OpponentColor == Color.White) {
                    var square = new SquareModel(CurrentGame.Squares[7 - j, 7 - i]);
                    squares.Add(square);
                }
            }
        }
        return squares;
    }
}
