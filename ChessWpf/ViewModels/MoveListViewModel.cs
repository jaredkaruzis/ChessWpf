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
        var moves = new List<MoveListModel>();

        var chunks = e.MoveList.Chunk(2).ToList();
        for (int i = 0; i < chunks.Count(); i++) {
            moves.Add(new MoveListModel(chunks[i], i + 1));
        }

        MoveList = moves;
    }
}
