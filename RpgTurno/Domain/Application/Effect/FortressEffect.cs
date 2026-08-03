using Domain.Application.Effect.Base;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Texture.Sprite;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;

namespace Domain.Application.Effect;

public class FortressEffect : BaseEffect
{
    public override string Name => "Fortress";
    public override string Description => "This unit looks \nlike a fortress, \nreducing damage \ntaken by 30%";
    public override SpriteData Icon => new FortressIconSprite();

    public FortressEffect() : base(duration: 3)
    {
    }

    public override void OnReceiveAttack(SkillContext context)
    {
        context.Value = (int)(context.Value * 0.7f);
    }
}
