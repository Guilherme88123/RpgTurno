using Domain.Application.Effect;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilWarrior;
using Domain.Const.Text;
using Domain.Enum;
using Domain.Enum.Skill;

namespace Domain.Application.Entity.Units.Enemy.EvilWarrior;

public class EvilWarriorEntity : BaseUnitEntity
{
    private int _attackVariation = 1;

    public EvilWarriorEntity(int level = 1) : base(
        stats: new EvilWarriorStats(level), 
        skillTree: new EvilWarriorSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new EvilWarriorIdleSprite());
        Animation.Add(CreatureStateType.Run, new EvilWarriorRunSprite());
        Animation.Add(CreatureStateType.Guard, new EvilWarriorGuardSprite());
        Animation.Add((CreatureStateType.Attack, SkillCode.Slash), new EvilWarriorAttackSprite());
        Animation.Add((CreatureStateType.Attack, SkillCode.HeavySlash), new EvilWarriorAttackSprite());
        Animation.Add((CreatureStateType.Attack, SkillCode.GuardStance), new EvilWarriorAttack2Sprite());
        Animation.Add((CreatureStateType.Attack, SkillCode.Cleave), new EvilWarriorAttackSprite());
        Animation.Add((CreatureStateType.Attack, SkillCode.Execution), new EvilWarriorAttack2Sprite());

        Icon = new EvilWarriorAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        Name = TextConst.EvilWarriorUnit;
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
