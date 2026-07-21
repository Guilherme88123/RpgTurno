using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Ui.Icons;

public class YellowStarIconSprite : SpriteData
{
    public YellowStarIconSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.YellowStarIcon))
    {
    }
}
