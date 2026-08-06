using Domain.Application.Components.Image;
using Domain.Application.Components.Text;
using Domain.Application.MenuComponents.Frame;
using Domain.Application.Texture.Sprite;
using Domain.Application.Texture.Sprite.Custom.Ui.Banners;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;
using Domain.Const.Text;
using Domain.Dto.Language;
using Domain.Enum.Save;
using Domain.Model.Save;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RpgTurno.Custom.Component.Save.Delete;
using System;

namespace RpgTurno.Custom.Component.Save;

public class SaveSlotComponent : FrameComponent
{
    private const int Margin = 48;

    private readonly Action<SaveModel, SavePositionType> _onSaveSelect;
    private readonly Action<SaveModel> _onSaveDelete;
    private readonly SaveModel _save;
    private readonly SavePositionType _position;

    private readonly TextComponent _progressText = new(positionXByCenter: true, positionYByCenter: true);
    private readonly ButtonSlotComponent _backgroundButton;
    private readonly TextComponent _titleText = new();
    private readonly TextComponent _lastPlayText = new();
    private readonly TextComponent _createDayText = new();
    private readonly ImageComponent _gameFinishIcon = new(new YellowStarIconSprite(), 24, 24);
    private readonly DeleteButtonSlotComponent _deleteSaveButton;

    public SaveSlotComponent(Action<SaveModel, SavePositionType> onSaveSelect, Action<SaveModel> onSaveDelete, Action onDeletionDialogOpen, Action onDeletionDialogClose, SaveModel save, SavePositionType position)
    {
        _onSaveSelect = onSaveSelect;
        _onSaveDelete = onSaveDelete;
        _save = save;
        _position = position;

        Bounds = new(0, 0, 900, 256);

        bool hasSave = save is not null;
        bool hasGameFinished = hasSave && save.HasGameFinish;

        var slotName = GetTitleByPosition();

        _titleText.SetText(slotName);
        _progressText.SetText(GetProgressText(save));
        if (hasSave)
        {
            _lastPlayText.SetText($"{LanguageManager.Get(TextConst.LastPlay)}: {GetDateTimeFriendly(save.LastPlayDate)}");
            _createDayText.SetText($"{LanguageManager.Get(TextConst.Created)}: {GetDateTimeFriendly(save.CreationDate)}");
        }

        _lastPlayText.IsVisible = hasSave;
        _createDayText.IsVisible = hasSave;

        _gameFinishIcon.IsVisible = hasGameFinished;

        if (hasGameFinished)
            _progressText.Color = _titleText.Color = _lastPlayText.Color = _createDayText.Color = Color.Gold;

        _backgroundButton = new(GetSpriteBySaveStatus());
        _backgroundButton.SetBounds(Bounds.Width, Bounds.Height);
        _backgroundButton.Click += OnBackgroundButtonClick;

        _deleteSaveButton = new(slotName, OnDeleteButtonClick, onDeletionDialogOpen, onDeletionDialogClose);
        _deleteSaveButton.IsVisible = hasSave;

        AddChild(_backgroundButton);
        AddChild(_titleText);
        AddChild(_progressText);
        AddChild(_gameFinishIcon);
        AddChild(_lastPlayText);
        AddChild(_createDayText);
    }

    private string GetTitleByPosition()
    {
        var slotName = LanguageManager.Get(TextConst.Slot);

        return _position switch
        {
            SavePositionType.Top => $"{slotName} 1",
            SavePositionType.Middle => $"{slotName} 2",
            SavePositionType.Bottom => $"{slotName} 3",
        };
    }

    private string GetProgressText(SaveModel save)
    {
        if (save is null)
            return LanguageManager.Get(TextConst.EmptySlot);

        return $"{LanguageManager.Get(TextConst.Progress)}: {save.Progress}%";
    }

    private SpriteData GetSpriteBySaveStatus()
    {
        return _save switch
        {
            null => new PaperBannerSprite(),
            _ => new SpecialPaperBannerSprite(),
        };
    }

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);

        _backgroundButton.SetPosition(positionX, positionY);
        _titleText.SetPosition(positionX + Margin, positionY + Margin);
        if (_save is not null)
        {
            _lastPlayText.SetPosition(positionX + Margin, Bounds.Bottom - Margin - _lastPlayText.Bounds.Height);
            _createDayText.SetPosition(Bounds.Right - Margin - _createDayText.Bounds.Width, Bounds.Bottom - Margin - _lastPlayText.Bounds.Height);
        }

        _progressText.SetPosition(Bounds.Center.X, Bounds.Center.Y);
        _gameFinishIcon.SetPosition(_progressText.Bounds.Right + 8, Bounds.Center.Y - _gameFinishIcon.Bounds.Height / 3 * 2);

        _deleteSaveButton.SetPosition(Bounds.Right, Bounds.Center.Y - _deleteSaveButton.Bounds.Height / 2);
    }

    private void OnBackgroundButtonClick()
    {
        _onSaveSelect?.Invoke(_save, _position);
    }

    private void OnDeleteButtonClick()
    {
        _onSaveDelete?.Invoke(_save);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _titleText.OffsetY =
            _progressText.OffsetY =
            _gameFinishIcon.OffsetY =
            _deleteSaveButton.OffsetY =
            _lastPlayText.OffsetY =
            _createDayText.OffsetY =
            _backgroundButton.OffsetY;
    }

    public void UpdateDeleteButton(GameTime gameTime)
    {
        if (_deleteSaveButton.IsEnable)
            _deleteSaveButton.Update(gameTime);
    }

    public void UpdateDeleteDialog(GameTime gameTime)
    {
        if (_deleteSaveButton.IsEnable)
            _deleteSaveButton.UpdateDialog(gameTime);
    }

    public void DrawDeleteButton(SpriteBatch spriteBatch)
    {
        if (_deleteSaveButton.IsVisible)
            _deleteSaveButton.Draw(spriteBatch);
    }

    public void DrawDeleteDialog(SpriteBatch spriteBatch)
    {
        if (_deleteSaveButton.IsVisible)
            _deleteSaveButton.DrawDialog(spriteBatch);
    }

    private string GetDateTimeFriendly(DateTime dateTime)
    {
        var today = DateTime.Today;
        var date = dateTime.Date;

        if (date == today)
            return $"{LanguageManager.Get(TextConst.Today)} • {dateTime:HH:mm}";

        if (date == today.AddDays(-1))
            return $"{LanguageManager.Get(TextConst.Yesterday)} • {dateTime:HH:mm}";

        var days = (today - date).Days;

        if (days <= 7)
            return LanguageManager.Get(TextConst.DaysAgo).Replace("{day}", days.ToString());

        return dateTime.ToString("MMM dd, yyyy");
    }
}
