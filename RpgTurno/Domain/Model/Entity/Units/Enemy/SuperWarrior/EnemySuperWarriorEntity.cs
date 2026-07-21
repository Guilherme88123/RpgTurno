using Domain.Enum;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Texture.Sprite.Custom.Enemy.SuperWarrior;
using Domain.Model.Texture.Sprite.Custom.ParticleFx;

namespace Domain.Model.Entity.Units.Enemy.SuperWarrior;

public class EnemySuperWarriorEntity : BaseUnitEntity
{
    public EnemySuperWarriorEntity() : base(stats: new EnemySuperWarriorStats(level: 20), skillTree: new EnemySuperWarriorSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new EnemySuperWarriorIdleSprite());
        Animation.Add(CreatureStateType.Running, new EnemySuperWarriorRunSprite());
        Animation.Add(CreatureStateType.Defending, new EnemySuperWarriorDefenceSprite());
        Animation.Add(CreatureStateType.Attacking, new EnemySuperWarriorAttackingSprite());

        SizeX = 144;
        SizeY = 144;
        Name = "Supreme Warrior";

        AnimationSizeX = 294;
        AnimationSizeY = 294;

        Icon = new EnemySuperWarriorAvatarSprite();
    }
}
