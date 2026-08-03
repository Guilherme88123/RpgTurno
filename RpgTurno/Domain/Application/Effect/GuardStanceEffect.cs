using Domain.Application.Effect.Base;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Texture.Sprite;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;

namespace Domain.Application.Effect;

public class GuardStanceEffect : BaseEffect
{
    public override string Name => "Guard Stance";
    public override string Description => "This unit is in \na guard stance, \nreducing damage \ntaken by 45%";
    public override SpriteData Icon => new ShieldIconSprite();

    public GuardStanceEffect() : base(duration: 2)
    {
    }

    public override void OnReceiveAttack(SkillContext context)
    {
        context.Value = (int)(context.Value * 0.65f);
    }
}
