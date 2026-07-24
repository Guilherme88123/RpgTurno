using Domain.Model.Components.Image;
using Domain.Model.Components.Text;
using Domain.Model.MenuComponents.Frame;
using Domain.Model.Texture.Sprite.Custom.Ui.Banners;
using Domain.Model.Texture.Sprite.Custom.Ui.Ribbons.Small;
using System;

namespace RpgTurno.Custom.Component.Map.Banner.Finish;

public class GameFinishBannerComponent : FrameComponent
{
    private const int Width = 534;
    private const int MarginY = 96;
    private const int MarginX = 48;

    private TextComponent _title = new(positionXByCenter: true, positionYByCenter: true);
    private ImageComponent _titleBackground = new(new BlueSmallRibbonSprite(), Width - MarginX * 2, 64);

    private TextComponent _textDefeatedBoss = new(positionXByCenter: true, positionYByCenter: true);
    private TextComponent _textThanks = new(positionXByCenter: true, positionYByCenter: true);
    private TextComponent _textMoreContent = new(positionXByCenter: true, positionYByCenter: true);

    private ButtonGameFinishBannerComponent _menuButton;

    public GameFinishBannerComponent(Action onMenuAction)
    {
        AnimationManager.Add(true, new ScrollBannerSprite());

        _menuButton = new("Main Menu", onMenuAction);

        _title.SetText("Congratulations!");
        _textDefeatedBoss.SetText("You defeated the supreme knight.");
        _textThanks.SetText("Thank you for playing Tiny RPG Alpha 0.1.0");
        _textMoreContent.SetText("More content coming soon!");

        AddChild(_titleBackground);
        AddChild(_title);
        AddChild(_menuButton);
        AddChild(_textDefeatedBoss);
        AddChild(_textThanks);
        AddChild(_textMoreContent);

        Bounds = new(0, 0, Width, 640);
    }

    #region Position

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);

        _title.SetPosition(Bounds.Center.X, Bounds.Y + MarginY);
        _titleBackground.SetPosition(Bounds.Center.X - _titleBackground.Bounds.Width / 2, Bounds.Y + MarginY - 32);

        _textDefeatedBoss.SetPosition(Bounds.Center.X, GetYPositionByIndex(0));
        _textThanks.SetPosition(Bounds.Center.X, GetYPositionByIndex(1));
        _textMoreContent.SetPosition(Bounds.Center.X, GetYPositionByIndex(2));

        _menuButton.SetPosition(Bounds.Center.X - _menuButton.Bounds.Width / 2, Bounds.Bottom - _menuButton.Bounds.Height - MarginY);
    }

    private int GetYPositionByIndex(int index)
    {
        var textHeight = 40;
        return Bounds.Y + MarginY * 3 + index * textHeight;
    }

    #endregion
}
