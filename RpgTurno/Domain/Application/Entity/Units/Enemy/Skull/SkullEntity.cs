using Domain.Application.Entity.Units.Base;
using Domain.Application.Entity.Units.Enemy.EvilWarrior;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.Skull;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.Skull;

public class SkullEntity : BaseUnitEntity
{
    public SkullEntity(int level = 1) : base(
        stats: new SkullStats(level),
        skillTree: new EvilWarriorSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new SkullIdleSprite());
        Animation.Add(CreatureStateType.Run, new SkullRunSprite());
        Animation.Add(CreatureStateType.Attack, new SkullAttackSprite());

        Icon = new SkullAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        Name = TextConst.SkullUnit;
    }
}
