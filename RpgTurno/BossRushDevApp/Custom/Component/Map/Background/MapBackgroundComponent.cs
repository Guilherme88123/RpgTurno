using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Model.Components.Image;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace RpgTurno.Custom.CustomComponents.Map.Background;

public class MapBackgroundComponent : ImageComponent
{
    public MapBackgroundComponent() : base(GetSprite(), GlobalOptionsDto.WidthSize, GlobalOptionsDto.HeightSize)
    {
    }

    private static SpriteData GetSprite()
    {
        return new SpriteData(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WorldMap));
    }
}
