using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Sprite.Border;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Entity.Units.Ally.Lancer;

public class LancerEntity : BaseUnitEntity
{
    public LancerEntity() : base(stats: new LancerStats(level: 1))
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.LancerIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.LancerRun);
        var defending = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.LancerDefence);
        var attack = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.LancerAttack);

        var spriteBorder = new BorderDefinition(0, 0, 0, 0);

        Animation.Add(CreatureStateType.Idle, new AnimationClip(idle, 12, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Running, new AnimationClip(running, 6, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Defending, new AnimationClip(defending, 6, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Attacking, new AnimationClip(attack, 3, 1, 0.1f, border: spriteBorder));

        SizeX = 96;
        SizeY = 96;
        Name = "Lancer";

        AnimationSizeX = 320;
        AnimationSizeY = 320;

        var iconTexture = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.LancerAvatar);
        Icon = new SpriteData(iconTexture, new BorderDefinition(16, 16, 16, 16));
    }
}
