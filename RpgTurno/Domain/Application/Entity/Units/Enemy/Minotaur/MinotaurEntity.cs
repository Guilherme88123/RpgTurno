using Domain.Application.Entity.Units.Base;
using Domain.Application.Entity.Units.Enemy.EvilLancer;
using Domain.Application.Entity.Units.Enemy.EvilWarrior;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.Minotaur;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.Minotaur;

public class MinotaurEntity : BaseUnitEntity
{
    public MinotaurEntity(int level = 1) : base(
        stats: new EvilLancerStats(level),
        skillTree: new EvilWarriorSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new MinotaurIdleSprite());
        Animation.Add(CreatureStateType.Run, new MinotaurRunSprite());
        Animation.Add(CreatureStateType.Guard, new MinotaurGuardSprite());
        Animation.Add(CreatureStateType.Attack, new MinotaurAttackSprite());

        Icon = new MinotaurAvatarSprite();

        SizeX = 160;
        SizeY = 160;

        AnimationSizeX = 320;
        AnimationSizeY = 320;

        Name = TextConst.MinotaurUnit;
    }
}
