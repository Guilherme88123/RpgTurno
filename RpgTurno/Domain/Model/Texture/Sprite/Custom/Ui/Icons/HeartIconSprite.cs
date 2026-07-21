using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Ui.Icons;

public class HeartIconSprite : SpriteData
{
    public HeartIconSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.HeartIcon))
    {
    }
}
