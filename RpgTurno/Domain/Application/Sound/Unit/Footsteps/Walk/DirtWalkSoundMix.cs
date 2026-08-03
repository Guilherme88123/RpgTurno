using Domain.Application.Sound.Base;

namespace Domain.Application.Sound.Unit.Footsteps.Walk;

public class DirtWalkSoundMix : ManagerSoundMix
{
    public DirtWalkSoundMix()
        : base(
            [
            new DirtWalk1SoundEffect(),
            new DirtWalk2SoundEffect(),
            new DirtWalk3SoundEffect(),
            new DirtWalk4SoundEffect(),
            new DirtWalk5SoundEffect()
            ],
            0.3f,
            isRandomSequence: true)
    {
    }
}