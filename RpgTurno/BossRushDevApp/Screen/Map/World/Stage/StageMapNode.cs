using Domain.Enum.Stage;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace RpgTurno.Screen.Map.World.Stage;

public class StageMapNode
{
    public StageCode StageCode { get; set; }
    public Vector2 Position { get; set; }
    public bool Cleared { get; set; }
    public List<StageMapNode> NextNodes { get; set; } = new();
    public StageMapNode PreviousNode { get; set; }

    public StageMapNode(StageCode stageCode, Vector2 position)
    {
        StageCode = stageCode;
        Position = position;
    }

    public StageMapNode GetNextNode()
    {
        if (NextNodes.Count > 0)
        {
            return NextNodes[0];
        }

        return null;
    }
}
