using Domain.Application.Effect;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Ally.Lancer;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Ally.Lancer;

public class LancerEntity : BaseUnitEntity
{
    public override int FeetPadding => 64;

    public LancerEntity(int level = 1) : base(
        stats: new LancerStats(level), 
        skillTree: new LancerSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new LancerIdleSprite());
        Animation.Add(CreatureStateType.Run, new LancerRunSprite());
        Animation.Add(CreatureStateType.Guard, new LancerGuardSprite());
        Animation.Add(CreatureStateType.Attack, new LancerAttackSprite());

        Icon = new LancerAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 320;
        AnimationSizeY = 320;

        Name = TextConst.LancerUnit;
    }

    protected override void UpdateAnimation()
    {
        if (HasLastBastionEffect() && CreatureState == CreatureStateType.Idle)
        {
            Animation.Update(CreatureStateType.Guard);
            return;
        }

        base.UpdateAnimation();
    }

    private bool HasLastBastionEffect()
    {
        return Effects.Any(x => x.Effect is LastBastionEffect);
    }
}
