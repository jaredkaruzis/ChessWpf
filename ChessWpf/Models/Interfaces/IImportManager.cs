using ChessEngine;

namespace ChessWpf; 
public interface IImportManager {
    public Board ImportPGN();
    public Board ImportFEN();
}