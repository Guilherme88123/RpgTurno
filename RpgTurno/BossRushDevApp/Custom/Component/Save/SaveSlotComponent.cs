using Domain.Application.Components.Text;
using Domain.Application.MenuComponents.Frame;
using Domain.Application.Texture.Sprite;
using Domain.Application.Texture.Sprite.Custom.Ui.Banners;
using Domain.Enum.Save;
using Domain.Model.Save;
using System;

namespace RpgTurno.Custom.Component.Save;

public class SaveSlotComponent : FrameComponent
{
    private const int Margin = 64;

    private readonly Action<SaveModel, SavePositionType> _onSaveSelect;
    private readonly SaveModel _save;
    private readonly SavePositionType _position;

    private readonly ButtonSlotComponent _button;
    private readonly TextComponent _titleText = new();
    private readonly TextComponent _progressText = new();
    private readonly TextComponent _emptySlotText = new(positionXByCenter: true, positionYByCenter: true);

    public SaveSlotComponent(Action<SaveModel, SavePositionType> onSaveSelect, SaveModel save, SavePositionType position)
    {
        _onSaveSelect = onSaveSelect;
        _save = save;
        _position = position;

        Bounds = new(0, 0, 900, 256);

        _titleText.SetText(GetTitleByPosition());
        _progressText.SetText(GetProgressText(save));
        _emptySlotText.SetText("Empty Slot");

        _button = new(GetSpriteBySaveStatus());
        _button.SetBounds(Bounds.Width, Bounds.Height);
        _button.Click += OnButtonClick;

        bool isEmptySlot = _save is null;

        _progressText.IsVisible = !isEmptySlot;
        _emptySlotText.IsVisible = isEmptySlot;

        AddChild(_titleText);
        AddChild(_progressText);
        AddChild(_emptySlotText);
        AddChild(_button);
    }

    private string GetTitleByPosition()
    {
        return _position switch
        {
            SavePositionType.Top => "1",
            SavePositionType.Middle => "2",
            SavePositionType.Bottom => "3",
        };
    }

    private string GetProgressText(SaveModel save)
    {
        if (save is null)
            return string.Empty;

        return $"{save.Progress}%";
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

        var textHeight = 32;

        _titleText.SetPosition(positionX + Margin, positionY + Margin);
        _progressText.SetPosition(Bounds.Right - Margin * 2, Bounds.Bottom - Margin - textHeight);
        _emptySlotText.SetPosition(Bounds.Center.X, Bounds.Center.Y);
        _button.SetPosition(positionX, positionY);
    }

    private void OnButtonClick()
    {
        _onSaveSelect?.Invoke(_save, _position);
    }
}
