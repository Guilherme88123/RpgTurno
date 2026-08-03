using Domain.Const.Sound.Effect;
using Domain.Application.Sound.Base;

namespace Domain.Application.Sound.Attack.Archer;

public class ShootAttackSoundEffect : SoundEffectData
{
    public ShootAttackSoundEffect() : base(SoundEffectConst.ShootAttack)
    {
    }
}
