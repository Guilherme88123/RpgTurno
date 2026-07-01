using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Model.Components.Text;
using Domain.Model.MenuComponents.Frame;
using Domain.Model.Sprite.Border;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RpgTurno.Screen.Map.World.Stage.Node;

namespace RpgTurno.CustomComponents.Map.Stage;

//TODO: Adicionar detalhes sobre o stage
public class MapNodeBannerComponent : FrameComponent
{
    private const int _fixedSlice = 64;
    private const int _sizeX = 256;
    private const int _sizeY = 320;
    private const int _marginY = 100;

    private readonly TextComponent _nameText = new(positionByCenter: true);
    private readonly TextComponent _clearedText = new(positionByCenter: true);

    public MapNodeBannerComponent()
    {
        var paperBannerSprite = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.PaperBanner);
        AnimationManager.Add(true, new AnimationClip([new ResizableSpriteData(paperBannerSprite, ResizableSpriteType.Full, _fixedSlice, _fixedSlice, null, 64)]));

        AddChild(_nameText);
        AddChild(_clearedText);

        Bounds = new Rectangle(0, 0, _sizeX, _sizeY);
    }

    public void SetCurrentMapNode(StageMapNode mapNode)
    {
        SetPositionByMapNode(mapNode);

        _nameText.SetText(mapNode.Name);
        _clearedText.SetText(mapNode.Cleared ? "Cleared" : "Not Cleared");
    }

    private void SetPositionByMapNode(MapNodeData mapNode)
    {
        var positionX = mapNode.Position.X - _sizeX / 2;
        var positionY = mapNode.Position.Y - _sizeY - _marginY;

        SetPosition((int)positionX, (int)positionY);
    }

    public override void SetPosition(int positionX, int positionY)
    {
        var bouncedPositionY = ApplyBounce(positionY);
        base.SetPosition(positionX, bouncedPositionY);

        _nameText.SetPosition(positionX + Bounds.Width / 2, bouncedPositionY + 70);
        _clearedText.SetPosition(positionX + Bounds.Width / 2, bouncedPositionY + 100);
    }

    private int ApplyBounce(int baseValue)
    {
        var bounce = GlobalVariablesDto.GetBounceValue();

        return baseValue - bounce;
    }
}
