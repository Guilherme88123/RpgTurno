using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Model.Components.Image;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace RpgTurno.CustomComponents.Background;

public class PlayBackgroundComponent : ImageComponent
{
    public PlayBackgroundComponent() : base(GetSprite(), GlobalOptionsDto.WidthSize * 4, GlobalOptionsDto.HeightSize)
    {
    }

    private static SpriteData GetSprite()
    {
        return new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.BasicGrassMap));
    }
}
