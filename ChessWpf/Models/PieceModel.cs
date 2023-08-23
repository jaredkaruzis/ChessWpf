using ChessEngine;
using System.Collections.Generic;

namespace ChessWpf;

public class PieceModel {

    public PieceType Type;
    public Color Color;

    public List<int[]> Moves;

    public PieceModel(Piece p) {
        Type = p.Type;
        Color = p.Color;
        Moves = GetMoves(p.Moves);
    }

    private List<int[]> GetMoves(List<Square> moves) {
        var moveList = new List<int[]>();
        foreach(var move in moves) {
            moveList.Add(new int[] {move.X, move.Y});
        }
        return moveList;
    }
}
