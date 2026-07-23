using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Ui.Ribbons.Big;

public class BlueBigRibbonSprite : ResizableSpriteData
{
    public BlueBigRibbonSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.BigRibbons),
        ResizableSpriteType.Horizontal,
        fixedHorizontal: 112,
        fixedVertical: 0,
        border: new BorderDefinition(16, 512, 16, 16),
        piecesGap: 64)
    {
    }
}
