using Domain.Const.Sound.Effect;
using Domain.Application.Sound.Base;

namespace Domain.Application.Sound.Ui;

public class GameOverSoundEffect : SoundEffectData
{
    public GameOverSoundEffect() : base(SoundEffectConst.GameOver)
    {
    }
}
