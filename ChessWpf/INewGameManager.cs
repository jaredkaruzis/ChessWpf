using System;

namespace ChessWpf {
    public interface INewGameManager {
        public EventHandler<EventArgs> OpenNewGameMenuEvent { get; set; }
        public void OpenNewGameMenu();

    }
}