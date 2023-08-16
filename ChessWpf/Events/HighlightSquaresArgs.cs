using System.Collections.Generic;
using System;

namespace ChessWpf;

public class HighlightSquaresArgs : EventArgs {
    public SquareButton clicked;
    public List<int[]> squares;
    public HighlightSquaresArgs(SquareButton o, List<int[]> s) {
        clicked = o;
        squares = s;
    }
}