using Domain.Const.Sound.Effect;
using Domain.Model.Sound.Base;

namespace Domain.Model.Sound;

public class SwordAttackSoundEffect : SoundEffectData
{
    public SwordAttackSoundEffect(): base(SoundEffectConst.SwordAttack)
    {
    }
}
