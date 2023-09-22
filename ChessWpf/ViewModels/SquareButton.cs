using ChessEngine;
using System;
using System.Dynamic;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using Prism.Mvvm;
using Prism.Commands;

namespace ChessWpf;

public class SquareButton : BindableBase {

    public dynamic Commands { get; } = new ExpandoObject();

    public event EventHandler<HighlightSquaresArgs> HighlightSquares;

    public SquareModel Square;
    private PieceModel _piece;

    private SolidColorBrush RedBrush = new SolidColorBrush(Colors.Red);
    private SolidColorBrush LightGrayBrush = new SolidColorBrush(Colors.LightGray);
    private SolidColorBrush DarkGrayBrush = new SolidColorBrush(Colors.DarkGray);
    private SolidColorBrush YellowBrush  = new SolidColorBrush(Colors.Yellow);

    private SolidColorBrush BackgroundBrush;

    public PieceModel Piece {
        get => _piece;
        set {
            SetProperty(ref _piece, value);
            ImagePath = GetImagePath();
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
            ButtonColor = value ? RedBrush : BackgroundBrush;
            SetProperty(ref _selected, value);
        }
    }

    private bool _highlighted;
    public bool Highlighted {
        get => _highlighted;
        set {
            ButtonColor = value ? YellowBrush : BackgroundBrush;
            SetProperty(ref _highlighted, value);
        }
    }

    private SolidColorBrush _buttonColor;
    public SolidColorBrush ButtonColor {
        get => _buttonColor;
        set => SetProperty(ref _buttonColor, value);
    }

    public SquareButton(SquareModel s) {
        Square = s;
        BackgroundBrush = GetBackground();
        ButtonColor = BackgroundBrush;
        Commands.SelectSquare = new DelegateCommand(Select);
        Piece = s.Piece;
    }

    public void Select() {
        Application.Current.Dispatcher.Invoke(new Action(() => {
            HighlightSquares?.Invoke(this, new HighlightSquaresArgs(this, Piece?.Moves));
        }));
    }

    private SolidColorBrush GetBackground() {
        return Square.X % 2 == 0 ?
            (Square.Y % 2 == 0 ? LightGrayBrush : DarkGrayBrush) :
            (Square.Y % 2 == 1 ? LightGrayBrush : DarkGrayBrush);
    }

    private string GetImagePath() {
        if (Piece != null) {
            return Piece.Color == ChessEngine.Color.White ? whitePieceImages[Piece.Type] : blackPieceImages[Piece.Type];
        } else {
            return "../Images/empty.png";
        }
    }

    public static Dictionary<PieceType, string> whitePieceImages = new Dictionary<PieceType, string>() {
            {PieceType.Bishop, "../Images/whitebishop.png" },
            {PieceType.King, "../Images/whiteking.png" },
            {PieceType.Knight, "../Images/whiteknight.png" },
            {PieceType.Pawn, "../Images/whitepawn.png" },
            {PieceType.Queen, "../Images/whitequeen.png" },
            {PieceType.Rook, "../Images/whiterook.png" },
    };

    public static Dictionary<PieceType, string> blackPieceImages = new Dictionary<PieceType, string>() {
            {PieceType.Bishop, "../Images/blackbishop.png" },
            {PieceType.King, "../Images/blackking.png" },
            {PieceType.Knight, "../Images/blackknight.png" },
            {PieceType.Pawn, "../Images/blackpawn.png" },
            {PieceType.Queen, "../Images/blackqueen.png" },
            {PieceType.Rook, "../Images/blackrook.png" },
    };
}