using Domain.Application.Entity.Units.Base;
using Domain.Dto.Global;
using Domain.Dto.Session;
using Domain.Enum.Save;
using Domain.Enum.Stage;
using Domain.Enum.Unit;
using Domain.Interface.Repositories.Save;
using Domain.Interface.Repositories.Stage;
using Domain.Interface.Repositories.Unit;
using Domain.Model.Save;
using Domain.Model.Stage;
using Domain.Model.Unit;
using RpgTurno.Screen.Map.World.Stage;
using Service.Unit;

namespace Service.Save;

public static class SaveManager
{
    private static readonly IUnitService _unitService = GlobalVariablesDto.GetService<IUnitService>();
    private static readonly IStageService _stageService = GlobalVariablesDto.GetService<IStageService>();
    private static readonly ISaveService _saveService = GlobalVariablesDto.GetService<ISaveService>();

    #region Save Selection

    public static async Task HandleSaveSelectionAsync(SaveModel selectedSave, SavePositionType position)
    {
        if (selectedSave is null)
            selectedSave = await CreateDefaultSaveSlotAsync(position);

        var units = await GetUnitsBySave(selectedSave.Id);
        var stages = await GetStagesBySave(selectedSave.Id);

        InitializeGameSessionSave(stages, units);
    }

    private static void InitializeGameSessionSave(List<StageModel> stages, List<BaseUnitEntity> units)
    {
        var gameSession = GlobalVariablesDto.GetService<GameSession>();
        gameSession.Initialze(MapFactory.Create(stages), units);
    }

    #region Units Handler

    private static async Task<List<BaseUnitEntity>> GetUnitsBySave(Guid saveId)
    {
        var unitModels = await GetUnitsModelsAsync(saveId);
        return ParseModelToUnitEntity(unitModels);
    }

    private static async Task<List<UnitModel>> GetUnitsModelsAsync(Guid saveId)
    {
        return await _unitService.GetBySaveAsync(saveId);
    }

    private static List<BaseUnitEntity> ParseModelToUnitEntity(List<UnitModel> unitModels)
    {
        List<BaseUnitEntity> unitsList = new();

        foreach (var unitModel in unitModels)
        {
            var unitEntity = CreateUnitEntityByModel(unitModel.UnitCode, unitModel.Level);

            unitEntity.Stats.CurrentExperience = unitModel.CurrentExperience;

            unitsList.Add(unitEntity);
        }

        return unitsList;
    }

    private static BaseUnitEntity CreateUnitEntityByModel(UnitCode unitCode, int level)
    {
        return UnitFactory.Create(unitCode, level);
    }

    #endregion

    #region Stages Handler

    private static async Task<List<StageModel>> GetStagesBySave(Guid saveId)
    {
        return await _stageService.GetBySaveAsync(saveId);
    }

    #endregion

    #region Default Save Creation

    private static async Task<SaveModel> CreateDefaultSaveSlotAsync(SavePositionType position)
    {
        var save = new SaveModel
        {
            CreationDate = DateTime.Now,
            LastPlayDate = DateTime.Now,
            Position = position,
            Progress = 0,
        };

        await _saveService.CreateAsync(save);
        await CreateDefaultUnitsAsync(save);
        await CreateDefaultStagesAsync(save);

        return save;
    }

    private static async Task CreateDefaultUnitsAsync(SaveModel save)
    {
        var defaultUnits = new List<UnitModel>()
        {
            new UnitModel() { UnitCode = UnitCode.Archer },
            new UnitModel() { UnitCode = UnitCode.Cleric },
            new UnitModel() { UnitCode = UnitCode.Lancer },
            new UnitModel() { UnitCode = UnitCode.Warrior },
        };

        foreach (var unit in defaultUnits)
        {
            unit.SaveId = save.Id;
            unit.Level = 1;

            await _unitService.CreateAsync(unit);
        }
    }

    private static async Task CreateDefaultStagesAsync(SaveModel save)
    {
        var defaultStages = new List<StageModel>()
        {
            new StageModel() { StageCode = StageCode.Tower },
            new StageModel() { StageCode = StageCode.Barrack },
            new StageModel() { StageCode = StageCode.Castle },
        };

        foreach (var stage in defaultStages)
        {
            stage.SaveId = save.Id;
            stage.IsCompleted = false;

            await _stageService.CreateAsync(stage);
        }
    }

    #endregion

    #endregion
}
