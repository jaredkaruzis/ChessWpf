using System;
using System.Windows;

namespace ChessWpf;

public class NewGameManager : INewGameManager {

    public EventHandler<EventArgs> OpenNewGameMenuEvent { get; set; }

    public NewGameManager() { }

    public void OpenNewGameMenu() {
        Application.Current.Dispatcher.Invoke(new Action(() => OpenNewGameMenuEvent(this, null)));
    }
}
