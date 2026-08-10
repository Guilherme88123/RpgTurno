using Domain.Application.Effect.Base;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Texture.Sprite;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;
using Domain.Const.Text;

namespace Domain.Application.Effect;

public class BraveryBlessEffect : BaseEffect
{
    public override string Name => TextConst.BraveryBlessEffect;
    public override string Description => TextConst.BraveryBlessEffectDescription;
    public override SpriteData Icon => new SwordIconSprite();

    public BraveryBlessEffect() : base(duration: 3)
    {
    }

    public override void OnAttack(SkillContext context)
    {
        context.Value = (int)(context.Value * 1.4f);
    }
}
