using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.CustomSprites;

public class SmallDustAnimation : AnimationClip
{
    public SmallDustAnimation() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.SmallDustEffect), 8, 1, 0.1f, 1)
    {
    }
}
