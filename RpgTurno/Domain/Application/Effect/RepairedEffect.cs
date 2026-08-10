using Domain.Application.Effect.Base;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Texture.Sprite;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;

namespace Domain.Application.Effect;

public class RepairedEffect : BaseEffect
{
    public override string Name => "Repaired";
    public override string Description => "This unit has \nbeen repaired; its \nattack is 15% stronger, \nand it is taking \n10% less damage";

    public override SpriteData Icon => new HammerIconSprite();

    public RepairedEffect() : base(duration: 3)
    {
    }

    public override void OnAttack(SkillContext context)
    {
        context.Value = (int)(context.Value * 1.15f);
    }

    public override void OnReceiveAttack(SkillContext context)
    {
        context.Value = (int)(context.Value * 0.9f);
    }
}
