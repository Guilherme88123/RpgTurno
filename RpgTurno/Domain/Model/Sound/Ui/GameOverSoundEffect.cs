using Domain.Const.Sound.Effect;
using Domain.Model.Sound.Base;

namespace Domain.Model.Sound.Ui;

public class GameOverSoundEffect : SoundEffectData
{
    public GameOverSoundEffect() : base(SoundEffectConst.GameOver)
    {
    }
}
