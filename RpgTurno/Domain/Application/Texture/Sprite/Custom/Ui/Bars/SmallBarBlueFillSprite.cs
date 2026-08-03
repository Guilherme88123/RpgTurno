using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Application.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Texture.Sprite.Custom.Ui.Bars;

public class SmallBarBlueFillSprite : SpriteData
{
    public SmallBarBlueFillSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SmallBarBlueFill),
        border: new BorderDefinition(16, 16, 0, 0))
    {
    }
}
