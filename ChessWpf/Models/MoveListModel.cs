using Prism.Mvvm;

namespace ChessWpf;

public class MoveListModel : BindableBase {

    private int _index = 0;
    public int Index {
        get => _index;
        set => SetProperty(ref _index, value);
    }

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

    public MoveListModel(string[] chunk, int moveNumber) {
        _whiteMove = string.Empty;
        _blackMove = string.Empty;

        Index = moveNumber;

        if (chunk.Length > 0) {
            _whiteMove = chunk[0];
        }
        if (chunk.Length > 1) {
            _blackMove = chunk[1];
        }
    }
}
