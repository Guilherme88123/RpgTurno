using Domain.Model.Entity.Units.Base;
using System.Collections.Generic;
using System.Linq;

namespace RpgTurno.Screen.Play.Battle.Turn;

public class TurnQueueManager
{
    private Queue<BaseUnitEntity> _unitsQueue = new();

    public void SetUnitsQueue(List<BaseUnitEntity> rawUnitsList)
    {
        _unitsQueue.Clear();

        var ordenedUnitsList = GetQueueOrderned(rawUnitsList);

        foreach (var unit in ordenedUnitsList)
        {
            _unitsQueue.Enqueue(unit);
        }
    }

    private List<BaseUnitEntity> GetQueueOrderned(List<BaseUnitEntity> rawUnitsList)
    {
        //TODO: Ordem de ataque por alguma estatística em específico (Velocidade, Destreza, etc)
        var orderUnitsList = rawUnitsList.Shuffle().ToList();

        return orderUnitsList;
    }

    public BaseUnitEntity GetPeekUnit()
    {
        return _unitsQueue.Peek();
    }

    public List<BaseUnitEntity> GetUnitQueueList()
    {
        return _unitsQueue.ToList();
    }

    public void NextTurn()
    {
        var oldFirst = _unitsQueue.Dequeue();
        _unitsQueue.Enqueue(oldFirst);
    }

    public void RemoveUnit(BaseUnitEntity unit)
    {
        _unitsQueue = new Queue<BaseUnitEntity>(_unitsQueue.Where(x => x != unit));
    }
}
