using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Animation;
using Domain.Application.Skill.Base.Data;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Sound.Attack.Archer;
using Domain.Const.Text;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;

namespace Domain.Application.Skill.BombFish;

public class DestructiveBombSkill : BaseSkill
{
    public override string Name => TextConst.DestructiveBomb;
    public override string Description => TextConst.DestructiveBombDescription;

    public override TargetSkillType TargetType => TargetSkillType.Enemy;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.All;
    public override SkillType Type => SkillType.Attack;

    public override float PowerMin => 0.8f;
    public override float PowerMax => 1.1f;

    public override int Cooldown => 4;
    public override int ManaCost => 10;

    public override SkillAnimation Animation => new SkillAnimation(null, null, new ArrowRainAttackSoundEffect(), true);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        return ExecuteDefaultMultipleTargetAttack(skillData);
    }
}
