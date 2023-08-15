using ChessWpf.ViewModels;
using Prism.Unity;
using System.Windows;
using Unity;

namespace ChessWpf; 
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App {

    protected override void OnStartup(StartupEventArgs e) {
        base.OnStartup(e);
        IUnityContainer container = new UnityContainer();

        container.RegisterSingleton<IBoardModel, BoardModel>();

        container.RegisterType<IBoardViewModel, BoardViewModel>();
        container.RegisterType<IMainWindow, MainWindow>();

        var mainWindow = container.Resolve<MainWindow>(); // Creating Main window
        mainWindow.Show();
    }
}
