using System;
using System.Collections.Generic;

namespace ChessWpf;

public class RefreshMoveListEventArgs : EventArgs {
    public List<string> MoveList;
}
