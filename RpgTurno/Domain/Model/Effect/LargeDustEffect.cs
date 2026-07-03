using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Effect;

public class LargeDustEffect : AnimationClip
{
    public LargeDustEffect() : base(GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.LargeDustEffect), 10, 1, 0.1f, 1)
    {
    }
}
