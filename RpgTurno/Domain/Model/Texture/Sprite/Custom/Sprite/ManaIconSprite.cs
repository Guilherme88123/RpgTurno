using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.Custom.Sprite;

public class ManaIconSprite : SpriteData
{
    public ManaIconSprite() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ManaIcon))
    {
    }
}
