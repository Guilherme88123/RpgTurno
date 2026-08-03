using Domain.Const.Sound.Effect;
using Domain.Application.Sound.Base;

namespace Domain.Application.Sound.Ui;

public class ButtonClickSoundEffect : SoundEffectData
{
    public ButtonClickSoundEffect() : base(SoundEffectConst.ButtonClick)
    {
    }
}
