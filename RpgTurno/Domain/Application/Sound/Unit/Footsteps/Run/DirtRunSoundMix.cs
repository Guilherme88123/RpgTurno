using Domain.Application.Sound.Base;

namespace Domain.Application.Sound.Unit.Footsteps.Run;

public class DirtRunSoundMix : ManagerSoundMix
{
    public DirtRunSoundMix() 
        : base(
            [
            new DirtRun1SoundEffect(), 
            new DirtRun2SoundEffect(), 
            new DirtRun3SoundEffect(), 
            new DirtRun4SoundEffect(), 
            new DirtRun5SoundEffect()
            ], 
            0.3f, 
            isRandomSequence: true)
    {
    }
}
