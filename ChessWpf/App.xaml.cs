﻿using Prism.Mvvm;
using System.Windows;
using Unity;

namespace ChessWpf; 

public partial class App {

    protected override void OnStartup(StartupEventArgs e) {
        base.OnStartup(e);
        IUnityContainer container = new UnityContainer();

        container.RegisterSingleton<IBoardManager, BoardManager>();
        container.RegisterSingleton<INewGameManager, NewGameManager>();
        container.RegisterSingleton<IExportManager, ExportManager>();
        container.RegisterSingleton<IImportManager, ImportManager>();

        container.RegisterType<IBoardViewModel, BoardViewModel>();
        container.RegisterType<IMenuViewModel, MenuViewModel>();
        container.RegisterType<INewGameViewModel, NewGameViewModel>();
        container.RegisterType<IGameOverViewModel, GameOverViewModel>();
        container.RegisterType<IPromotePieceViewModel, PromotePieceViewModel>();
        container.RegisterType<IMoveListViewModel, MoveListViewModel>();
        
        container.RegisterType<IMainWindow, MainWindow>();

        ViewModelLocationProvider.SetDefaultViewModelFactory((IMenuViewModel) => container.Resolve(IMenuViewModel));
        ViewModelLocationProvider.SetDefaultViewModelFactory((IBoardViewModel) => container.Resolve(IBoardViewModel));
        ViewModelLocationProvider.SetDefaultViewModelFactory((INewGameViewModel) => container.Resolve(INewGameViewModel));
        ViewModelLocationProvider.SetDefaultViewModelFactory((IGameOverViewModel) => container.Resolve(IGameOverViewModel));
        ViewModelLocationProvider.SetDefaultViewModelFactory((IPromotePieceViewModel) => container.Resolve(IPromotePieceViewModel));
        ViewModelLocationProvider.SetDefaultViewModelFactory((IMoveListViewModel) => container.Resolve(IMoveListViewModel));
        ViewModelLocationProvider.SetDefaultViewModelFactory((IImportManager) => container.Resolve(IImportManager));

        var mainWindow = container.Resolve<MainWindow>();
        mainWindow.Show();
    }
}
