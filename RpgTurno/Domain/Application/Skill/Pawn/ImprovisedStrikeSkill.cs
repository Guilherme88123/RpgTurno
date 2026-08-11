using Domain.Application.Entity.Units.Enemy.EvilPawn;
using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Animation;
using Domain.Application.Skill.Base.Data;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Sound.Attack.Warrior;
using Domain.Const.Text;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Enum.Unit.Pawn;

namespace Domain.Application.Skill.Pawn;

public class ImprovisedStrikeSkill : BaseSkill
{
    public override string Name => TextConst.ImprovisedStrike;
    public override string Description => TextConst.ImprovisedStrikeDescription;

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 1.15f;
    public override float PowerMax => 1.4f;

    public override int Cooldown => 0;
    public override int ManaCost => 0;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new ExecutionSwordAttackSoundEffect(), false, 0.3f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        return ExecuteDefaultSingleTargetAttack(skillData);
    }

    public override void BeforeExecute(SkillExecuteData skillData)
    {
        if (skillData.Sender is EvilPawnEntity pawn)
            ApplyPawnAnimation(pawn);
    }

    private void ApplyPawnAnimation(EvilPawnEntity pawn)
    {
        pawn.SetUsedTool(GetRngTool());
    }

    private static PawnToolType GetRngTool()
    {
        return (PawnToolType)Random.Shared.Next(1, 4);
    }
}
