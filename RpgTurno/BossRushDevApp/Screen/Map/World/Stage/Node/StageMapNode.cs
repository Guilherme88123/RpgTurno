using Domain.Enum.Stage;
using Microsoft.Xna.Framework;

namespace RpgTurno.Screen.Map.World.Stage.Node;

public class StageMapNode : MapNodeData
{
    public StageCode StageCode { get; set; }
    public bool Cleared { get; set; }

    public StageMapNode(Vector2 position, StageCode stageCode) : base(position)
    {
        StageCode = stageCode;
    }
}
