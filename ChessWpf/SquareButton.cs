using ChessEngine;
using ChessWpf.ViewModels;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Dynamic;
using System.Windows.Media;
using System;
using System.Windows;

namespace ChessWpf;

public class SquareButton : BindableBase {

    public dynamic Commands { get; } = new ExpandoObject();

    public event EventHandler<HighlightSquaresArgs> HighlightSquares;

    public Square Square;   // TODO REMOVE BOARD REFERENCE
    private Piece _piece;   // TODO REMOVE BOARD REFERENCE

    public Piece Piece {
        get => _piece;
        set {
            SetProperty(ref _piece, value);
            if (value != null) {
                ImagePath = Piece.Color == ChessEngine.Color.White ? whitePieceImages[Piece.Type] : blackPieceImages[Piece.Type];
            }
            else {
                ImagePath = "Images/empty.png";
            }
        }
    }

    private SolidColorBrush GetBackground() {
        if (Square.X % 2 == 0) {
            return Square.Y % 2 == 0 ? new SolidColorBrush(Colors.LightGray) : new SolidColorBrush(Colors.DarkGray);
        }
        else {
            return Square.Y % 2 == 1 ? new SolidColorBrush(Colors.LightGray) : new SolidColorBrush(Colors.DarkGray);
        }
    }

    private string _imagePath;
    public string ImagePath {
        get => _imagePath;
        set => SetProperty(ref _imagePath, value);
    }

    private bool _selected;
    public bool Selected {
        get => _selected;
        set {
            ButtonColor = value ? new SolidColorBrush(Colors.Red) : GetBackground();
            SetProperty(ref _selected, value);
        }
    }

    private bool _highlighted;
    public bool Highlighted {
        get => _highlighted;
        set {
            ButtonColor = value ? new SolidColorBrush(Colors.Yellow) : GetBackground();
            SetProperty(ref _highlighted, value);
        }
    }

    private SolidColorBrush _buttonColor;
    public SolidColorBrush ButtonColor {
        get => _buttonColor;
        set => SetProperty(ref _buttonColor, value);
    }

    public SquareButton(Square s) {
        Square = s;
        ButtonColor = GetBackground();
        Commands.SelectSquare = new DelegateCommand(Select);
        Piece = s.Piece;
    }

    public void Select() {
        var moves = new List<int[]>();
        if (Piece != null) {
            foreach (var square in Piece.Moves) {
                moves.Add(new int[2] { square.X, square.Y });
            }
        }

        Application.Current.Dispatcher.Invoke(new Action(() => {
            HighlightSquares?.Invoke(this, new HighlightSquaresArgs(this, moves));
        }));
    }

    public static Dictionary<PieceType, string> whitePieceImages = new Dictionary<PieceType, string>() {
            {PieceType.Bishop, "Images/Chess_blt60.png" },
            {PieceType.King, "Images/Chess_klt60.png" },
            {PieceType.Knight, "Images/Chess_nlt60.png" },
            {PieceType.Pawn, "Images/Chess_plt60.png" },
            {PieceType.Queen, "Images/Chess_qlt60.png" },
            {PieceType.Rook, "Images/Chess_rlt60.png" },
        };
    public static Dictionary<PieceType, string> blackPieceImages = new Dictionary<PieceType, string>() {
            {PieceType.Bishop, "Images/Chess_bdt60.png" },
            {PieceType.King, "Images/Chess_kdt60.png" },
            {PieceType.Knight, "Images/Chess_ndt60.png" },
            {PieceType.Pawn, "Images/Chess_pdt60.png" },
            {PieceType.Queen, "Images/Chess_qdt60.png" },
            {PieceType.Rook, "Images/Chess_rdt60.png" },
        };
}