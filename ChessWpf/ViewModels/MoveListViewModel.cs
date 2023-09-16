using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChessWpf; 

public class MoveListViewModel : BindableBase, IMoveListViewModel {

    private readonly IBoardManager _boardModel;

    private List<MoveListModel> _moveList = new List<MoveListModel>();
    public List<MoveListModel> MoveList {
        get => _moveList;
        set => SetProperty(ref _moveList, value);
    }

    public MoveListViewModel(IBoardManager boardModel) {
        _boardModel = boardModel;
        _boardModel.RefreshMoveListEventHandler += Refresh;
    }

    public void Refresh(object sender, RefreshMoveListEventArgs e) {
        MoveList = e.MoveList.Chunk(2).Select(moveRow => new MoveListModel(moveRow)).ToList();
    }
}
