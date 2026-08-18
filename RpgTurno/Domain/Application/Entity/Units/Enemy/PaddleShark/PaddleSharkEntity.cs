using Domain.Application.Entity.Units.Base;
using Domain.Application.Entity.Units.Enemy.EvilLancer;
using Domain.Application.Entity.Units.Enemy.EvilWarrior;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.PaddleShark;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.PaddleShark;

public class PaddleSharkEntity : BaseUnitEntity
{
    public PaddleSharkEntity(int level = 1) : base(
        stats: new EvilLancerStats(level),
        skillTree: new EvilWarriorSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new PaddleSharkIdleSprite());
        Animation.Add(CreatureStateType.Run, new PaddleSharkRunSprite());
        Animation.Add(CreatureStateType.Attack, new PaddleSharkAttackSprite());

        Icon = new PaddleSharkAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        Name = TextConst.PaddleSharkUnit;
    }
}
