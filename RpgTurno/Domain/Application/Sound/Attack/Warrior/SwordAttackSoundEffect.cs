using Domain.Const.Sound.Effect;
using Domain.Application.Sound.Base;

namespace Domain.Application.Sound.Attack.Warrior;

public class SwordAttackSoundEffect : SoundEffectData
{
    public SwordAttackSoundEffect(): base(SoundEffectConst.SwordAttack)
    {
    }
}
