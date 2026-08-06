using Domain.Application.Effect;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Sprite.Border;
using Domain.Application.Texture.Sprite;
using Domain.Application.Texture.Sprite.Custom.Units.Ally.Warrior;
using Domain.Const.Sprite;
using Domain.Const.Text;
using Domain.Dto.Global;
using Domain.Enum;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Entity.Units.Ally.Warrior;

public class WarriorEntity : BaseUnitEntity
{
    public WarriorEntity(int level = 1) : base(stats: new WarriorStats(level), skillTree: new WarriorSkillTree())
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WarriorIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WarriorRun);
        var attack = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WarriorAttack);

        var spriteBorder = new BorderDefinition(0, 0, 0, 0);

        Animation.Add(CreatureStateType.Idle, new AnimationClip(idle, 8, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Running, new AnimationClip(running, 6, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Defending, new WarriorGuardSprite());
        Animation.Add(CreatureStateType.Attacking, new AnimationClip(attack, 4, 1, 0.1f, border: spriteBorder));

        SizeX = 96;
        SizeY = 96;
        Name = TextConst.WarriorUnit;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        var iconTexture = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.WarriorAvatar);
        Icon = new SpriteData(iconTexture, new BorderDefinition(16, 16, 16, 16));
    }

    protected override void UpdateAnimation()
    {
        if (HasGuardStanceEffect() && CreatureState == CreatureStateType.Idle)
        {
            Animation.Update(CreatureStateType.Defending);
            return;
        }

        base.UpdateAnimation();
    }

    private bool HasGuardStanceEffect()
    {
        return Effects.Any(x => x.Effect is GuardStanceEffect);
    }
}
