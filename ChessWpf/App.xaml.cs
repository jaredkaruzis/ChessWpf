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
        container.RegisterType<IMainWindow, MainWindow>();

        ViewModelLocationProvider.SetDefaultViewModelFactory((IMenuViewModel) =>
        {
            return container.Resolve(IMenuViewModel);
        });


        var mainWindow = container.Resolve<MainWindow>(); // Creating Main window
        mainWindow.Show();
    }
}
