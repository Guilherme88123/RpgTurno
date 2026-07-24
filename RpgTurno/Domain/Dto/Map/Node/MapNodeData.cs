using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Domain.Dto.Map.Node;

public class MapNodeData
{
    public Vector2 Position { get; set; }

    public List<MapNodeData> NextNodes { get; set; } = new();
    public MapNodeData PreviousNode { get; set; }

    public MapNodeData(Vector2 position)
    {
        Position = position;
    }

    public MapNodeData GetNextNode()
    {
        if (NextNodes.Count > 0)
        {
            return NextNodes[0];
        }

        return null;
    }
}
