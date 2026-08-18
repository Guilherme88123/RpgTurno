using Domain.Application.Entity.Units.Base;
using Domain.Application.Entity.Units.Enemy.EvilWarrior;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.Lizard;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.Lizard;

public class LizardEntity : BaseUnitEntity
{
    public LizardEntity(int level = 1) : base(
        stats: new LizardStats(level),
        skillTree: new EvilWarriorSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new LizardIdleSprite());
        Animation.Add(CreatureStateType.Run, new LizardRunSprite());
        Animation.Add(CreatureStateType.Attack, new LizardAttackSprite());

        Icon = new LizardAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        Name = TextConst.LizardUnit;
    }
}
