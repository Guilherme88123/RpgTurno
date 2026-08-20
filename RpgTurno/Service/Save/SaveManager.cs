using Domain.Application.Entity.Units.Base;
using Domain.Dto.Global;
using Domain.Dto.Map;
using Domain.Dto.Session;
using Domain.Enum.Language;
using Domain.Enum.Save;
using Domain.Enum.Stage;
using Domain.Enum.Unit;
using Domain.Interface.Repositories.Save;
using Domain.Interface.Repositories.Settings;
using Domain.Interface.Repositories.Stage;
using Domain.Interface.Repositories.Unit;
using Domain.Model.Save;
using Domain.Model.Settings;
using Domain.Model.Stage;
using Domain.Model.Unit;
using Service.Unit;

namespace Service.Save;

public static class SaveManager
{
    private static readonly IUnitService _unitService = GlobalVariablesDto.GetService<IUnitService>();
    private static readonly IStageService _stageService = GlobalVariablesDto.GetService<IStageService>();
    private static readonly ISaveService _saveService = GlobalVariablesDto.GetService<ISaveService>();
    private static readonly ISettingsService _settingsService = GlobalVariablesDto.GetService<ISettingsService>();

    #region Save Selection

    public static async Task<GameSessionSave> HandleSaveSelectionAsync(SaveModel selectedSave, SavePositionType position)
    {
        if (selectedSave is null)
            selectedSave = await CreateDefaultSaveSlotAsync(position);
        else
            await UpdateLastPlayDateAsync(selectedSave);

        var units = await GetUnitsBySave(selectedSave.Id);
        var stages = await GetStagesBySave(selectedSave.Id);

        return InitializeGameSessionSave(selectedSave, stages, units);
    }

    private static async Task UpdateLastPlayDateAsync(SaveModel save)
    {
        save.LastPlayDate = DateTime.Now;
        await _saveService.UpdateAsync(save);
    }

    private static GameSessionSave InitializeGameSessionSave(SaveModel save, List<StageModel> stages, List<BaseUnitEntity> units)
    {
        return new GameSessionSave(save.Id, MapFactory.Create(stages), units);
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

            unitEntity.Id = unitModel.Id;
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
            new StageModel() { StageCode = StageCode.Kingdom01 },
            new StageModel() { StageCode = StageCode.Kingdom01 },
            new StageModel() { StageCode = StageCode.KingdomBoss },
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

    #region Save Update

    public static async Task UpdateGameSaveAsync(GameSessionSave gameSave)
    {
        await UpdateSaveAsync(gameSave);
        await UpdateStagesAsync(gameSave);
        await UpdateUnitsAsync(gameSave);
    }

    private static async Task UpdateSaveAsync(GameSessionSave gameSave)
    {
        var save = await _saveService.GetAsync(gameSave.SaveId);

        save.LastPlayDate = DateTime.Now;
        save.Progress = GetSaveProgress(gameSave);

        await _saveService.UpdateAsync(save);
    }

    private static List<StageMapNode> GetStagesMapNodesByMap(MapData map)
    {
        return map.Nodes
            .Where(x => x is StageMapNode)
            .Select(x => x as StageMapNode)
            .ToList();
    }

    private static int GetSaveProgress(GameSessionSave gameSave)
    {
        var totalStages = GetStagesMapNodesByMap(gameSave.Map);

        var totalStagesCount = (double)totalStages.Count;
        var completedStagesCount = (double)totalStages.Where(x => x.Cleared).Count();

        return (int)((completedStagesCount / totalStagesCount) * 100);
    }

    private static async Task UpdateStagesAsync(GameSessionSave gameSave)
    {
        var stages = GetStagesMapNodesByMap(gameSave.Map);

        foreach (var stage in stages)
        {
            var stageModel = await _stageService.GetAsync(stage.Id);

            if (stageModel is null || stageModel.IsCompleted == stage.Cleared)
                continue;

            stageModel.IsCompleted = stage.Cleared;

            await _stageService.UpdateAsync(stageModel);
        }
    }

    private static async Task UpdateUnitsAsync(GameSessionSave gameSave)
    {
        foreach (var ally in gameSave.Allies)
        {
            var unitModel = await _unitService.GetAsync(ally.Id);

            if (unitModel is null)
                continue;

            unitModel.Level = ally.Stats.Level;
            unitModel.CurrentExperience = ally.Stats.CurrentExperience;

            await _unitService.UpdateAsync(unitModel);
        }
    }

    #endregion

    #region Save Delete

    public static async Task DeleteSaveAsync(SaveModel save)
    {
        await _saveService.DeleteAsync(save.Id);
    }

    #endregion

    #region Settings

    #region Default Create

    public static async Task<bool> HasSettingsSaveAsync()
    {
        return await _settingsService.AnyAsync();
    }

    public static async Task CreateDefaultSettingsAsync()
    {
        var settings = GetDefaultSettings();

        await _settingsService.CreateAsync(settings);
    }

    private static SettingsModel GetDefaultSettings()
    {
        return new SettingsModel()
        {
            EffectsVolume = 80,
            MusicVolume = 60,
            Fullscreen = false,
            Language = LanguageType.English,
            ResolutionHeight = 720,
            ResolutionWidth = 1280,
            ShowFps = false,
        };
    }

    #endregion

    #region Load Settings

    public static async Task<SettingsModel> GetSettingsSaveAsync()
    {
        return await _settingsService.GetAsync();
    }

    #endregion

    #region Settings Update

    public static async Task UpdateSettingsSaveAsync(SettingsModel settings)
    {
        var oldSettings = await GetSettingsSaveAsync();

        settings.Id = oldSettings.Id;

        await _settingsService.UpdateAsync(settings);
    }

    #endregion

    #endregion
}
