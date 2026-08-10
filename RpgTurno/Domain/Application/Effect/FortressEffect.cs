using Domain.Application.Effect.Base;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Texture.Sprite;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;
using Domain.Const.Text;

namespace Domain.Application.Effect;

public class FortressEffect : BaseEffect
{
    public override string Name => TextConst.FortressEffect;
    public override string Description => TextConst.FortressEffectDescription;
    public override SpriteData Icon => new FortressIconSprite();

    public FortressEffect() : base(duration: 3)
    {
    }

    public override void OnReceiveAttack(SkillContext context)
    {
        context.Value = (int)(context.Value * 0.7f);
    }
}
