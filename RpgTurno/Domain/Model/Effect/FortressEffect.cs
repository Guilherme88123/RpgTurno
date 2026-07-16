using Domain.Model.Effect.Base;
using Domain.Model.Skill.Base.Result;
using Domain.Model.Texture.Sprite;
using Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Icons;

namespace Domain.Model.Effect;

public class FortressEffect : BaseEffect
{
    public override string Name => "Fortress";
    public override string Description => "This unit looks \nlike a fortress, \nreducing damage \ntaken by 30%";
    public override SpriteData Icon => new ShieldIconSprite();

    public FortressEffect() : base(duration: 3)
    {
    }

    public override void OnReceiveAttack(SkillContext context)
    {
        context.Value = (int)(context.Value * 0.7f);
    }
}
