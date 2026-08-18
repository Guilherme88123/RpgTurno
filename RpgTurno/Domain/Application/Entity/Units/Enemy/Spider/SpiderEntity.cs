using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.Spider;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.Spider;

public class SpiderEntity : BaseUnitEntity
{
    public SpiderEntity(int level = 1) : base(
        stats: new SpiderStats(level),
        skillTree: new SpiderSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new SpiderIdleSprite());
        Animation.Add(CreatureStateType.Run, new SpiderRunSprite());
        Animation.Add(CreatureStateType.Attack, new SpiderAttackSprite());

        Icon = new SpiderAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        Name = TextConst.SpiderUnit;
    }
}
