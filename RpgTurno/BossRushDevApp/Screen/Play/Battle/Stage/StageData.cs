using RpgTurno.Screen.Play.Battle.Wave;
using System.Collections.Generic;

namespace RpgTurno.Screen.Play.Battle.Stage;

public class StageData
{
    private int _currentWaveIndex;

    public List<WaveData> Waves { get; }

    public StageData(List<WaveData> waves)
    {
        Waves = waves;
    }

    public WaveData GetCurrentWave()
    {
        return Waves[_currentWaveIndex];
    }

    public bool HasNextWave()
    {
        return GetCurrentWaveIndex() < Waves.Count;
    }

    public void NextWave()
    {
        if (HasNextWave())
        {
            _currentWaveIndex++;
        }
    }

    public bool IsFinished()
    {
        return !HasNextWave() && GetCurrentWave().IsCompleted();
    }

    public int GetCurrentWaveIndex()
    {
        return _currentWaveIndex + 1;
    }

    public int GetCountWaves()
    {
        return Waves.Count;
    }
}
