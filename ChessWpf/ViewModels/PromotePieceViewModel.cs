using Prism.Commands;
using System.Dynamic;
using ChessEngine;
using Prism.Mvvm;

namespace ChessWpf;

public class PromotePieceViewModel : BindableBase, IPromotePieceViewModel {

    public readonly IBoardModel _boardModel;

    private Square _origin;
    private Square _destination;

    public dynamic Commands { get; } = new ExpandoObject();


    private bool _isPromotingPiece;
    public bool IsPromotingPiece {
        get => _isPromotingPiece;
        set => SetProperty(ref _isPromotingPiece, value);
    }

    private string _knightImagePath;
    public string KnightImagePath {
        get => _knightImagePath;
        set => SetProperty(ref _knightImagePath, value);
    }

    private string _bishopImagePath;
    public string BishopImagePath {
        get => _bishopImagePath;
        set => SetProperty(ref _bishopImagePath, value);
    }

    private string _rookImagePath;
    public string RookImagePath {
        get => _rookImagePath;
        set => SetProperty(ref _rookImagePath, value);
    }

    private string _queenImagePath;
    public string QueenImagePath {
        get => _queenImagePath;
        set => SetProperty(ref _queenImagePath, value);
    }

    public PromotePieceViewModel(IBoardModel boardModel) {
        _boardModel = boardModel;
        _boardModel.PromotePieceEventHandler += BeginPromotingPiece;

        Commands.SelectKnight = new DelegateCommand(SelectKnight);
        Commands.SelectBishop = new DelegateCommand(SelectBishop);
        Commands.SelectRook = new DelegateCommand(SelectRook);
        Commands.SelectQueen = new DelegateCommand(SelectQueen);
    }

    public void BeginPromotingPiece(object sender, PromotePieceEventArgs e) {
        IsPromotingPiece = true;

        _origin = e.OriginSquare;
        _destination = e.DestinationSquare;

        var colorString = e.OriginSquare.Piece.IsWhite ? "white" : "black";
        KnightImagePath = $"Images/{colorString}knight.png";
        BishopImagePath = $"Images/{colorString}bishop.png";
        RookImagePath = $"Images/{colorString}rook.png";
        QueenImagePath = $"Images/{colorString}queen.png";
    }

    public void SelectKnight() {
        CompleteMove(PieceType.Knight);
    }

    public void SelectBishop() {
        CompleteMove(PieceType.Bishop);
    }

    public void SelectRook() {
        CompleteMove(PieceType.Rook);
    }

    public void SelectQueen() {
        CompleteMove(PieceType.Queen);
    }

    public void CompleteMove(PieceType type) {
        _boardModel.SubmitMove(_origin, _destination, type);

        IsPromotingPiece = false;

        _origin = null;
        _destination = null;
    }
}
