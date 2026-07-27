using Domain.Dto.Global;
using Domain.Model.Components.Image;
using Domain.Model.MenuComponents.Frame;
using Domain.Model.Texture.Sprite.Custom.Terrain.Trees;
using Domain.Model.Texture.Sprite.Custom.Ui.Maps;

namespace RpgTurno.Custom.CustomComponents.Map.Background;

public class WorldMapBackgroundComponent : FrameComponent
{
    private readonly ImageComponent _image = new(new WorldMapBackgroundSprite(), GlobalOptionsDto.WidthSize, GlobalOptionsDto.HeightSize);

    private static Tree1Sprite _treeSprite = new();
    private readonly ImageComponent _tree = new(_treeSprite, (int)(_treeSprite.Width / 1.5f), (int)(_treeSprite.Height / 1.5f));

    public WorldMapBackgroundComponent()
    {
        AddChild(_image);
        AddChild(_tree);
    }
}
