using Domain.Const.Sound.Effect;
using Domain.Model.Sound.Base;

namespace Domain.Model.Sound.Attack.Archer;

public class ShootAttackSoundEffect : SoundEffectData
{
    public ShootAttackSoundEffect() : base(SoundEffectConst.ShootAttack)
    {
    }
}
