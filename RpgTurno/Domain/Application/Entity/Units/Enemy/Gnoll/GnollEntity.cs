using Domain.Application.Entity.Units.Base;
using Domain.Application.Entity.Units.Enemy.EvilLancer;
using Domain.Application.Entity.Units.Enemy.EvilWarrior;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.Bear;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.Gnoll;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.Gnoll;

public class GnollEntity : BaseUnitEntity
{
    public GnollEntity(int level = 1) : base(
        stats: new EvilLancerStats(level),
        skillTree: new EvilWarriorSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new GnollIdleSprite());
        Animation.Add(CreatureStateType.Run, new GnollRunSprite());
        Animation.Add(CreatureStateType.Attack, new GnollAttackSprite());

        Icon = new GnollAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        Name = TextConst.GnollUnit;
    }
}
