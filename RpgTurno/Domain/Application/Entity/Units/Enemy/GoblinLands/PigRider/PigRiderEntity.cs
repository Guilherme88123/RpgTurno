using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.PigRider;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.SpearGoblin;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.PigRider;

public class PigRiderEntity : BaseUnitEntity
{
    public PigRiderEntity(int level = 1) : base(
        stats: new PigRiderStats(level),
        skillTree: new PigRiderSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new PigRiderIdleSprite());
        Animation.Add(CreatureStateType.Run, new PigRiderRunSprite());
        Animation.Add(CreatureStateType.Attack, new PigRiderAttackSprite());

        Icon = new SpearGoblinAvatarSprite();

        SizeX = 128;
        SizeY = 128;

        AnimationSizeX = 256;
        AnimationSizeY = 256;

        Name = TextConst.PigRiderUnit;
    }
}
