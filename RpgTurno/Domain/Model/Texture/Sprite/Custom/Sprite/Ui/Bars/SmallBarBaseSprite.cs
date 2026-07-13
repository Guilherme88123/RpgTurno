using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Sprite;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Bars;

public class SmallBarBaseSprite : ResizableSpriteData
{
    public SmallBarBaseSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SmallBarBase),
        ResizableSpriteType.Horizontal,
        fixedHorizontal: 16,
        fixedVertical: 0,
        border: new BorderDefinition(16, 16, 48, 48), 
        piecesGap: 64)
    {
    }
}
