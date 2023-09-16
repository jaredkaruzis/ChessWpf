using Microsoft.Win32;
using System.IO;

namespace ChessWpf; 

public class ExportManager : IExportManager {

    private readonly IBoardManager _boardManager;

    public ExportManager(IBoardManager boardModel) { 
        _boardManager = boardModel;
    }

    public void Export() {
        ExportPGN();
    }

    public void ExportPGN() {
        var dialog = new SaveFileDialog();
        dialog.Filter = "PGN Files (*.pgn)|*.pgn";
        if (dialog.ShowDialog() != null) {
            using (var stream = dialog.OpenFile()) {
                using (var writer = new StreamWriter(stream)) {
                    writer.Write(_boardManager.CurrentGame.ExportPGN());
                }
            }
        }
    }

    public void ExportFEN() {
        var dialog = new SaveFileDialog();
        dialog.Filter = "FEN Files (*.fen)|*.fen";
        if (dialog.ShowDialog() != null) {
            using (var stream = dialog.OpenFile()) {
                using (var writer = new StreamWriter(stream)) {
                    writer.Write(_boardManager.CurrentGame.ExportFEN());
                }
            }
        }
    }
}
