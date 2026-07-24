using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Ui.Bars;

public class BigBarBaseSprite : ResizableSpriteData
{
    public BigBarBaseSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.BigBarBase),
        ResizableSpriteType.Horizontal,
        fixedHorizontal: 32,
        fixedVertical: 0,
        border: new BorderDefinition(0, 0, 32, 32),
        piecesGap: 64)
    {
    }
}
