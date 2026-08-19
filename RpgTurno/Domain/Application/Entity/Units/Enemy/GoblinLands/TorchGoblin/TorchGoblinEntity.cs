using Domain.Application.Entity.Units.Base;
using Domain.Application.Texture.Sprite.Custom.Units.Enemy.TorchGoblin;
using Domain.Const.Text;
using Domain.Enum;

namespace Domain.Application.Entity.Units.Enemy.TorchGoblin;

public class TorchGoblinEntity : BaseUnitEntity
{
    public TorchGoblinEntity(int level = 1) : base(
        stats: new TorchGoblinStats(level),
        skillTree: new TorchGoblinSkillTree())
    {
        Animation.Add(CreatureStateType.Idle, new TorchGoblinIdleSprite());
        Animation.Add(CreatureStateType.Run, new TorchGoblinRunSprite());
        Animation.Add(CreatureStateType.Attack, new TorchGoblinAttackSprite());

        Icon = new TorchGoblinAvatarSprite();

        SizeX = 96;
        SizeY = 96;

        AnimationSizeX = 192;
        AnimationSizeY = 192;

        Name = TextConst.TorchGoblinUnit;
    }
}
