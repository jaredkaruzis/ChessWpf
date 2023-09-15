using ChessEngine;
using System;

namespace ChessWpf;

public class PromotePieceEventArgs : EventArgs {
    public Square OriginSquare;
    public Square DestinationSquare;
}