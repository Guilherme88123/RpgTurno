using Domain.Enum.Skill;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Factory;
using Domain.Model.Skill.Base.Result;

namespace Domain.Model.Skill.Base.Unit;

public class UnitSkill
{
    public SkillCode SkillCode { get; private set; }
    private BaseSkill _skill { get; set; }

    public string Name => _skill.Name;
    public string Description => _skill.Description;
    public TargetSkillType TargetType => _skill.TargetType;
    public TargetSkillAmount TargetAmount => _skill.TargetAmount;
    public SkillType Type => _skill.Type;

    public SkillAnimation Animation => _skill.Animation;

    public int CurrentCooldown { get; private set; }

    public UnitSkill(SkillCode skillCode)
    {
        SkillCode = skillCode;
        _skill = SkillFactory.Create(skillCode);
    }

    public bool CanUse()
    {
        return CurrentCooldown <= 0;
    }

    public SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        if (!CanUse())
            throw new InvalidOperationException("Skill is on cooldown.");

        CurrentCooldown = _skill.Cooldown;

        return _skill.ExecuteSkill(skillData);
    }

    public void TickCooldown()
    {
        if (CurrentCooldown > 0)
            CurrentCooldown--;
    }
}
