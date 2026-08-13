using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Ally.Cleric;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilPawn;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Ally.Cleric;

public class ClericEntity : BaseUnitEntity
{
    public ClericEntity(int level = 1) : base(
        stats: new ClericStats(level), 
        skillTree: new ClericSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new ClericIdleSprite());
        Animation.Add(CreatureStateType.Run, new ClericRunSprite());
        Animation.Add(CreatureStateType.Attack, new ClericAttackSprite());

        Icon = new ClericAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        Name = TextConst.ClericUnit;
    }
}
