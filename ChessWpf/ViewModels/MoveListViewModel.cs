using Prism.Mvvm;
using System.Collections.Generic;

namespace ChessWpf; 

public class MoveListViewModel : BindableBase, IMoveListViewModel {

    private readonly IBoardModel _boardModel;

    private List<string> _moveList;
    public List<string> MoveList {
        get => _moveList;
        set => SetProperty(ref _moveList, value);
    }

    public MoveListViewModel(IBoardModel boardModel) {
        _boardModel = boardModel;
        _boardModel.RefreshMoveListEventHandler += Refresh;
    }

    public void Refresh(object sender, RefreshMoveListEventArgs e) {
        MoveList = e.MoveList;
    }
}
