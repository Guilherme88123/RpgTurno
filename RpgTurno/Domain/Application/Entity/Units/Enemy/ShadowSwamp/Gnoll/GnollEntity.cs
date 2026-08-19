using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.Gnoll;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.Gnoll;

public class GnollEntity : BaseUnitEntity
{
    public GnollEntity(int level = 1) : base(
        stats: new GnollStats(level),
        skillTree: new GnollSkillTree())
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
