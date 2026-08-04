using Domain.Const.Text;
using Domain.Dto.Map;
using Domain.Dto.Map.Node;
using Domain.Enum.Stage;
using Domain.Model.Stage;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace RpgTurno.Screen.Map.World.Stage;

public static class MapFactory
{
    public static MapData Create(List<StageModel> stages)
    {
        var map = new MapData();

        var start = new StartMapNode(new Vector2(130, 750));
        var stage1 = new StageMapNode(new Vector2(640, 620), StageCode.Tower, TextConst.EvilTowerStage, 1);
        var stage2 = new StageMapNode(new Vector2(1100, 950), StageCode.Barrack, TextConst.BarracksOfValorStage, 2);
        var stage3 = new StageMapNode(new Vector2(1450, 450), StageCode.Castle, TextConst.TheCastleStage, 3);

        start.NextNodes.Add(stage1);
        stage1.NextNodes.Add(stage2);
        stage2.NextNodes.Add(stage3);

        stage1.PreviousNode = start;
        stage2.PreviousNode = stage1;
        stage3.PreviousNode = stage2;

        map.Nodes.Add(stage1);
        map.Nodes.Add(stage2);
        map.Nodes.Add(stage3);

        LoadStages(map, stages);

        map.StartStage = start;

        return map;
    }

    private static void LoadStages(MapData map, List<StageModel> stages)
    {
        var mapStages = map.Nodes
            .Where(x => x is StageMapNode)
            .Select(x => x as StageMapNode);

        foreach (var mapStage in mapStages)
        {
            var stage = stages.FirstOrDefault(x => x.StageCode == mapStage.StageCode);

            if (stage is null)
                continue;

            mapStage.Cleared = stage.IsCompleted;
        }
    }
}
