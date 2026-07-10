using Domain.Const.Sprite;
using Domain.Dto.Global;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Texture.Sprite.CustomSprites;

public class LargeDustAnimation : AnimationClip
{
    public LargeDustAnimation() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.LargeDustEffect), 10, 1, 0.2f, 1)
    {
    }
}
