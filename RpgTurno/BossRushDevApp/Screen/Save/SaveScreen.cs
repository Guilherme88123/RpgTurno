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
using RpgTurno.Custom.Component.Save.Menu;
using RpgTurno.Screen.Map.World.Stage;
using RpgTurnoApp.Screen.Base;
using Service.Save;
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
        var saveSlot1Component = new SaveSlotComponent(InitGameWithSelectedSave, OnDeleteSave, SaveSlot1, SavePositionType.Top);
        var saveSlot2Component = new SaveSlotComponent(InitGameWithSelectedSave, OnDeleteSave, SaveSlot2, SavePositionType.Middle);
        var saveSlot3Component = new SaveSlotComponent(InitGameWithSelectedSave, OnDeleteSave, SaveSlot3, SavePositionType.Bottom);

        var height = saveSlot1Component.Bounds.Height;
        var spacing = 16;
        var totalHeight = height * 3 + spacing * 2;

        var x = GlobalOptionsDto.WidthSize / 2 - saveSlot1Component.Bounds.Width / 2;
        var y = GlobalOptionsDto.HeightSize / 2 - totalHeight / 2;

        saveSlot1Component.SetPosition(x, y);
        saveSlot2Component.SetPosition(x, y + height + spacing);
        saveSlot3Component.SetPosition(x, y + (height + spacing) * 2);

        var menuButton = new MainMenuSaveButtonComponent();
        menuButton.SetPosition(
            GlobalOptionsDto.WidthSize / 2 - menuButton.Bounds.Width / 2, 
            GlobalOptionsDto.HeightSize - menuButton.Bounds.Height - spacing);

        return
        [
            saveSlot1Component,
            saveSlot2Component,
            saveSlot3Component,
            menuButton,
        ];
    }

    private void InitGameWithSelectedSave(SaveModel selectedSave, SavePositionType position)
    {
        HandleGameSaveSelection(selectedSave, position).Wait();
        GlobalVariablesDto.ChangeScreen?.Invoke(ScreenConst.MapScreen);
    }

    private async Task HandleGameSaveSelection(SaveModel selectedSave, SavePositionType position)
    {
        var gameSave = await SaveManager.HandleSaveSelectionAsync(selectedSave, position);
        GameSession.InitialzeSave(gameSave);
    }

    private void OnDeleteSave(SaveModel save)
    {
        HandleGameSaveDelete(save).Wait();
        LoadSaves().Wait();
        Initialize();
    }

    private async Task HandleGameSaveDelete(SaveModel save)
    {
        await SaveManager.DeleteSaveAsync(save);
    }
}
