using Domain.Application.Components.Button;
using Domain.Application.Texture.Sprite.Custom.Ui.Buttons;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;
using Domain.Dto.Global;
using Domain.Enum.Component.Button;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace RpgTurno.Custom.Component.Save.Delete;

public class DeleteButtonSlotComponent : ButtonIconComponent
{
    private readonly ConfirmSaveDeletionComponent _confirmDialog;
    private readonly Action OnConfirm;
    private readonly Action OnOpenDialog;
    private readonly Action OnCloseDialog;

    public DeleteButtonSlotComponent(string slotName, Action onDelete, Action onOpenDialog, Action onCloseDialog) : base(new CloseIconSprite())
    {
        AnimationManager.Add(ButtonInteractionState.Regular, new SmallRedRoundButtonRegularSprite());
        AnimationManager.Add(ButtonInteractionState.Pressed, new SmallRedRoundButtonPressedSprite());

        Bounds = new(0, 0, 124, 124);

        Click += OpenDialog;

        OnConfirm = onDelete;
        OnOpenDialog = onOpenDialog;
        OnCloseDialog = onCloseDialog;

        _confirmDialog = new(slotName, OnConfirmDialog, CloseDialog);
        _confirmDialog.IsVisible = false;
        _confirmDialog.IsEnable = false;
    }

    private void OnConfirmDialog()
    {
        OnConfirm?.Invoke();
        CloseDialog();
    }

    private void OpenDialog()
    {
        _confirmDialog.IsVisible = true;
        _confirmDialog.IsEnable = true;
        OnOpenDialog?.Invoke();
    }

    private void CloseDialog()
    {
        _confirmDialog.IsVisible = false;
        _confirmDialog.IsEnable = false;
        OnCloseDialog?.Invoke();
    }

    public void UpdateDialog(GameTime gameTime)
    {
        if (_confirmDialog.IsEnable)
            _confirmDialog?.Update(gameTime);
    }

    public void DrawDialog(SpriteBatch spriteBatch)
    {
        if (_confirmDialog.IsVisible)
            _confirmDialog?.Draw(spriteBatch);
    }

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);

        _confirmDialog.SetPosition(GlobalOptionsDto.WidthSize / 2 - _confirmDialog.Bounds.Width / 2, GlobalOptionsDto.HeightSize / 2 - _confirmDialog.Bounds.Height / 2);
    }
}
