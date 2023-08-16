using System.Windows;

namespace ChessWpf; 

public partial class MainWindow : Window, IMainWindow {
    // TODO seperate board view model to just the board view 
    public MainWindow(IBoardViewModel bvm) {
        InitializeComponent();
        this.DataContext = bvm;
    }
}
