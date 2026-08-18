using Domain.Application.Entity.Units.Base;
using Domain.Application.Entity.Units.Enemy.EvilWarrior;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.Troll;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.Troll;

public class TrollEntity : BaseUnitEntity
{
    public TrollEntity(int level = 1) : base(
        stats: new TrollStats(level),
        skillTree: new EvilWarriorSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new TrollIdleSprite());
        Animation.Add(CreatureStateType.Run, new TrollRunSprite());
        Animation.Add(CreatureStateType.Attack, new TrollAttackSprite());

        Icon = new TrollAvatarSprite();

        SizeX = 192;
        SizeY = 192;

        AnimationSizeX = 384;
        AnimationSizeY = 384;

        Name = TextConst.TrollUnit;
    }
}
