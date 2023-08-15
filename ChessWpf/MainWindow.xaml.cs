using System.Windows;

namespace ChessWpf; 
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, IMainWindow {

    IBoardViewModel bvm;

    public MainWindow(IBoardViewModel bvm) {
        InitializeComponent();
        this.bvm = bvm;
        this.DataContext = bvm;
    }
}
