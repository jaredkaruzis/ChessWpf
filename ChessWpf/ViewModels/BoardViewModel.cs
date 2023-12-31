﻿using ChessEngine;
using System.Linq;
using System.Dynamic;
using System.Windows.Data;
using System.ComponentModel;
using System.Collections.Generic;
using Prism.Mvvm;
using Prism.Commands;

namespace ChessWpf; 

public class BoardViewModel : BindableBase, IBoardViewModel {

    private readonly IBoardManager _boardModelManager;

    private List<SquareButton> _squareButtons = new List<SquareButton>();
    public List<SquareButton> SquareButtons {
        get => _squareButtons;
        set => SetProperty(ref _squareButtons, value);
    }

    public ICollectionView Squares { get; private set; }

    public dynamic Commands { get; } = new ExpandoObject();

    private SquareButton _originSquare;
    private List<SquareButton> _highlightedSquares = new List<SquareButton>();

    public BoardViewModel(IBoardManager boardModel) {
        _boardModelManager = boardModel;
        _boardModelManager.RefreshBoardEventHandler += OnRefreshBoard;

        Squares = CollectionViewSource.GetDefaultView(SquareButtons);
        LoadDummyBoard();
    }

    public void Refresh(List<SquareModel> inSquares) {
        SquareButtons.Clear();
        foreach (var s in inSquares) {
            var squareButton = new SquareButton(s);
            squareButton.HighlightSquares += OnSquareSelected;
            SquareButtons.Add(squareButton);
        }
        Squares.Refresh();
    }

    public void OnRefreshBoard(object sender, RefreshBoardEventArgs e) {
        Refresh(e.Squares);
    }

    public void OnSquareSelected(object sender, HighlightSquaresArgs e) {
        
        // This is the first click, starting off fresh. No need to reset anything
        if (_originSquare == null) {

            if (e.clicked.Piece == null) {      
                return; // Clicked an empty square, cancel selection
            }
            if (e.clicked.Piece.Color != _boardModelManager.CurrentTurn()) {    
                return; // Clicked a enemy piece, cancel selection
            }
            // Select this piece and highlight its moves
            _originSquare = e.clicked;
            _originSquare.Selected = true;

            foreach (var s in SquareButtons) {
                foreach (var move in e.squares) {
                    if (s.Square.X == move[0] && s.Square.Y == move[1]) {
                        s.Highlighted = true;
                        _highlightedSquares.Add(s);
                    }
                }
            }
        }   
        // Second click. Activate move OR deselect current piece AND/OR select a new piece
        else if (_originSquare != null) {

            // Reclick, just clear the selected square
            if (_originSquare == e.clicked) {
                ClearHighlightedSquares();
                _originSquare.Selected = false;
                _originSquare = null;
                return;
            }

            // Check if the clicked square was highlighted
            if (_highlightedSquares.Any()) {

                foreach (var a in _highlightedSquares) {

                    // If the square was highlighted, execute the move
                    if (a.Square.X == e.clicked.Square.X && a.Square.Y == e.clicked.Square.Y) {

                        ClearHighlightedSquares();
                        _originSquare.Selected = false;

                        if (!_boardModelManager.SubmitMove(_originSquare.Square.SquareReference, e.clicked.Square.SquareReference)) {
                            //TODO Error Handling? //TODO Figure out how to process moves into notation of some kind 
                        }
                        _originSquare.Piece = null;
                        _originSquare = null;
                        return;
                    }
                }
            }

            // Not a move, reset
            ClearHighlightedSquares();
            _originSquare.Selected = false;
            _originSquare = null;

            // If we clicked on a different piece, select it and do highlighting
            if (e.clicked.Piece != null) {

                // If the piece is not-turn, don't highlight it
                if (e.clicked.Piece.Color != _boardModelManager.CurrentTurn()) {
                    return;
                }

                // Otherwise do it up
                _originSquare = e.clicked;
                _originSquare.Selected = true;
                foreach(var move in e.squares) {
                    foreach(var butt in SquareButtons) {
                        if (move[0] == butt.Square.X && move[1] == butt.Square.Y) {
                            butt.Highlighted = true;
                            _highlightedSquares.Add(butt);
                        }
                    }
                }
            }
        }
    }

    public void ClearHighlightedSquares() {
        _highlightedSquares.ForEach(x => x.Highlighted = false);
        _highlightedSquares.Clear();
    }

    private void LoadDummyBoard() {
        for (int i = 0; i < 64; i++) {
            SquareButtons.Add(new SquareButton(new SquareModel(new Square(i / 8, i % 8))));
        }
    }
}
