using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.Thief;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.Thief;

public class ThiefEntity : BaseUnitEntity
{
    public ThiefEntity(int level = 1) : base(
        stats: new ThiefStats(level),
        skillTree: new ThiefSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new ThiefIdleSprite());
        Animation.Add(CreatureStateType.Run, new ThiefRunSprite());
        Animation.Add(CreatureStateType.Attack, new ThiefAttackSprite());

        Icon = new ThiefAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        Name = TextConst.ThiefUnit;
    }
}
