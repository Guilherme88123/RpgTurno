using Domain.Const.Sound.Effect;
using Domain.Application.Sound.Base;

namespace Domain.Application.Sound.Ui;

public class GameWinSoundEffect : SoundEffectData
{
    public GameWinSoundEffect() : base(SoundEffectConst.GameWin)
    {
    }
}
