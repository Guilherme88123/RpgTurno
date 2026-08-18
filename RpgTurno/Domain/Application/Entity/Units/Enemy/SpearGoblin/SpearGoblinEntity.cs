using Domain.Application.Entity.Units.Base;
using Domain.Application.Entity.Units.Enemy.EvilLancer;
using Domain.Application.Entity.Units.Enemy.EvilWarrior;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.SpearGoblin;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.SpearGoblin;

public class SpearGoblinEntity : BaseUnitEntity
{
    public SpearGoblinEntity(int level = 1) : base(
        stats: new EvilLancerStats(level),
        skillTree: new EvilWarriorSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new SpearGoblinIdleSprite());
        Animation.Add(CreatureStateType.Run, new SpearGoblinRunSprite());
        Animation.Add(CreatureStateType.Attack, new SpearGoblinAttackFastSprite());

        Icon = new SpearGoblinAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 256;
        AnimationSizeY = 256;

        Name = TextConst.SpearGoblinUnit;
    }
}
