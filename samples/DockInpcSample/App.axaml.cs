﻿using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using DockInpcSample.Themes;
using DockInpcSample.ViewModels;
using DockInpcSample.Views;

namespace DockInpcSample;

public class App : Application
{
    public static IThemeManager? ThemeManager;

    public override void Initialize()
    {
        ThemeManager = new FluentThemeManager();

        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // DockManager.s_enableSplitToWindow = true;

        var mainWindowViewModel = new MainWindowViewModel();

        switch (ApplicationLifetime)
        {
            case IClassicDesktopStyleApplicationLifetime desktopLifetime:
                {
                    var mainWindow = new MainWindow
                    {
                        DataContext = mainWindowViewModel
                    };

                    mainWindow.Closing += (_, _) =>
                    {
                        mainWindowViewModel.CloseLayout();
                    };

                    desktopLifetime.MainWindow = mainWindow;

                    desktopLifetime.Exit += (_, _) =>
                    {
                        mainWindowViewModel.CloseLayout();
                    };

                    break;
                }
            case ISingleViewApplicationLifetime singleViewLifetime:
                {
                    var mainView = new MainView()
                    {
                        DataContext = mainWindowViewModel
                    };

                    singleViewLifetime.MainView = mainView;

                    break;
                }
        }

        base.OnFrameworkInitializationCompleted();
#if DEBUG
        this.AttachDevTools();
#endif
    }
}
