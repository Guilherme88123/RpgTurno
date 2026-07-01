using Domain.Enum.Stage;
using Microsoft.Xna.Framework;

namespace RpgTurno.Screen.Map.World.Stage;

public static class MapFactory
{
    public static MapData Create()
    {
        var map = new MapData();

        var stage1 = new StageMapNode(StageCode.Tower, new Vector2(650, 500));
        var stage2 = new StageMapNode(StageCode.Barrack, new Vector2(1100, 800));
        var stage3 = new StageMapNode(StageCode.Castle, new Vector2(1450, 300));

        stage1.NextNodes.Add(stage2);
        stage2.NextNodes.Add(stage3);

        stage2.PreviousNode = stage1;
        stage3.PreviousNode = stage2;

        map.Nodes.Add(stage1);
        map.Nodes.Add(stage2);
        map.Nodes.Add(stage3);

        map.StartStage = stage1;

        return map;
    }
}
