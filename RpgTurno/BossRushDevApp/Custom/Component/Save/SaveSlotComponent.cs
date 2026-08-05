using Domain.Application.Components.Image;
using Domain.Application.Components.Text;
using Domain.Application.MenuComponents.Frame;
using Domain.Application.Texture.Sprite;
using Domain.Application.Texture.Sprite.Custom.Ui.Banners;
using Domain.Application.Texture.Sprite.Custom.Ui.Icons;
using Domain.Enum.Save;
using Domain.Model.Save;
using Microsoft.Xna.Framework;
using System;

namespace RpgTurno.Custom.Component.Save;

public class SaveSlotComponent : FrameComponent
{
    private const int Margin = 48;

    private readonly Action<SaveModel, SavePositionType> _onSaveSelect;
    private readonly SaveModel _save;
    private readonly SavePositionType _position;

    private readonly ButtonSlotComponent _button;
    private readonly TextComponent _titleText = new();
    private readonly TextComponent _progressText = new(positionXByCenter: true, positionYByCenter: true);
    private readonly ImageComponent _gameFinishIcon = new(new YellowStarIconSprite(), 24, 24);

    public SaveSlotComponent(Action<SaveModel, SavePositionType> onSaveSelect, SaveModel save, SavePositionType position)
    {
        _onSaveSelect = onSaveSelect;
        _save = save;
        _position = position;

        Bounds = new(0, 0, 900, 256);

        _titleText.SetText(GetTitleByPosition());
        _progressText.SetText(GetProgressText(save));

        bool hasGameFinished = save is not null && save.Progress >= 100;

        _gameFinishIcon.IsVisible = hasGameFinished;

        if (hasGameFinished)
            _progressText.Color = _titleText.Color = Color.Gold;

        _button = new(GetSpriteBySaveStatus());
        _button.SetBounds(Bounds.Width, Bounds.Height);
        _button.Click += OnButtonClick;

        AddChild(_button);
        AddChild(_titleText);
        AddChild(_progressText);
        AddChild(_gameFinishIcon);
    }

    private string GetTitleByPosition()
    {
        return _position switch
        {
            SavePositionType.Top => "Slot 1",
            SavePositionType.Middle => "Slot 2",
            SavePositionType.Bottom => "Slot 3",
        };
    }

    private string GetProgressText(SaveModel save)
    {
        if (save is null)
            return "Empty Slot";

        return $"Progress: {save.Progress}%";
    }

    private SpriteData GetSpriteBySaveStatus()
    {
        if (_save is null)
            return new PaperBannerSprite();
        else
            return new SpecialPaperBannerSprite();
    }

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);

        _button.SetPosition(positionX, positionY);
        _titleText.SetPosition(positionX + Margin, positionY + Margin);

        _progressText.SetPosition(Bounds.Center.X, Bounds.Center.Y);
        _gameFinishIcon.SetPosition(_progressText.Bounds.Right + 8, Bounds.Center.Y - _gameFinishIcon.Bounds.Height / 3 * 2);
    }

    private void OnButtonClick()
    {
        _onSaveSelect?.Invoke(_save, _position);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _titleText.OffsetY = _progressText.OffsetY = _gameFinishIcon.OffsetY = _button.OffsetY;
    }
}
