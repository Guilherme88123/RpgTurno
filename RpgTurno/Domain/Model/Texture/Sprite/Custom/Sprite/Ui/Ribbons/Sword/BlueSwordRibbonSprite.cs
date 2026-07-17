using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Ribbons.Sword;

public class BlueSwordRibbonSprite : ResizableSpriteData
{
    public BlueSwordRibbonSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SwordRibbons),
        ResizableSpriteType.Horizontal,
        fixedHorizontal: 128,
        fixedVertical: 0,
        border: new BorderDefinition(0, 512, 0, 0),
        piecesGap: 64)
    {
    }
}
