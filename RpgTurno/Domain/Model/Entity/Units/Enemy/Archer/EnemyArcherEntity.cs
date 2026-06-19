using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Entity.Units.Base;
using Domain.Model.Sprite.Border;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Entity.Units.Enemy.Archer;

public class EnemyArcherEntity : BaseUnitEntity
{
    public EnemyArcherEntity()
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyArcherIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyArcherRun);
        var attack = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyArcherAttack);

        var spriteBorder = new BorderDefinition(48, 48, 48, 48);

        Animation.Add(CreatureStateType.Idle, new AnimationClip(idle, 6, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Running, new AnimationClip(running, 4, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Attacking, new AnimationClip(attack, 8, 1, 0.1f, border: spriteBorder));

        SizeX = 96;
        SizeY = 96;
        Name = "Evil Archer";
    }
}
