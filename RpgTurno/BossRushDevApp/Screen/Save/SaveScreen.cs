using Domain.Application.Components.Base;
using Domain.Const.Screen;
using Domain.Dto.Global;
using Domain.Dto.Session;
using Domain.Enum.Save;
using Domain.Interface.Repositories.Save;
using Domain.Model.Save;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RpgTurno.Custom.Component.Save;
using RpgTurno.Custom.Component.Save.Menu;
using RpgTurnoApp.Screen.Base;
using Service.Save;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpgTurno.Screen.Save;

public class SaveScreen : BaseScreen
{
    public override string ScreenCode => ScreenConst.SaveScreen;

    private bool _isDialogOpen = false;

    private readonly ISaveService _saveService;

    private SaveModel SaveSlot1;
    private SaveModel SaveSlot2;
    private SaveModel SaveSlot3;

    private SaveSlotComponent _saveSlot1Component;
    private SaveSlotComponent _saveSlot2Component;
    private SaveSlotComponent _saveSlot3Component;

    private MainMenuSaveButtonComponent _exitButton;

    public SaveScreen()
    {
        _saveService = GlobalVariablesDto.GetService<ISaveService>();

        LoadSaves().Wait();
    }

    #region Initialize

    private async Task LoadSaves()
    {
        var saves = await _saveService.GetAllAsync();

        SaveSlot1 = saves.FirstOrDefault(x => x.Position == SavePositionType.Top);
        SaveSlot2 = saves.FirstOrDefault(x => x.Position == SavePositionType.Middle);
        SaveSlot3 = saves.FirstOrDefault(x => x.Position == SavePositionType.Bottom);
    }

    protected override List<BaseComponent> InitializeComponents()
    {
        _saveSlot1Component = new SaveSlotComponent(InitGameWithSelectedSave, OnDeleteSave, OnDialogOpen, OnDialogClose, SaveSlot1, SavePositionType.Top);
        _saveSlot2Component = new SaveSlotComponent(InitGameWithSelectedSave, OnDeleteSave, OnDialogOpen, OnDialogClose, SaveSlot2, SavePositionType.Middle);
        _saveSlot3Component = new SaveSlotComponent(InitGameWithSelectedSave, OnDeleteSave, OnDialogOpen, OnDialogClose, SaveSlot3, SavePositionType.Bottom);

        var height = _saveSlot1Component.Bounds.Height;
        var spacing = 16;
        var totalHeight = height * 3 + spacing * 2;

        var x = GlobalOptionsDto.WidthSize / 2 - _saveSlot1Component.Bounds.Width / 2;
        var y = GlobalOptionsDto.HeightSize / 2 - totalHeight / 2;

        _saveSlot1Component.SetPosition(x, y);
        _saveSlot2Component.SetPosition(x, y + height + spacing);
        _saveSlot3Component.SetPosition(x, y + (height + spacing) * 2);

        _exitButton = new MainMenuSaveButtonComponent();
        _exitButton.SetPosition(
            GlobalOptionsDto.WidthSize / 2 - _exitButton.Bounds.Width / 2,
            GlobalOptionsDto.HeightSize - _exitButton.Bounds.Height - spacing);

        return
        [
            _exitButton,
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

    #endregion

    #region Update

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        UpdateDeleteDialogs(gameTime);

        if (_isDialogOpen)
            return;

        UpdateDeleteButtons(gameTime);
        UpdateSaveSlots(gameTime);
    }

    public void UpdateSaveSlots(GameTime gameTime)
    {
        _saveSlot1Component.Update(gameTime);
        _saveSlot2Component.Update(gameTime);
        _saveSlot3Component.Update(gameTime);
    }

    public void UpdateDeleteButtons(GameTime gameTime)
    {
        _saveSlot1Component.UpdateDeleteButton(gameTime);
        _saveSlot2Component.UpdateDeleteButton(gameTime);
        _saveSlot3Component.UpdateDeleteButton(gameTime);
    }

    public void UpdateDeleteDialogs(GameTime gameTime)
    {
        _saveSlot1Component.UpdateDeleteDialog(gameTime);
        _saveSlot2Component.UpdateDeleteDialog(gameTime);
        _saveSlot3Component.UpdateDeleteDialog(gameTime);
    }

    #endregion

    #region Draw

    public override void Draw()
    {
        base.Draw();
        DrawDeleteButton(GlobalVariablesDto.SpriteBatchInterface);
        DrawSaveSlots(GlobalVariablesDto.SpriteBatchInterface);

        if (_isDialogOpen)
            DrawPausedShade();

        DrawDeleteDialog(GlobalVariablesDto.SpriteBatchInterface);
    }

    public void DrawSaveSlots(SpriteBatch spriteBatch)
    {
        _saveSlot1Component.Draw(spriteBatch);
        _saveSlot2Component.Draw(spriteBatch);
        _saveSlot3Component.Draw(spriteBatch);
    }

    public void DrawDeleteButton(SpriteBatch spriteBatch)
    {
        _saveSlot1Component.DrawDeleteButton(spriteBatch);
        _saveSlot2Component.DrawDeleteButton(spriteBatch);
        _saveSlot3Component.DrawDeleteButton(spriteBatch);
    }

    public void DrawDeleteDialog(SpriteBatch spriteBatch)
    {
        _saveSlot1Component.DrawDeleteDialog(spriteBatch);
        _saveSlot2Component.DrawDeleteDialog(spriteBatch);
        _saveSlot3Component.DrawDeleteDialog(spriteBatch);
    }

    private void DrawPausedShade()
    {
        var screenRectangle = new Rectangle(0, 0, GlobalOptionsDto.WidthSize, GlobalOptionsDto.HeightSize);
        GlobalVariablesDto.SpriteBatchInterface.Draw(GlobalVariablesDto.Pixel, screenRectangle, Color.Black * 0.4f);
    }

    #endregion

    #region Dialog Flag Control

    private void OnDialogOpen()
    {
        _isDialogOpen = true;
        UpdateDialogEnableComponents();
    }

    private void OnDialogClose()
    {
        _isDialogOpen = false;
        UpdateDialogEnableComponents();
    }

    private void UpdateDialogEnableComponents()
    {
        _exitButton.IsEnable = !_isDialogOpen;
    }

    #endregion
}
