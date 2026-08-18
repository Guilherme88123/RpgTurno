using Domain.Application.Entity.Units.Base;
using Domain.Application.Entity.Units.Enemy.EvilLancer;
using Domain.Application.Entity.Units.Enemy.EvilWarrior;
using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Unit;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.SpearGoblin;
using Domain.Const.Text;
using Domain.Enum;
using Domain.Enum.Skill;

namespace Domain.Application.Entity.Units.Enemy.SpearGoblin;

public class SpearGoblinEntity : BaseUnitEntity
{
    private SkillCode _executedSkill;

    public SpearGoblinEntity(int level = 1) : base(
        stats: new EvilLancerStats(level),
        skillTree: new EvilWarriorSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new SpearGoblinIdleSprite());
        Animation.Add(CreatureStateType.Run, new SpearGoblinRunSprite());
        Animation.Add((CreatureStateType.Attack, SkillCode.Slash), new SpearGoblinAttackFastSprite());
        Animation.Add((CreatureStateType.Attack, SkillCode.HeavySlash), new SpearGoblinAttackStrongSprite());

        Icon = new SpearGoblinAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 256;
        AnimationSizeY = 256;

        Name = TextConst.SpearGoblinUnit;
    }

    public override void BeforeSkillExecute(UnitSkill skill)
    {
        _executedSkill = skill.SkillCode;
    }

    protected override void UpdateAnimation()
    {
        if (CreatureState == CreatureStateType.Attack)
        {
            Animation.Update((CreatureState, _executedSkill));
            return;
        }

        base.UpdateAnimation();
    }
}
