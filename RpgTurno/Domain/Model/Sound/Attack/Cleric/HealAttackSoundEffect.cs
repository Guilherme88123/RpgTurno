using Domain.Const.Sound.Effect;
using Domain.Model.Sound.Base;

namespace Domain.Model.Sound.Attack.Cleric;

public class HealAttackSoundEffect : SoundEffectData
{
    public HealAttackSoundEffect() : base(SoundEffectConst.HealAttack)
    {
    }
}
