using Domain.Application.Effect;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Sprite.Border;
using Domain.Application.Texture.Sprite;
using Domain.Const.Sprite;
using Domain.Const.Text;
using Domain.Dto.Global;
using Domain.Enum;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Entity.Units.Enemy.Warrior;

public class EnemyWarriorEntity : BaseUnitEntity
{
    public EnemyWarriorEntity(int level = 1) : base(stats: new EnemyWarriorStats(level), skillTree: new EnemyWarriorSkillTree())
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyWarriorIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyWarriorRun);
        var defending = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyWarriorGuard);
        var attack = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyWarriorAttack);

        var spriteBorder = new BorderDefinition(0, 0, 0, 0);

        Animation.Add(CreatureStateType.Idle, new AnimationClip(idle, 8, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Run, new AnimationClip(running, 6, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Guard, new AnimationClip(defending, 6, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Attack, new AnimationClip(attack, 4, 1, 0.1f, border: spriteBorder));

        SizeX = 96;
        SizeY = 96;
        Name = TextConst.EvilWarriorUnit;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        var iconTexture = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyWarriorAvatar);
        Icon = new SpriteData(iconTexture, new BorderDefinition(16, 16, 16, 16));
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
