using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilArcher;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.EvilArcher;

public class EvilArcherEntity : BaseUnitEntity
{
    public EvilArcherEntity(int level = 1) : base(
        stats: new EvilArcherStats(level),
        skillTree: new EvilArcherSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new EvilArcherIdleSprite());
        Animation.Add(CreatureStateType.Run, new EvilArcherRunSprite());
        Animation.Add(CreatureStateType.Attack, new EvilArcherAttackSprite());

        Icon = new EvilArcherAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        Name = TextConst.EvilArcherUnit;
    }
}
