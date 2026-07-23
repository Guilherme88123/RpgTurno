using Domain.Const.Sound.Effect;
using Domain.Model.Sound.Base;

namespace Domain.Model.Sound.Attack.Archer;

public class PowerShootSoundEffect : SoundEffectData
{
    public PowerShootSoundEffect() : base(SoundEffectConst.PowerShootAttack)
    {
    }
}
