using Domain.Dto.Global;
using System;

namespace RpgTurno.Screen.Play.Battle.Delay;

public class DelayManager
{
    private const float DelayTurnExecution = 0.7f;
    private float _currentDelayTurnExecution;

    public void ResetDelayTurnExecution() => _currentDelayTurnExecution = DelayTurnExecution;
    public bool HasDelayTurnExecutionComplete() => _currentDelayTurnExecution <= 0;

    private const float DelayAttackExecution = 0.5f;
    private float _currentDelayAttackExecution;

    public void ResetDelayAttackExecution() => _currentDelayAttackExecution = DelayAttackExecution;
    public bool HasDelayAttackExecutionComplete() => _currentDelayAttackExecution <= 0;

    public void Update()
    {
        _currentDelayTurnExecution = Math.Max(0, _currentDelayTurnExecution - GlobalVariablesDto.DeltaTime);
        _currentDelayAttackExecution = Math.Max(0, _currentDelayAttackExecution - GlobalVariablesDto.DeltaTime);
    }
}
