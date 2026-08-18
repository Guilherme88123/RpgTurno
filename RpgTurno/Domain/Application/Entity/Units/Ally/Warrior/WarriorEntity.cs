using Domain.Application.Effect;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Ally.Warrior;
using Domain.Const.Text;
using Domain.Enum;
using Domain.Enum.Skill;

namespace Domain.Application.Entity.Units.Ally.Warrior;

public class WarriorEntity : BaseUnitEntity
{
    public WarriorEntity(int level = 1) : base(
        stats: new WarriorStats(level), 
        skillTree: new WarriorSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new WarriorIdleSprite());
        Animation.Add(CreatureStateType.Run, new WarriorRunSprite());
        Animation.Add(CreatureStateType.Guard, new WarriorGuardSprite());
        Animation.Add((CreatureStateType.Attack, SkillCode.Slash), new WarriorAttackSprite());
        Animation.Add((CreatureStateType.Attack, SkillCode.HeavySlash), new WarriorAttackSprite());
        Animation.Add((CreatureStateType.Attack, SkillCode.GuardStance), new WarriorAttack2Sprite());
        Animation.Add((CreatureStateType.Attack, SkillCode.Cleave), new WarriorAttackSprite());
        Animation.Add((CreatureStateType.Attack, SkillCode.Execution), new WarriorAttack2Sprite());

        Icon = new WarriorAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        Name = TextConst.WarriorUnit;
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
