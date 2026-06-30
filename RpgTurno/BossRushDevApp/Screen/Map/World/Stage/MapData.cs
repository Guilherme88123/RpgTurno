using System.Collections.Generic;

namespace RpgTurno.Screen.Map.World.Stage;

public class MapData
{
    public List<StageMapNode> Nodes { get; set; } = new();
    public StageMapNode StartStage { get; set; }
}
