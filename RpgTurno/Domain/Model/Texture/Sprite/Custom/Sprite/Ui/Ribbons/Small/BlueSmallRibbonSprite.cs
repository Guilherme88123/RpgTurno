using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Ribbons.Small;

public class BlueSmallRibbonSprite : ResizableSpriteData
{
    public BlueSmallRibbonSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SmallRibbons),
        ResizableSpriteType.Horizontal,
        fixedHorizontal: 64,
        fixedVertical: 0,
        border: new BorderDefinition(0, 576, 0, 0),
        piecesGap: 64)
    {
    }
}
