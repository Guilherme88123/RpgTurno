using Domain.Const.Text;
using Domain.Enum.Skill.Target;
using Domain.Enum.Skill.Type;
using Domain.Application.Effect;
using Domain.Application.Skill.Base;
using Domain.Application.Skill.Base.Animation;
using Domain.Application.Skill.Base.Data;
using Domain.Application.Skill.Base.Result;
using Domain.Application.Sound.Attack.Cleric;
using Domain.Application.Texture.Sprite.Custom.ParticleFx;

namespace Domain.Application.Skill;

public class BlessSkill : BaseSkill
{
    public override string Name => TextConst.Bless;
    public override string Description => TextConst.BlessDescription;

    public override TargetSkillType TargetType => TargetSkillType.Ally;
    public override TargetSkillAmount TargetAmount => TargetSkillAmount.Single;
    public override SkillType Type => SkillType.Stats;

    public override float PowerMin => 0f;
    public override float PowerMax => 0f;

    public override int Cooldown => 3;
    public override int ManaCost => 10;

    public override SkillAnimation Animation => new SkillAnimation(new HealSprite(), null, new BlessAttackSoundEffect(), true);

    public override SkillResult ExecuteSkill(SkillExecuteData skillData)
    {
        skillData.Target.AddEffect(new BraveryBlessEffect());

        return new SkillResult(new SkillContext(skillData.Sender, skillData.Target));
    }
}
