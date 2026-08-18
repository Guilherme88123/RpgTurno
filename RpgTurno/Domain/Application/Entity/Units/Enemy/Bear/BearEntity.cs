using Domain.Application.Entity.Units.Base;
using Domain.Application.Entity.Units.Enemy.EvilWarrior;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.Bear;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.Bear;

public class BearEntity : BaseUnitEntity
{
    public BearEntity(int level = 1) : base(
        stats: new BearStats(level),
        skillTree: new EvilWarriorSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new BearIdleSprite());
        Animation.Add(CreatureStateType.Run, new BearRunSprite());
        Animation.Add(CreatureStateType.Attack, new BearAttackSprite());

        Icon = new BearAvatarSprite();

        SizeX = 128;
        SizeY = 128;

        AnimationSizeX = 256;
        AnimationSizeY = 256;

        Name = TextConst.BearUnit;
    }
}
