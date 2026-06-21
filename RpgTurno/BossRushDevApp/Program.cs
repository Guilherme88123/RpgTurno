using Domain.Dto.Global;
using Domain.Interface.Screen;
using Domain.Interface.UiManager;
using Microsoft.Extensions.DependencyInjection;
using RpgTurno.Screen.Play;
using Service.Screen;
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

        services.AddTransient<IScreen, PlayScreen>();

        services.AddTransient<PlayScreen>();

        services.AddTransient<IScreenManager, ScreenManager>();
        services.AddTransient<IUiManagerService, UiManagerService>();

        #endregion

        var provider = services.BuildServiceProvider();
        GlobalVariablesDto.ServiceProvider = provider;

        using var game = new RpgTurno();
        game.Run();
    }
}