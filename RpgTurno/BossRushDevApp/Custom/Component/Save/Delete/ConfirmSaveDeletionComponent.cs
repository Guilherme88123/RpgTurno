using Domain.Application.Components.Image;
using Domain.Application.Components.Text;
using Domain.Application.MenuComponents.Frame;
using Domain.Application.Texture.Sprite.Custom.Ui.Banners;
using Domain.Application.Texture.Sprite.Custom.Ui.Ribbons.Small;
using Domain.Const.Text;
using Domain.Dto.Language;
using System;

namespace RpgTurno.Custom.Component.Save.Delete;

public class ConfirmSaveDeletionComponent : FrameComponent
{
    private const int Width = 1028;
    private const int Height = 300;
    private const int Margin = 64;
    private const int Spacing = 16;

    private int TitleBackgroundWidth = Width - Margin * 2;

    private readonly ImageComponent _titleBackground;
    private readonly TextComponent _titleText;
    private readonly ConfirmSaveDeletionButtonComponent _confirmButton;
    private readonly ConfirmSaveDeletionButtonComponent _cancelButton;

    public ConfirmSaveDeletionComponent(string slotName, Action onConfirm, Action onCancel)
    {
        AnimationManager.Add(true, new ScrollBannerSprite());

        Bounds = new(0, 0, Width, Height);

        _titleBackground = new(new RedSmallRibbonSprite(), TitleBackgroundWidth, Margin);

        _titleText = new(positionXByCenter: true, positionYByCenter: true);
        _titleText.SetText($"{LanguageManager.Get(TextConst.ConfirmSaveDeletion).Replace("{slot}", slotName)}?");

        _confirmButton = new(LanguageManager.Get(TextConst.Confirm), onConfirm, isDanger: true);

        _cancelButton = new(LanguageManager.Get(TextConst.Cancel), onCancel, isDanger: false);

        AddChild(_titleBackground);
        AddChild(_titleText);
        AddChild(_confirmButton);
        AddChild(_cancelButton);
    }

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);

        _titleText.SetPosition(Bounds.Center.X, Bounds.Y + Margin + 32);
        _titleBackground.SetPosition(Bounds.Center.X - _titleBackground.Bounds.Width / 2, _titleText.Bounds.Y - 20);

        _confirmButton.SetPosition(Bounds.Center.X - _confirmButton.Bounds.Width - Spacing, Bounds.Bottom - Margin - _confirmButton.Bounds.Height);
        _cancelButton.SetPosition(Bounds.Center.X + Spacing, Bounds.Bottom - Margin - _cancelButton.Bounds.Height);
    }
}
