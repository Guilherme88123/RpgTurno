using Domain.Const.Sound.Effect;
using Domain.Application.Sound.Base;

namespace Domain.Application.Sound.Unit.Death;

public class UnitDeathSoundEffect : SoundEffectData
{
    public UnitDeathSoundEffect() : base(SoundEffectConst.UnitDeath)
    {
    }
}
