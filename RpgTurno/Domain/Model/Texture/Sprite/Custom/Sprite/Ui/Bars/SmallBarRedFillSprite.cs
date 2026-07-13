using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Bars;

public class SmallBarRedFillSprite : SpriteData
{
    public SmallBarRedFillSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SmallBarRedFill),
        border: new BorderDefinition(16, 16, 0, 0))
    {
    }
}
