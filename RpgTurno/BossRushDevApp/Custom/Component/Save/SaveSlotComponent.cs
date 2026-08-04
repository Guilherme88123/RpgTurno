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

    private readonly Action<SaveModel> _onSaveSelect;
    private readonly SaveModel _save;
    private readonly SavePositionType _position;

    private readonly ButtonSlotComponent _button;
    private readonly TextComponent _title = new();

    public SaveSlotComponent(Action<SaveModel> onSaveSelect, SaveModel save, SavePositionType position)
    {
        _onSaveSelect = onSaveSelect;
        _save = save;
        _position = position;

        Bounds = new(0, 0, 900, 256);

        _title.SetText(GetTitleByPosition());

        _button = new(GetSpriteBySaveStatus());
        _button.SetBounds(Bounds.Width, Bounds.Height);
        _button.Click += OnButtonClick;

        AddChild(_title);
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

    private SpriteData GetSpriteBySaveStatus()
    {
        return _save switch
        {
            _ when _save is null => new PaperBannerSprite(),
            _ when _save.Progress < 100 => new WoodBannerSprite(),
            _ when _save.Progress >= 100 => new SpecialPaperBannerSprite(),
        };
    }

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);

        _title.SetPosition(positionX + Margin, positionY + Margin);
        _button.SetPosition(positionX, positionY);
    }

    private void OnButtonClick()
    {
        _onSaveSelect?.Invoke(_save);
    }
}
