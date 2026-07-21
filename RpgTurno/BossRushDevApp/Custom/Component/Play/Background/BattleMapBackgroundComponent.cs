using Domain.Dto.Global;
using Domain.Model.Components.Image;
using Domain.Model.Texture.Sprite.Custom.Ui.Maps;

namespace RpgTurno.Custom.CustomComponents.Play.Background;

public class BattleMapBackgroundComponent : ImageComponent
{
    public BattleMapBackgroundComponent() : base(
        new BattleMapBackgroundSprite(),
        GlobalOptionsDto.WidthSize * 4, 
        GlobalOptionsDto.HeightSize)
    {
    }
}
