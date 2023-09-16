using ChessEngine;
using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessWpf; 

public class BoardManager : IBoardManager {

    public Board CurrentGame { get; set; }

    public int OpponenetLevel { get; set; }     // level 1 = easy, level 2 = medium, level 3 = hard

    public Color OpponentColor { get; set; }

    public EventHandler<RefreshBoardEventArgs> RefreshBoardEventHandler { get; set; }
    public EventHandler<RefreshMoveListEventArgs> RefreshMoveListEventHandler { get; set; }
    public EventHandler<GameOverEventArgs> GameOverEventHandler { get; set; }
    public EventHandler<PromotePieceEventArgs> PromotePieceEventHandler{ get; set; }

    public BoardManager() {}

    public void StartNewGame(Color PlayerColor = Color.White) {
        OpponentColor = PlayerColor == Color.Black ? Color.White : Color.Black;
        OpponenetLevel = 2;
        CurrentGame = new Board();
        Refresh();
    }

    public void StartNewGame(Color PlayerColor, int AiLevel) {
        OpponentColor = PlayerColor == Color.Black ? Color.White : Color.Black;
        OpponenetLevel = AiLevel;
        CurrentGame = new Board();
        Refresh();
    }

    public bool SubmitMove(string move) {
        var success = CurrentGame.SubmitMove(move);
        Refresh();
        return success;
    }

    public bool SubmitMove(Square origin, Square destination, PieceType promotionType = PieceType.Empty) {
        if (origin.Piece.IsPawn && (destination.Y == 0 || destination.Y == 7) && promotionType == PieceType.Empty) {
            WaitForPiecePromotion(origin, destination);
            return false;
        }
        var success = CurrentGame.SubmitMove(origin, destination, promotionType);
        Refresh();
        return success;
    }

    public Color CurrentTurn() { 
        if (CurrentGame == null) return Color.NoColor;
        else return CurrentGame.CurrentTurn;
    }

    private void Refresh() {
        RefreshBoard();
        RefreshMoveList();
      
        if (CurrentGame.GameOver) {
            NotifyGameOver();
        }
        else if (CurrentGame.CurrentTurn == OpponentColor) {
            var aiTask = new Task(MoveAI);
            aiTask.Start();
        }
    }

    private void RefreshBoard() {
        var e = new RefreshBoardEventArgs() {
            Squares = FlattenSquares(),
        };
        Application.Current.Dispatcher.Invoke(new Action(() => RefreshBoardEventHandler(this, e)));
    }

    private void RefreshMoveList() {
        var e = new RefreshMoveListEventArgs() {
            MoveList = CurrentGame.MoveHistory.Select(move => move.AlgebraicNotation).ToList(),
        };
        Application.Current.Dispatcher.Invoke(new Action(() => RefreshMoveListEventHandler(this, e)));   
    }

    private void NotifyGameOver() {
        var e = new GameOverEventArgs() {
            Winner = CurrentGame.Winner,
            Message = CurrentGame.GameOverMessage,
        };
        Application.Current.Dispatcher.Invoke(new Action(() => GameOverEventHandler(this, e)));
    }

    private void WaitForPiecePromotion(Square origin, Square destination) {
        var e = new PromotePieceEventArgs() {
            OriginSquare = origin,
            DestinationSquare = destination,
        };
        Application.Current.Dispatcher.Invoke(new Action(() => PromotePieceEventHandler(this, e)));
    }

    private void MoveAI() {
        var move = AI.MinmaxMove(CurrentGame);
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
