using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.HexShaman;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.HexShaman;

public class HexShamanEntity : BaseUnitEntity
{
    public HexShamanEntity(int level = 1) : base(
        stats: new HexShamanStats(level),
        skillTree: new HexShamanSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new HexShamanIdleSprite());
        Animation.Add(CreatureStateType.Run, new HexShamanRunSprite());
        Animation.Add(CreatureStateType.Attack, new HexShamanAttackSprite());

        Icon = new HexShamanAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        Name = TextConst.HexShamanUnit;
    }
}
