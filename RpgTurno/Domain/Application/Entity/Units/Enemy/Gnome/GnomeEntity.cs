using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.Gnome;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.Gnome;

public class GnomeEntity : BaseUnitEntity
{
    public GnomeEntity(int level = 1) : base(
        stats: new GnomeStats(level),
        skillTree: new GnomeSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new GnomeIdleSprite());
        Animation.Add(CreatureStateType.Run, new GnomeRunSprite());
        Animation.Add(CreatureStateType.Attack, new GnomeAttackSprite());

        Icon = new GnomeAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        Name = TextConst.GnomeUnit;
    }
}
