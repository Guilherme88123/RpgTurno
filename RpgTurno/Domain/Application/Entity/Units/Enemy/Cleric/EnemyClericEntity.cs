using Domain.Const.Sprite;
using Domain.Const.Text;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Sprite.Border;
using Domain.Application.Texture.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Entity.Units.Enemy.Cleric;

public class EnemyClericEntity : BaseUnitEntity
{
    public EnemyClericEntity(int level = 1) : base(stats: new EnemyClericStats(level), skillTree: new EnemyClericSkillTree())
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyClericIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyClericRun);
        var attack = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyClericAttack);

        var spriteBorder = new BorderDefinition(0, 0, 0, 0);

        Animation.Add(CreatureStateType.Idle, new AnimationClip(idle, 6, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Run, new AnimationClip(running, 4, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Attack, new AnimationClip(attack, 11, 1, 0.1f, border: spriteBorder));

        SizeX = 96;
        SizeY = 96;
        Name = TextConst.EvilClericUnit;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        var iconTexture = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.EnemyClericAvatar);
        Icon = new SpriteData(iconTexture, new BorderDefinition(16, 16, 16, 16));
    }
}
