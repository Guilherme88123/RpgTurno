using Domain.Application.Components.Base;
using Domain.Const.Screen;
using Domain.Dto.Global;
using Domain.Enum.Save;
using Domain.Interface.Repositories.Save;
using Domain.Model.Save;
using RpgTurno.Custom.Component.Save;
using RpgTurnoApp.Screen.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpgTurno.Screen.Save;

public class SaveScreen : BaseScreen
{
    public override string ScreenCode => ScreenConst.SaveScreen;

    private readonly ISaveService _saveService;
    private SaveModel SaveSlot1;
    private SaveModel SaveSlot2;
    private SaveModel SaveSlot3;

    public SaveScreen()
    {
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

    private void InitGameWithSelectedSave(SaveModel selectedSave)
    {
        GlobalVariablesDto.ChangeScreen?.Invoke(ScreenConst.MapScreen);
    }
}
