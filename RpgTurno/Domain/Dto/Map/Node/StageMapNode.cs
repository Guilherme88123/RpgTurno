using Domain.Enum.Stage;
using Microsoft.Xna.Framework;

namespace Domain.Dto.Map.Node;

public class StageMapNode : MapNodeData
{
    public Guid Id { get; set; }

    public StageCode StageCode { get; set; }
    public bool Cleared { get; set; }

    public string Name { get; set; }

    public int Difficulty { get; set; }

    public StageMapNode(Vector2 position, StageCode stageCode, string name, int difficulty) : base(position)
    {
        StageCode = stageCode;
        Name = name;
        Difficulty = difficulty;
    }
}
