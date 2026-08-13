using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Ally.Archer;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Ally.Archer;

public class ArcherEntity : BaseUnitEntity
{
    public ArcherEntity(int level = 1) : base(
        stats: new ArcherStats(level), 
        skillTree: new ArcherSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new ArcherIdleSprite());
        Animation.Add(CreatureStateType.Run, new ArcherRunSprite());
        Animation.Add(CreatureStateType.Attack, new ArcherAttackSprite());

        Icon = new ArcherAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        Name = TextConst.ArcherUnit;
    }
}
