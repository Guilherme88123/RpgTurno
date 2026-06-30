using Domain.Const.Screen;
using Domain.Model.Components.Base;
using RpgTurno.CustomComponents.Map.Background;
using RpgTurnoApp.Screen.Base;
using System.Collections.Generic;

namespace RpgTurno.Screen.Map;

public class MapScreen : BaseScreen
{
    public override string ScreenCode => ScreenConst.MapScreen;

    private MapBackgroundComponent _backgroundImageComponent;

    #region Initialize

    protected override List<BaseComponent> InitializeComponents()
    {
        _backgroundImageComponent = new();

        return new()
        {
            _backgroundImageComponent,
        };
    }

    #endregion
}
