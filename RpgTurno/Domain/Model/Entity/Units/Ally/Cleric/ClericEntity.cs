using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Animation;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Sprite.Border;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Entity.Units.Ally.Cleric;

public class ClericEntity : BaseUnitEntity
{
    public ClericEntity()
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ClericIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ClericRun);
        var attack = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ClericAttack);

        var spriteBorder = new BorderDefinition(48, 48, 48, 48);

        Animation.AddAnimation(CreatureStateType.Idle, new Animation.AnimationClip(idle, 6, 1, 0.1f, border: spriteBorder));
        Animation.AddAnimation(CreatureStateType.Running, new Animation.AnimationClip(running, 4, 1, 0.1f, border: spriteBorder));
        Animation.AddAnimation(CreatureStateType.Attacking, new Animation.AnimationClip(attack, 11, 1, 0.1f, border: spriteBorder));

        SizeX = 96;
        SizeY = 96;
    }
}
