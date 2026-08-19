using Domain.Dto.Global;
using Domain.Enum.Stage;
using Infrastructure.Tiled;
using Infrastructure.Tiled.Dto;

namespace Service.Stage;

public static class StageMapDtoFactory
{
    public static TiledMapDto Create(StageCode stageCode)
    {
        var stageName = GetNameByStageCode(stageCode);
        var stageFilename = GetFilenameByStageName(stageName);
        return GetTiledDtoByFilename(stageFilename);
    }

    private static string GetNameByStageCode(StageCode stageCode)
    {
        return stageCode switch
        {
            StageCode.EvilTower => "TowerMap",
            StageCode.BarracksOfValor => "BarrackMap",
            StageCode.TheCastle => "CastleMap",
        };
    }

    private static string GetFilenameByStageName(string stageName)
    {
        return Path.Combine(GlobalVariablesDto.Content.RootDirectory, $"{stageName}.json");
    }

    private static TiledMapDto GetTiledDtoByFilename(string filename)
    {
        return TiledManagerService.ParseTiledMap(filename);
    }
}
