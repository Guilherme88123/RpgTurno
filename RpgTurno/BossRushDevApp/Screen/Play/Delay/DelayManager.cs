using Domain.Dto.Global;

namespace RpgTurno.Screen.Play.Delay;

public class DelayManager
{
    private const float DelayTurnExecution = 0.5f;
    private float _currentDelayTurnExecution = DelayTurnExecution;

    public void ResetDelayTurnExecution() => _currentDelayTurnExecution = DelayTurnExecution;
    public bool HasDelayTurnExecutionComplete() => _currentDelayTurnExecution < 0;

    private const float DelayAttackExecution = 0.5f;
    private float _currentDelayAttackExecution = DelayTurnExecution;

    public void ResetDelayAttackExecution() => _currentDelayAttackExecution = DelayAttackExecution;
    public bool HasDelayAttackExecutionComplete() => _currentDelayAttackExecution < 0;

    public void Update()
    {
        _currentDelayTurnExecution -= GlobalVariablesDto.DeltaTime;
        _currentDelayAttackExecution -= GlobalVariablesDto.DeltaTime;
    }
}
