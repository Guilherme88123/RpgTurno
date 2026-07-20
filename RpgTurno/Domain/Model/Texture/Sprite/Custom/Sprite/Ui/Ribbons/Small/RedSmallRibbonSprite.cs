using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Ribbons.Small;

public class RedSmallRibbonSprite : ResizableSpriteData
{
    public RedSmallRibbonSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SmallRibbons),
        ResizableSpriteType.Horizontal,
        fixedHorizontal: 64,
        fixedVertical: 0,
        border: new BorderDefinition(128, 448, 0, 0),
        piecesGap: 64)
    {
    }
}
