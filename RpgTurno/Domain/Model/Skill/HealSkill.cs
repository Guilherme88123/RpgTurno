using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Model.Skill.Base;
using Domain.Model.Skill.Base.Animation;
using Domain.Model.Skill.Base.Data;
using Domain.Model.Skill.Base.Result;
using Domain.Model.Texture.Sprite.CustomSprites;

namespace Domain.Model.Skill;

public class HealSkill : BaseSkill
{
    public override string Name => "Heal";
    public override string Description => "A heal that \nsave your allies";

    public override TargetSkillType TargetType => TargetSkillType.Ally;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Heal;

    public override float PowerMin => 0.7f;
    public override float PowerMax => 1.0f;

    public override int Cooldown => 2;

    public override SkillAnimation Animation => new SkillAnimation(new HealAnimation(), null, true, 0.5f);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        var healAmount = CalculateValue(skillData);

        skillData.Target.RecieveHeal(healAmount);

        return new SkillResult(skillData.Sender, skillData.Target, healAmount);
    }
}
