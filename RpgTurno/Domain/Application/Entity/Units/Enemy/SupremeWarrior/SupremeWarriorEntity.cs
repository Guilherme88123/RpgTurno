using Domain.Application.Effect;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.SupremeWarrior;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.SupremeWarrior;

public class SupremeWarriorEntity : BaseUnitEntity
{
    public SupremeWarriorEntity(int level = 1) : base(
        stats: new SupremeWarriorStats(level),
        skillTree: new SupremeWarriorSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new SupremeWarriorIdleSprite());
        Animation.Add(CreatureStateType.Run, new SupremeWarriorRunSprite());
        Animation.Add(CreatureStateType.Guard, new SupremeWarriorGuardSprite());
        Animation.Add(CreatureStateType.Attack, new SupremeWarriorAttackSprite());

        Icon = new SupremeWarriorAvatarSprite();

        SizeX = 144;
        SizeY = 144;

        AnimationSizeX = 294;
        AnimationSizeY = 294;

        Name = TextConst.SupremeWarriorUnit;
    }

    protected override void UpdateAnimation()
    {
        if (HasGuardStanceEffect() && CreatureState == CreatureStateType.Idle)
        {
            Animation.Update(CreatureStateType.Guard);
            return;
        }

        base.UpdateAnimation();
    }

    private bool HasGuardStanceEffect()
    {
        return Effects.Any(x => x.Effect is GuardStanceEffect);
    }
}
