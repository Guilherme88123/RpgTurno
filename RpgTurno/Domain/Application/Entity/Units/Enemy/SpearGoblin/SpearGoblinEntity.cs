using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.SpearGoblin;
using Domain.Const.Text;
using Domain.Enum;
using Domain.Enum.Skill;

namespace Domain.Application.Entity.Units.Enemy.SpearGoblin;

public class SpearGoblinEntity : BaseUnitEntity
{
    public SpearGoblinEntity(int level = 1) : base(
        stats: new SpearGoblinStats(level),
        skillTree: new SpearGoblinSkillTree())
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
}
