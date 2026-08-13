using Domain.Const.Sprite;
using Domain.Const.Text;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Sprite.Border;
using Domain.Application.Texture.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Entity.Units.Enemy.Archer;

public class EnemyArcherEntity : BaseUnitEntity
{
    public EnemyArcherEntity(int level = 1) : base(stats: new EnemyArcherStats(level), skillTree: new EnemyArcherSkillTree())
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyArcherIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyArcherRun);
        var attack = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyArcherAttack);

        var spriteBorder = new BorderDefinition(0, 0, 0, 0);

        Animation.Add(CreatureStateType.Idle, new AnimationClip(idle, 6, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Run, new AnimationClip(running, 4, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Attack, new AnimationClip(attack, 8, 1, 0.1f, border: spriteBorder));

        SizeX = 96;
        SizeY = 96;
        Name = TextConst.EvilArcherUnit;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        var iconTexture = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyArcherAvatar);
        Icon = new SpriteData(iconTexture, new BorderDefinition(16, 16, 16, 16));
    }
}
