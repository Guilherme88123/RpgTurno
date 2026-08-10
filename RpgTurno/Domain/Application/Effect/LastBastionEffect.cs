using Domain.Application.Effect.Base;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Texture.Sprite;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;
using Domain.Const.Text;

namespace Domain.Application.Effect;

public class LastBastionEffect : BaseEffect
{
    public override string Name => TextConst.LastBastionEffect;
    public override string Description => TextConst.LastBastionEffectDescription;
    public override SpriteData Icon => new LastBastionIconSprite();

    public LastBastionEffect() : base(duration: 3)
    {
    }

    public override void OnAttack(SkillContext context)
    {
        context.Value = (int)(context.Value * 1.3f);
    }

    public override void OnReceiveAttack(SkillContext context)
    {
        context.Value = (int)(context.Value * 0.4f);
    }
}
