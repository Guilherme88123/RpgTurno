using Domain.Dto.Map.Node;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;

namespace Domain.Dto.Map;

public class MapData
{
    public List<MapNodeData> Nodes { get; set; } = new();
    public MapNodeData StartStage { get; set; }

    public bool Cleared => GetClearedStatus();

    private bool GetClearedStatus()
    {
        return Nodes
            .Where(x => x is StageMapNode)
            .Select(x => x as StageMapNode)
            .All(x => x.Cleared);
    }
}
