using Domain.Model.Effect.Base;
using Domain.Model.Skill.Base.Result;
using Domain.Model.Texture.Sprite;
using Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Icons;

namespace Domain.Model.Effect;

public class GuardStanceEffect : BaseEffect
{
    public override string Name => "Guard Stance";
    public override string Description => "This unit is in \na guard stance, \nreducing damage \ntaken by 30%";
    public override SpriteData Icon => new ShieldIconSprite();

    public GuardStanceEffect() : base(duration: 2)
    {
    }

    public override void OnReceiveAttack(SkillContext context)
    {
        context.Value = (int)(context.Value * 0.7f);
    }
}
