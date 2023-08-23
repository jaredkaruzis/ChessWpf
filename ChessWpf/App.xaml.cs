using Prism.Mvvm;
using System.Windows;
using Unity;

namespace ChessWpf; 

public partial class App {

    protected override void OnStartup(StartupEventArgs e) {
        base.OnStartup(e);
        IUnityContainer container = new UnityContainer();

        container.RegisterSingleton<IBoardModel, BoardModel>();
        container.RegisterSingleton<INewGameManager, NewGameManager>();
        container.RegisterSingleton<IExportManager, ExportManager>();

        container.RegisterType<IBoardViewModel, BoardViewModel>();
        container.RegisterType<IMenuViewModel, MenuViewModel>();
        container.RegisterType<INewGameViewModel, NewGameViewModel>();
        container.RegisterType<IGameOverViewModel, GameOverViewModel>();

        container.RegisterType<IMainWindow, MainWindow>();

        ViewModelLocationProvider.SetDefaultViewModelFactory((IMenuViewModel) => container.Resolve(IMenuViewModel));
        ViewModelLocationProvider.SetDefaultViewModelFactory((IBoardViewModel) => container.Resolve(IBoardViewModel));
        ViewModelLocationProvider.SetDefaultViewModelFactory((INewGameViewModel) => container.Resolve(INewGameViewModel));
        ViewModelLocationProvider.SetDefaultViewModelFactory((IGameOverViewModel) => container.Resolve(IGameOverViewModel));

        var mainWindow = container.Resolve<MainWindow>();
        mainWindow.Show();
    }
}
