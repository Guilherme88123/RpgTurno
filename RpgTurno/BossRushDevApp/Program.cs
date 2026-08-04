using Data.Context;
using Domain.Const.Database;
using Domain.Dto.Global;
using Domain.Dto.Session;
using Domain.Interface.Cursor;
using Domain.Interface.Language;
using Domain.Interface.Repositories.Save;
using Domain.Interface.Repositories.Settings;
using Domain.Interface.Repositories.Stage;
using Domain.Interface.Repositories.Unit;
using Domain.Interface.Screen;
using Domain.Interface.Transition;
using Domain.Interface.UiManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RpgTurno.Language;
using RpgTurno.Screen.Map;
using RpgTurno.Screen.Map.World.Stage;
using RpgTurno.Screen.Menu;
using RpgTurno.Screen.Option;
using RpgTurno.Screen.Play;
using RpgTurno.Screen.Save;
using Service.Cursor;
using Service.Repositories.Save;
using Service.Repositories.Settings;
using Service.Repositories.Stage;
using Service.Repositories.Unit;
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

        #region Entity Framework Core Configuration

        #region Database Context

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite($"Data Source={DatabaseConst.Filename}");
        });
        services.AddTransient<DbContext, AppDbContext>();

        #endregion

        #region Repositories

        services.AddTransient<ISaveService, SaveService>();
        services.AddTransient<ISettingsService, SettingsService>();
        services.AddTransient<IStageService, StageService>();
        services.AddTransient<IUnitService, UnitService>();

        #endregion

        #endregion

        var gameSession = new GameSession();

        services.AddSingleton(gameSession);

        services.AddTransient<IScreen, PlayScreen>();
        services.AddTransient<IScreen, MapScreen>();
        services.AddTransient<IScreen, MenuScreen>();
        services.AddTransient<IScreen, OptionScreen>();
        services.AddTransient<IScreen, SaveScreen>();

        services.AddTransient<PlayScreen>();
        services.AddTransient<MapScreen>();
        services.AddTransient<MenuScreen>();
        services.AddTransient<OptionScreen>();
        services.AddTransient<SaveScreen>();

        services.AddTransient<IScreenManager, ScreenManager>();
        services.AddTransient<IUiManagerService, UiManagerService>();
        services.AddTransient<ITransitionManager, TransitionManager>();
        services.AddSingleton<ICursorManager, CursorManager>();
        services.AddSingleton<ILanguageService, LanguageService>();

        #endregion

        var provider = services.BuildServiceProvider();

        GlobalVariablesDto.ServiceProvider = provider;

        using (var scope = provider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            db.Database.Migrate();
        }

        using var game = new RpgTurno();
        game.Run();
    }
}