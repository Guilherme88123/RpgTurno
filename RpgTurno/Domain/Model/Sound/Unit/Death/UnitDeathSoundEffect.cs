using Domain.Const.Sound.Effect;
using Domain.Model.Sound.Base;

namespace Domain.Model.Sound.Unit.Death;

public class UnitDeathSoundEffect : SoundEffectData
{
    public UnitDeathSoundEffect() : base(SoundEffectConst.UnitDeath)
    {
    }
}
