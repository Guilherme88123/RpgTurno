using Domain.Dto.Global;
using Microsoft.Xna.Framework.Audio;

namespace Domain.Model.Sound.Base;

public abstract class SoundEffectData
{
    private SoundEffect _sound;

    protected SoundEffectData(SoundEffect sound)
    {
        _sound = sound;
    }

    protected SoundEffectData(string soundName)
    {
        _sound = GlobalVariablesDto.Content.Load<SoundEffect>(soundName);
    }

    public void Play()
    {
        _sound.Play(GlobalOptionsDto.SfxVolumeFloat, 0f, 0f);
    }
}
