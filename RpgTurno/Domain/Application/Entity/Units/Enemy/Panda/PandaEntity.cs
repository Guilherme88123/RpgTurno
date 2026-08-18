using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.Panda;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.Panda;

public class PandaEntity : BaseUnitEntity
{
    public PandaEntity(int level = 1) : base(
        stats: new PandaStats(level),
        skillTree: new PandaSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new PandaIdleSprite());
        Animation.Add(CreatureStateType.Run, new PandaRunSprite());
        Animation.Add(CreatureStateType.Attack, new PandaAttackSprite());

        Icon = new PandaAvatarSprite();

        SizeX = 128;
        SizeY = 128;

        AnimationSizeX = 256;
        AnimationSizeY = 256;

        Name = TextConst.PandaUnit;
    }
}
