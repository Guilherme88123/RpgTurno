using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.Turtle;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.Turtle;

public class TurtleEntity : BaseUnitEntity
{
    public TurtleEntity(int level = 1) : base(
        stats: new TurtleStats(level),
        skillTree: new TurtleSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new TurtleIdleSprite());
        Animation.Add(CreatureStateType.Run, new TurtleRunSprite());
        Animation.Add(CreatureStateType.Attack, new TurtleAttackSprite());

        Icon = new TurtleAvatarSprite();

        SizeX = 160;
        SizeY = 160;

        AnimationSizeX = 320;
        AnimationSizeY = 320;

        Name = TextConst.TurtleUnit;
    }
}
