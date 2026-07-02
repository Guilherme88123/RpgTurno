using Domain.Enum.Stage;
using Microsoft.Xna.Framework;
using RpgTurno.Screen.Map.World.Stage.Node;

namespace RpgTurno.Screen.Map.World.Stage;

public static class MapFactory
{
    public static MapData Create()
    {
        var map = new MapData();

        var start = new StartMapNode(new Vector2(100, 650));
        var stage1 = new StageMapNode(new Vector2(650, 650), StageCode.Tower, "Evil Tower");
        var stage2 = new StageMapNode(new Vector2(1100, 950), StageCode.Barrack, "Barracks of Valor");
        var stage3 = new StageMapNode(new Vector2(1450, 450), StageCode.Castle, "The Castle");

        start.NextNodes.Add(stage1);
        stage1.NextNodes.Add(stage2);
        stage2.NextNodes.Add(stage3);

        stage1.PreviousNode = start;
        stage2.PreviousNode = stage1;
        stage3.PreviousNode = stage2;

        map.Nodes.Add(stage1);
        map.Nodes.Add(stage2);
        map.Nodes.Add(stage3);

        map.StartStage = start;

        return map;
    }
}
