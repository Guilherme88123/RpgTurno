using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Animation;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;

namespace Domain.Model.Entity.Units.Ally.Lancer;

public class LancerEntity : BaseUnitEntity
{
    public LancerEntity()
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.LancerIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.LancerRun);
        var defending = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.LancerDefence);
        var attack = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.LancerAttack);

        var spriteBorder = new BorderDefinition(48, 112, 112, 112);

        Animation.AddAnimation(CreatureStateType.Idle, new AnimationClip(idle, 12, 1, 0.1f, border: spriteBorder));
        Animation.AddAnimation(CreatureStateType.Running, new AnimationClip(running, 6, 1, 0.1f, border: spriteBorder));
        Animation.AddAnimation(CreatureStateType.Defending, new AnimationClip(defending, 6, 1, 0.1f, border: spriteBorder));
        Animation.AddAnimation(CreatureStateType.Attacking, new AnimationClip(attack, 3, 1, 0.1f, border: spriteBorder));

        SizeX = 96;
        SizeY = 224;
    }
}
