using Domain.Const.Sprite;
using Domain.Const.Text;
using Domain.Dto.Global;
using Domain.Enum;
using Domain.Application.Entity.Units.Base;
using Domain.Application.Sprite.Border;
using Domain.Application.Texture.Sprite;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Application.Entity.Units.Ally.Archer;

public class ArcherEntity : BaseUnitEntity
{
    public ArcherEntity(int level = 1) : base(stats: new ArcherStats(level), skillTree: new ArcherSkillTree())
    {
        var idle = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ArcherIdle);
        var running = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ArcherRun);
        var attack = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ArcherAttack);

        var spriteBorder = new BorderDefinition(0, 0, 0, 0);

        Animation.Add(CreatureStateType.Idle, new AnimationClip(idle, 6, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Running, new AnimationClip(running, 4, 1, 0.1f, border: spriteBorder));
        Animation.Add(CreatureStateType.Attacking, new AnimationClip(attack, 8, 1, 0.1f, border: spriteBorder));

        SizeX = 96;
        SizeY = 96;
        Name = TextConst.ArcherUnit;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        var iconTexture = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.ArcherAvatar);
        Icon = new SpriteData(iconTexture, new BorderDefinition(16, 16, 16, 16));
    }
}
