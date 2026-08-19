using Domain.Application.Effect;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.SupremeWarrior;
using Domain.Const.Text;
using Domain.Enum;
using Domain.Enum.Skill;

namespace Domain.Application.Entity.Units.Enemy.SupremeWarrior;

public class SupremeWarriorEntity : BaseUnitEntity
{
    private int _attackVariation = 1;

    public SupremeWarriorEntity(int level = 1) : base(
        stats: new SupremeWarriorStats(level),
        skillTree: new SupremeWarriorSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new SupremeWarriorIdleSprite());
        Animation.Add(CreatureStateType.Run, new SupremeWarriorRunSprite());
        Animation.Add(CreatureStateType.Guard, new SupremeWarriorGuardSprite());
        Animation.Add((CreatureStateType.Attack, SkillCode.Slash), new SupremeWarriorAttackSprite());
        Animation.Add((CreatureStateType.Attack, SkillCode.HeavySlash), new SupremeWarriorAttackSprite());
        Animation.Add((CreatureStateType.Attack, SkillCode.GuardStance), new SupremeWarriorAttack2Sprite());
        Animation.Add((CreatureStateType.Attack, SkillCode.Cleave), new SupremeWarriorAttackSprite());
        Animation.Add((CreatureStateType.Attack, SkillCode.Execution), new SupremeWarriorAttack2Sprite());

        Icon = new SupremeWarriorAvatarSprite();

        SizeX = 144;
        SizeY = 144;

        AnimationSizeX = 294;
        AnimationSizeY = 294;

        Name = TextConst.SupremeWarriorUnit;
    }

    protected override void UpdateAnimation()
    {
        if (HasGuardStanceEffect() && CreatureState == CreatureStateType.Idle)
        {
            Animation.Update(CreatureStateType.Guard);
            return;
        }

        base.UpdateAnimation();
    }

    private bool HasGuardStanceEffect()
    {
        return Effects.Any(x => x.Effect is GuardStanceEffect);
    }
}
