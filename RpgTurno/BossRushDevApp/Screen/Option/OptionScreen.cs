using Domain.Const.Screen;
using Domain.Dto.Global;
using Domain.Model.Components.Base;
using RpgTurno.Custom.Component.Option.Banner;
using RpgTurnoApp.Screen.Base;
using System.Collections.Generic;

namespace RpgTurno.Screen.Option;

public class OptionScreen : BaseScreen
{
    public override string ScreenCode => ScreenConst.OptionScreen;

    #region Initialize

    protected override List<BaseComponent> InitializeComponents()
    {
        var banner = new OptionsBannerComponent();
        banner.SetPosition(
            GlobalOptionsDto.WidthSize / 2 - banner.Bounds.Width / 2, 
            GlobalOptionsDto.HeightSize / 2 - banner.Bounds.Height / 2);

        return new()
        {
            banner,
        };
    }

    #endregion
}
