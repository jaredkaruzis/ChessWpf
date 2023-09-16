using Prism.Mvvm;

namespace ChessWpf;

public class MoveListModel : BindableBase {

    private string _whiteMove;
    public string WhiteMove { 
        get => _whiteMove;
        set => SetProperty(ref _whiteMove, value);
    }

    private string _blackMove;
    public string BlackMove {
        get => _blackMove;
        set => SetProperty(ref _blackMove, value);
    }

    public MoveListModel(string[] chunk) {
        _whiteMove = string.Empty;
        _blackMove = string.Empty;

        if (chunk.Length > 0) {
            _whiteMove = chunk[0];
        }
        if (chunk.Length > 1) {
            _blackMove = chunk[1];
        }
    }
}
