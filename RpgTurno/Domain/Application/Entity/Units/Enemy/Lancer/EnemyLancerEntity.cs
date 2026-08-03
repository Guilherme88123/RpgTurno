using Domain.Const.Sprite;
using Domain.Const.Text;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Sprite.Border;
using Domain.Application.Texture.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Entity.Units.Enemy.Lancer;

public class EnemyLancerEntity : BaseUnitEntity
{
    public EnemyLancerEntity() : base(stats: new EnemyLancerStats(level: 1), skillTree: new EnemyLancerSkillTree())
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyLancerIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyLancerRun);
        var defending = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyLancerDefence);
        var attack = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyLancerAttack);

        var spriteBorder = new BorderDefinition(0, 0, 0, 0);

        Animation.Add(CreatureStateType.Idle, new AnimationClip(idle, 12, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Running, new AnimationClip(running, 6, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Defending, new AnimationClip(defending, 6, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Attacking, new AnimationClip(attack, 3, 1, 0.1f, border: spriteBorder));

        SizeX = 96;
        SizeY = 96;
        Name = TextConst.EvilLancerUnit;

        AnimationSizeX = 320;
        AnimationSizeY = 320;

        var iconTexture = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyLancerAvatar);
        Icon = new SpriteData(iconTexture, new BorderDefinition(16, 16, 16, 16));
    }
}
