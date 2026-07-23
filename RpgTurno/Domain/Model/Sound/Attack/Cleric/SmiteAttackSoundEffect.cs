using Domain.Const.Sound.Effect;
using Domain.Model.Sound.Base;

namespace Domain.Model.Sound.Attack.Cleric;

public class SmiteAttackSoundEffect : SoundEffectData
{
    public SmiteAttackSoundEffect() : base(SoundEffectConst.SmiteAttack)
    {
    }
}
