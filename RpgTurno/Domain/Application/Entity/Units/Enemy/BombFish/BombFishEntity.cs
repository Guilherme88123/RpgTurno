using Domain.Application.Entity.Units.Base;
using Domain.Application.Entity.Units.Enemy.EvilLancer;
using Domain.Application.Entity.Units.Enemy.EvilWarrior;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.BombFish;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.BombFish;

public class BombFishEntity : BaseUnitEntity
{
    public BombFishEntity(int level = 1) : base(
        stats: new EvilLancerStats(level),
        skillTree: new EvilWarriorSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new BombFishIdleSprite());
        Animation.Add(CreatureStateType.Run, new BombFishRunSprite());
        Animation.Add(CreatureStateType.Attack, new BombFishAttackSprite());

        Icon = new BombFishAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        Name = TextConst.BombFishUnit;
    }
}
