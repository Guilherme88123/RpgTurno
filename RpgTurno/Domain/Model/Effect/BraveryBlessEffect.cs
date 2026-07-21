using Domain.Model.Effect.Base;
using Domain.Model.Skill.Base.Result;
using Domain.Model.Texture.Sprite;
using Domain.Model.Texture.Sprite.Custom.Ui.Icons;

namespace Domain.Model.Effect;

public class BraveryBlessEffect : BaseEffect
{
    public override string Name => "Bravery Bless";
    public override string Description => "This unit is \nimbued with the \nblessing of bravery, \nadding 40% to his \nattack damage";
    public override SpriteData Icon => new SwordIconSprite();

    public BraveryBlessEffect() : base(duration: 3)
    {
    }

    public override void OnAttack(SkillContext context)
    {
        context.Value = (int)(context.Value * 1.4f);
    }
}
