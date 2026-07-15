using Domain.Dto.Global;
using Domain.Dto.Session;
using Domain.Interface.Cursor;
using Domain.Interface.Screen;
using Domain.Interface.Transition;
using Domain.Interface.UiManager;
using Microsoft.Extensions.DependencyInjection;
using RpgTurno.Screen.Map;
using RpgTurno.Screen.Menu;
using RpgTurno.Screen.Option;
using RpgTurno.Screen.Play;
using Service.Cursor;
using Service.Screen;
using Service.Transition;
using Service.UiManager;
using System;

namespace RpgTurnoApp;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        var services = new ServiceCollection();

        #region Dependency Injection

        var gameSession = new GameSession();

        services.AddSingleton(gameSession);

        services.AddTransient<IScreen, PlayScreen>();
        services.AddTransient<IScreen, MapScreen>();
        services.AddTransient<IScreen, MenuScreen>();
        services.AddTransient<IScreen, OptionScreen>();

        services.AddTransient<PlayScreen>();
        services.AddTransient<MapScreen>();
        services.AddTransient<MenuScreen>();
        services.AddTransient<OptionScreen>();

        services.AddTransient<IScreenManager, ScreenManager>();
        services.AddTransient<IUiManagerService, UiManagerService>();
        services.AddTransient<ITransitionManager, TransitionManager>();
        services.AddSingleton<ICursorManager, CursorManager>();

        #endregion

        var provider = services.BuildServiceProvider();
        GlobalVariablesDto.ServiceProvider = provider;

        using var game = new RpgTurno();
        game.Run();
    }
}