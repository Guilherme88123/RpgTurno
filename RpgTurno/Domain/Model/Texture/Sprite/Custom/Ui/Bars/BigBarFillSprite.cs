using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Ui.Bars;

public class BigBarFillSprite : SpriteData
{
    public BigBarFillSprite() : base(
        GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.BigBarFill),
        border: new BorderDefinition(0, 0, 0, 0))
    {
    }
}
