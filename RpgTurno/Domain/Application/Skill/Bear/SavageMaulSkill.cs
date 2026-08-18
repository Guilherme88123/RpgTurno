using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Animation;
using Domain.Application.Skill.Base.Data;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Sound.Attack.Warrior;
using Domain.Const.Text;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;

namespace Domain.Application.Skill.Bear;

public class SavageMaulSkill : BaseSkill
{
    public override string Name => TextConst.SavageMaul;
    public override string Description => TextConst.SavageMaulDescription;

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 1.6f;
    public override float PowerMax => 1.9f;

    public override int Cooldown => 3;
    public override int ManaCost => 8;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new SwordAttackSoundEffect(), false);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        var skillResult = ExecuteDefaultSingleTargetAttack(skillData);

        var context = skillResult.Contexts.First();

        var healAmount = (int)(context.Value * 0.3f);

        skillData.Sender.RecieveHeal(healAmount, context.HasCritical);

        return skillResult;
    }
}
