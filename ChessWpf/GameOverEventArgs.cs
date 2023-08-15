using ChessEngine;
using System;

namespace ChessWpf; 
public class GameOverEventArgs : EventArgs {
    public Color Winner { get; set; }
    public string Message { get; set; }
}