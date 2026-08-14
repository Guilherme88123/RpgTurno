using Domain.Application.Effect;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilLancer;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.EvilLancer;

public class EvilLancerEntity : BaseUnitEntity
{
    public override int FeetPadding => 64;

    public EvilLancerEntity(int level = 1) : base(
        stats: new EvilLancerStats(level),
        skillTree: new EvilLancerSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new EvilLancerIdleSprite());
        Animation.Add(CreatureStateType.Run, new EvilLancerRunSprite());
        Animation.Add(CreatureStateType.Guard, new EvilLancerGuardSprite());
        Animation.Add(CreatureStateType.Attack, new EvilLancerAttackSprite());

        Icon = new EvilLancerAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 320;
        AnimationSizeY = 320;

        Name = TextConst.EvilLancerUnit;
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
