using System.Windows;

namespace ChessWpf; 

public partial class MainWindow : Window, IMainWindow {

    public MainWindow(IBoardViewModel bvm) {
        InitializeComponent();
        this.DataContext = bvm;
    }
}
