using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Sheep;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Sheep;

public class SheepEntity : BaseUnitEntity
{
    public SheepEntity(int level = 1) : base(stats: new SheepStats(level), skillTree: new SheepSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new SheepIdleSprite());
        Animation.Add(CreatureStateType.Run, new SheepRunSprite());
        Animation.Add(CreatureStateType.Attack, new SheepAttackSprite());

        Icon = new SheepAvatarSprite();

        SizeX = 80;
        SizeY = 80;
        Name = TextConst.SheepUnit;

        AnimationSizeX = 160;
        AnimationSizeY = 160;
    }
}
