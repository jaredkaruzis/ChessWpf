using ChessEngine;

namespace ChessWpf;

public class SquareModel {

    public int X;
    public int Y;

    public PieceModel Piece;

    public Square SquareReference;  // TODO REMOVE

    public SquareModel(Square square) { 
        X = square.X;
        Y = square.Y;
        if (square.HasPiece) {
            Piece = new PieceModel(square.Piece);
        }
        SquareReference = square;
    }
}
