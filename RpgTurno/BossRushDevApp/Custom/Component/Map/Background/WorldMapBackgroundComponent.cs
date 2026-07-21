using Domain.Dto.Global;
using Domain.Model.Components.Image;
using Domain.Model.Texture.Sprite.Custom.Ui.Maps;

namespace RpgTurno.Custom.CustomComponents.Map.Background;

public class WorldMapBackgroundComponent : ImageComponent
{
    public WorldMapBackgroundComponent() : base(new WorldMapBackgroundSprite(), GlobalOptionsDto.WidthSize, GlobalOptionsDto.HeightSize)
    {
    }
}
