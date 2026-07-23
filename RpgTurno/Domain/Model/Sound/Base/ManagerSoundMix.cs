using Domain.Dto.Global;

namespace Domain.Model.Sound.Base;

public abstract class ManagerSoundMix
{
    public bool IsRandomSequence { get; private set; }

    private Queue<SoundEffectData> _soundsQueue;

    private readonly float SoundDelay;
    private float _currentSoundDelay;
    private bool HasSoundGone => _currentSoundDelay == 0;

    protected ManagerSoundMix(List<SoundEffectData> sounds, float soundTime, bool isRandomSequence = false)
    {
        SoundDelay = soundTime;
        _soundsQueue = new(sounds);
        IsRandomSequence = isRandomSequence;
    }

    public void Update()
    {
        UpdateDelay();

        if (HasSoundGone)
        {
            ResetDelay();
            PlayNextSound();
        }
    }

    public void Reset()
    {
        _currentSoundDelay = 0;
    }

    private void UpdateDelay()
    {
        _currentSoundDelay = Math.Max(_currentSoundDelay - GlobalVariablesDto.DeltaTime, 0);
    }

    private void ResetDelay()
    {
        _currentSoundDelay = SoundDelay;
    }

    private void PlayNextSound()
    {
        var sound = GetNextSound();

        if (sound is null)
            return;

        sound.Play();
    }

    private SoundEffectData GetNextSound()
    {
        if (IsRandomSequence)
            return GetRandomNextSound();
        else
            return GetSequenceNextSound();
    }

    private SoundEffectData GetRandomNextSound()
    {
        return _soundsQueue.ToList().Shuffle().FirstOrDefault();
    }

    private SoundEffectData GetSequenceNextSound()
    {
        var sound = _soundsQueue.Dequeue();
        _soundsQueue.Enqueue(sound);
        return sound;
    }
}
