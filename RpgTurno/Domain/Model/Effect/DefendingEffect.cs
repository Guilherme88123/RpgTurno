using Domain.Model.Effect.Base;
using Domain.Model.Skill.Base.Result;
using Domain.Model.Texture.Sprite;
using Domain.Model.Texture.Sprite.Custom.Sprite;

namespace Domain.Model.Effect;

public class DefendingEffect : BaseEffect
{
    public override string Name => "Defense";
    public override string Description => "This unit is on a defense posture, reducing damage taken by 30%";
    public override SpriteData Icon => new ShieldIconSprite();

    public DefendingEffect() : base(duration: 1)
    {
    }

    public override void OnReceiveAttack(SkillContext context)
    {
        context.Value = (int)(context.Value * 0.7f);
    }
}
