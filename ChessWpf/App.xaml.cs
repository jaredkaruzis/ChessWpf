using ChessWpf.ViewModels;
using Prism.Mvvm;
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
        container.RegisterType<IMenuViewModel, MenuViewModel>();
        container.RegisterType<INewGameViewModel, NewGameViewModel>();

        container.RegisterType<IMainWindow, MainWindow>();

        ViewModelLocationProvider.SetDefaultViewModelFactory((IMenuViewModel) => container.Resolve(IMenuViewModel));
        ViewModelLocationProvider.SetDefaultViewModelFactory((INewGameViewModel) => container.Resolve(INewGameViewModel));

        var mainWindow = container.Resolve<MainWindow>(); // Creating Main window
        mainWindow.Show();
    }
}
