using ChessEngine;
using Microsoft.Win32;
using System;
using System.IO;

namespace ChessWpf; 

public class ImportManager : IImportManager {

    private readonly IBoardManager _boardManager;

    public ImportManager(IBoardManager boardManager) {
        _boardManager = boardManager;
    }

    public Board ImportPGN() {
        Board returnBoard = null;

        var dialog = new OpenFileDialog();
        dialog.Filter = "PGN Files (*.pgn)|*.pgn";
        if (dialog.ShowDialog() != null) {
            using (var stream = dialog.OpenFile()) {
                using (var reader = new StreamReader(stream)) {
                    var PGN = reader.ReadToEnd();
                    returnBoard = new Board(PGN, true);
                }
            }
        }
        return returnBoard ?? throw new Exception();
    }

    public Board ImportFEN() {
        Board returnBoard = null;

        var dialog = new OpenFileDialog();
        dialog.Filter = "FEN Files (*.fen)|*.fen";
        if (dialog.ShowDialog() != null) {
            using (var stream = dialog.OpenFile()) {
                using (var reader = new StreamReader(stream)) {
                    var PGN = reader.ReadToEnd();
                    returnBoard = new Board(PGN, true);
                }
            }
        }
        return returnBoard ?? throw new Exception();
    }
}