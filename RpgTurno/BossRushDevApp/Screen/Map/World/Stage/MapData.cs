using RpgTurno.Screen.Map.World.Stage.Node;
using System.Collections.Generic;

namespace RpgTurno.Screen.Map.World.Stage;

public class MapData
{
    public List<MapNodeData> Nodes { get; set; } = new();
    public MapNodeData StartStage { get; set; }
}
