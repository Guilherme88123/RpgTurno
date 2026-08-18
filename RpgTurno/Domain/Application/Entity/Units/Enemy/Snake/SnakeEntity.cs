using Domain.Application.Entity.Units.Base;
using Domain.Application.Entity.Units.Enemy.EvilLancer;
using Domain.Application.Entity.Units.Enemy.EvilWarrior;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.Gnoll;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.Snake;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.Snake;

public class SnakeEntity : BaseUnitEntity
{
    public SnakeEntity(int level = 1) : base(
        stats: new EvilLancerStats(level),
        skillTree: new EvilWarriorSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new SnakeIdleSprite());
        Animation.Add(CreatureStateType.Run, new SnakeRunSprite());
        Animation.Add(CreatureStateType.Attack, new SnakeAttackSprite());

        Icon = new SnakeAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        Name = TextConst.SnakeUnit;
    }
}
