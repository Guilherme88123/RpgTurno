using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.EvilCleric;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.EvilCleric;

public class EvilClericEntity : BaseUnitEntity
{
    public EvilClericEntity(int level = 1) : base(
        stats: new EvilClericStats(level),
        skillTree: new EvilClericSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new EvilClericIdleSprite());
        Animation.Add(CreatureStateType.Run, new EvilClericRunSprite());
        Animation.Add(CreatureStateType.Attack, new EvilClericAttackSprite());

        Icon = new EvilClericAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        Name = TextConst.EvilClericUnit;
    }
}
