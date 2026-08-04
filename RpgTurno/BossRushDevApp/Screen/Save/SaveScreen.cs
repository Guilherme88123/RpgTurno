using Domain.Application.Components.Base;
using Domain.Application.Entity.Units.Ally.Archer;
using Domain.Application.Entity.Units.Ally.Cleric;
using Domain.Application.Entity.Units.Ally.Lancer;
using Domain.Application.Entity.Units.Ally.Warrior;
using Domain.Application.Entity.Units.Base;
using Domain.Const.Screen;
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
using RpgTurno.Custom.Component.Save;
using RpgTurno.Screen.Map.World.Stage;
using RpgTurnoApp.Screen.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpgTurno.Screen.Save;

public class SaveScreen : BaseScreen
{
    public override string ScreenCode => ScreenConst.SaveScreen;

    private readonly IUnitService _unitService;
    private readonly IStageService _stageService;
    private readonly ISaveService _saveService;

    private SaveModel SaveSlot1;
    private SaveModel SaveSlot2;
    private SaveModel SaveSlot3;

    public SaveScreen()
    {
        _unitService = GlobalVariablesDto.GetService<IUnitService>();
        _stageService = GlobalVariablesDto.GetService<IStageService>();
        _saveService = GlobalVariablesDto.GetService<ISaveService>();

        LoadSaves().Wait();
    }

    private async Task LoadSaves()
    {
        var saves = await _saveService.GetAllAsync();

        SaveSlot1 = saves.FirstOrDefault(x => x.Position == SavePositionType.Top);
        SaveSlot2 = saves.FirstOrDefault(x => x.Position == SavePositionType.Middle);
        SaveSlot3 = saves.FirstOrDefault(x => x.Position == SavePositionType.Bottom);
    }

    protected override List<BaseComponent> InitializeComponents()
    {
        var saveSlot1Component = new SaveSlotComponent(InitGameWithSelectedSave, SaveSlot1, SavePositionType.Top);
        var saveSlot2Component = new SaveSlotComponent(InitGameWithSelectedSave, SaveSlot2, SavePositionType.Middle);
        var saveSlot3Component = new SaveSlotComponent(InitGameWithSelectedSave, SaveSlot3, SavePositionType.Bottom);

        var height = saveSlot1Component.Bounds.Height;
        var spacing = 16;
        var totalHeight = height * 3 + spacing * 2;

        var x = GlobalOptionsDto.WidthSize / 2 - saveSlot1Component.Bounds.Width / 2;
        var y = GlobalOptionsDto.HeightSize / 2 - totalHeight / 2;

        saveSlot1Component.SetPosition(x, y);
        saveSlot2Component.SetPosition(x, y + height + spacing);
        saveSlot3Component.SetPosition(x, y + (height + spacing) * 2);

        return
        [
            saveSlot1Component,
            saveSlot2Component,
            saveSlot3Component
        ];
    }

    #region Save Handler

    private void InitGameWithSelectedSave(SaveModel selectedSave, SavePositionType position)
    {
        HandleSaveSelectionAsync(selectedSave, position).Wait();

        GlobalVariablesDto.ChangeScreen?.Invoke(ScreenConst.MapScreen);
    }

    private async Task HandleSaveSelectionAsync(SaveModel selectedSave, SavePositionType position)
    {
        if (selectedSave is null)
            selectedSave = await CreateDefaultSaveSlotAsync(position);

        var units = await GetUnitsBySave(selectedSave.Id);
        var stages = await GetStagesBySave(selectedSave.Id);

        InitializeGameSessionSave(stages, units);
    }

    private void InitializeGameSessionSave(List<StageModel> stages, List<BaseUnitEntity> units)
    {
        var gameSession = GlobalVariablesDto.GetService<GameSession>();
        gameSession.Initialze(MapFactory.Create(stages), units);
    }

    #region Units Handler

    private async Task<List<BaseUnitEntity>> GetUnitsBySave(Guid saveId)
    {
        var unitModels = await GetUnitsModelsAsync(saveId);
        return ParseModelToUnitEntity(unitModels);
    }

    private async Task<List<UnitModel>> GetUnitsModelsAsync(Guid saveId)
    {
        return await _unitService.GetBySaveAsync(saveId);
    }

    private List<BaseUnitEntity> ParseModelToUnitEntity(List<UnitModel> unitModels)
    {
        List<BaseUnitEntity> unitsList = new();

        foreach (var unitModel in unitModels)
        {
            var unitEntity = CreateUnitEntityByModel(unitModel.UnitCode);

            unitEntity.Stats.Level = unitModel.Level;
            unitEntity.Stats.CurrentExperience = unitModel.CurrentExperience;

            unitsList.Add(unitEntity);
        }

        return unitsList;
    }

    private BaseUnitEntity CreateUnitEntityByModel(UnitCode unitCode)
    {
        return unitCode switch
        {
            UnitCode.Archer => new ArcherEntity(),
            UnitCode.Cleric => new ClericEntity(),
            UnitCode.Lancer => new LancerEntity(),
            UnitCode.Warrior => new WarriorEntity(),
        };
    }

    #endregion

    #region Stages Handler

    private async Task<List<StageModel>> GetStagesBySave(Guid saveId)
    {
        return await _stageService.GetBySaveAsync(saveId);
    }

    #endregion

    #region Default Save Creation

    private async Task<SaveModel> CreateDefaultSaveSlotAsync(SavePositionType position)
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

    private async Task CreateDefaultUnitsAsync(SaveModel save)
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

    private async Task CreateDefaultStagesAsync(SaveModel save)
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
