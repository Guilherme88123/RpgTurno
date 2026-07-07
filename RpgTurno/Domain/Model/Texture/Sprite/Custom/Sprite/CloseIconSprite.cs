using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Sprite;

public class CloseIconSprite : SpriteData
{
    public CloseIconSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.CloseIcon))
    {
    }
}
