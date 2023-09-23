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

    public void ImportPGN() {
        try {
            var PGN = ImportPGNFromFile();
            var loadedBoard = new Board(PGN, pgn: true);
            _boardManager.LoadGame(loadedBoard);
        }
        catch {
            // TODO write error handler and UI notification
        }
    }

    public void ImportFEN() {
        try {
            var FEN = ImportFENFromFile();
            var loadedBoard = new Board(FEN);
            _boardManager.LoadGame(loadedBoard);
        }
        catch {
            // TODO write error handler and UI notification
        }
    }

    private string ImportFENFromFile() {
        var dialog = new OpenFileDialog();
        dialog.Filter = "FEN Files (*.fen)|*.fen";
        if (dialog.ShowDialog() != null) {
            using (var stream = dialog.OpenFile()) {
                using (var reader = new StreamReader(stream)) {
                    var FEN = reader.ReadToEnd();
                    return FEN;
                }
            }
        }
        throw new FileLoadException();
    }

    private string ImportPGNFromFile() {
        var dialog = new OpenFileDialog();
        dialog.Filter = "PGN Files (*.pgn)|*.pgn";
        if (dialog.ShowDialog() != null) {
            using (var stream = dialog.OpenFile()) {
                using (var reader = new StreamReader(stream)) {
                    var PGN = reader.ReadToEnd();
                    return PGN;
                }
            }
        }
        throw new FileLoadException();
    }
}