using Domain.Enum.Skill;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;

namespace Domain.Model.Skill.Base;

public abstract class BaseSkill
{
    public abstract string Name { get; }
    public abstract string Description { get; }

    public abstract TargetSkillType TargetType { get; }
    public abstract TargetSkillAmount TargetAmount { get; }

    public virtual float PowerMin => 0;
    public virtual float PowerMax => 0;

    public abstract int Cooldown { get; }

    public abstract SkillResult ExecuteSkill(SkillExecuteData skillData);

    public abstract SkillAnimation GetSkillAnimation();
}
