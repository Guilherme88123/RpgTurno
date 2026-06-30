using Domain.Const.Screen;
using Domain.Dto.Global;
using Domain.Model.Components.Base;
using Microsoft.Xna.Framework;
using RpgTurno.CustomComponents.Map.Background;
using RpgTurno.Screen.Map.World;
using RpgTurnoApp.Screen.Base;
using System.Collections.Generic;

namespace RpgTurno.Screen.Map;

public class MapScreen : BaseScreen
{
    public override string ScreenCode => ScreenConst.MapScreen;

    private WorldManager _worldManager;

    private MapBackgroundComponent _backgroundImageComponent;

    #region Initialize

    protected override List<BaseComponent> InitializeComponents()
    {
        _worldManager = new();
        _worldManager.Initialize();

        _backgroundImageComponent = new();

        return new();
    }

    #endregion

    #region Update

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _worldManager.Update();
    }

    #endregion

    #region Draw

    public override void Draw()
    {
        DrawBackground();
        DrawPlayer();

        base.Draw();
    }

    public void DrawPlayer()
    {
        _worldManager.Player.Draw();
    }

    private void DrawBackground()
    {
        _backgroundImageComponent.Draw(GlobalVariablesDto.SpriteBatchBackground);
    }

    #endregion
}
