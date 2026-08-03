using Domain.Const.Sound.Effect;
using Domain.Application.Sound.Base;

namespace Domain.Application.Sound.Attack.Cleric;

public class HealAttackSoundEffect : SoundEffectData
{
    public HealAttackSoundEffect() : base(SoundEffectConst.HealAttack)
    {
    }
}
