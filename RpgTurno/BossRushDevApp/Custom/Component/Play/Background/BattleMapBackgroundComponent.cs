using Domain.Dto.Global;
using Domain.Model.Components.Image;
using Domain.Model.Texture.Sprite.Custom.Sprite;

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
