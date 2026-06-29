using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Sprite.Border;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Entity.Units.Enemy.Cleric;

public class EnemyClericEntity : BaseUnitEntity
{
    public EnemyClericEntity() : base(stats: new EnemyClericStats(level: 1))
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyClericIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyClericRun);
        var attack = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyClericAttack);

        var spriteBorder = new BorderDefinition(0, 0, 0, 0);

        Animation.Add(CreatureStateType.Idle, new AnimationClip(idle, 6, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Running, new AnimationClip(running, 4, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Attacking, new AnimationClip(attack, 11, 1, 0.1f, border: spriteBorder));

        SizeX = 96;
        SizeY = 96;
        Name = "Evil Cleric";

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        var iconTexture = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyClericAvatar);
        Icon = new SpriteData(iconTexture, new BorderDefinition(16, 16, 16, 16));

        IsRanged = true;
    }
}
