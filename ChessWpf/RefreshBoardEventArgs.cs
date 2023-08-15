using ChessEngine;
using System;
using System.Collections.Generic;

namespace ChessWpf.ViewModels;

public class RefreshBoardEventArgs : EventArgs {
    public List<Square> Squares { get; set; }
}