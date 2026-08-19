using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.HarpoonShark;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.HarpoonShark;

public class HarpoonSharkEntity : BaseUnitEntity
{
    public HarpoonSharkEntity(int level = 1) : base(
        stats: new HarpoonSharkStats(level),
        skillTree: new HarpoonSharkSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new HarpoonSharkIdleSprite());
        Animation.Add(CreatureStateType.Run, new HarpoonSharkRunSprite());
        Animation.Add(CreatureStateType.Attack, new HarpoonSharkAttackSprite());

        Icon = new HarpoonSharkAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        Name = TextConst.HarpoonSharkUnit;
    }
}
