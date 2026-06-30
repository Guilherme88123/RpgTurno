using Domain.Enum.Stage;
using Microsoft.Xna.Framework;

namespace RpgTurno.Screen.Map.World.Stage;

public static class MapFactory
{
    public static MapData Create()
    {
        var map = new MapData();

        var stage1 = new StageMapNode(StageCode.Tower, new Vector2(200, 300));
        var stage2 = new StageMapNode(StageCode.Barrack, new Vector2(450, 250));
        var stage3 = new StageMapNode(StageCode.Castle, new Vector2(700, 300));

        stage1.NextNodes.Add(stage2);
        stage2.NextNodes.Add(stage3);

        map.Nodes.Add(stage1);
        map.Nodes.Add(stage2);
        map.Nodes.Add(stage3);

        map.StartStage = stage1;

        return map;
    }
}
