using Domain.Model.Effect.Base;
using Domain.Model.Skill.Base.Result;
using Domain.Model.Texture.Sprite;
using Domain.Model.Texture.Sprite.Custom.Sprite;

namespace Domain.Model.Effect;

public class RageEffect : BaseEffect
{
    public override string Name => "Rage";
    public override string Description => "This unit is consumed by rage, adding 40% to his attack damage";
    public override SpriteData Icon => new SwordIconSprite();

    public RageEffect() : base(duration: 3)
    {
    }

    public override void OnAttack(SkillContext context)
    {
        context.Value = (int)(context.Value * 1.4f);
    }
}
