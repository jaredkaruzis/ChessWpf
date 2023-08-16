using ChessEngine;
using System;
using System.Collections.Generic;

namespace ChessWpf;

public class RefreshBoardEventArgs : EventArgs {
    public List<Square> Squares { get; set; }
}