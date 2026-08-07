using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilPawn;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.EvilPawn;

public class EvilPawnEntity : BaseUnitEntity
{
    public EvilPawnEntity(int level = 1) : base(stats: new EvilPawnStats(level), skillTree: new EvilPawnSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new EvilPawnIdleSprite());
        Animation.Add(CreatureStateType.Running, new EvilPawnRunSprite());
        Animation.Add(CreatureStateType.Attacking, new EvilPawnAttackHammerSprite());

        Icon = new EvilPawnAvatarSprite();

        SizeX = 96;
        SizeY = 96;
        Name = TextConst.EvilPawnUnit;

        AnimationSizeX = 192;
        AnimationSizeY = 192;
    }
}
