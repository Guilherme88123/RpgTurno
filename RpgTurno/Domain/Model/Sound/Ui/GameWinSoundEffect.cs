using Domain.Const.Sound.Effect;
using Domain.Model.Sound.Base;

namespace Domain.Model.Sound.Ui;

public class GameWinSoundEffect : SoundEffectData
{
    public GameWinSoundEffect() : base(SoundEffectConst.GameWin)
    {
    }
}
