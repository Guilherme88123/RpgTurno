using Domain.Enum.Skill;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Factory;
using Domain.Model.Skill.Base.Result;

namespace Domain.Model.Skill.Base.Unit;

public class UnitSkill
{
    public SkillCode SkillCode { get; private set; }

    public BaseSkill Definition { get; private set; }

    public SkillAnimation Animation => Definition.Animation;

    public int CurrentCooldown { get; private set; }

    public UnitSkill(SkillCode skillCode)
    {
        SkillCode = skillCode;
        Definition = SkillFactory.Create(skillCode);
    }

    public bool CanUse()
    {
        return CurrentCooldown <= 0;
    }

    public SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        if (!CanUse())
            throw new InvalidOperationException("Skill is on cooldown.");

        CurrentCooldown = Definition.Cooldown;

        return Definition.ExecuteSkill(skillData);
    }

    public void TickCooldown()
    {
        if (CurrentCooldown > 0)
            CurrentCooldown--;
    }
}
