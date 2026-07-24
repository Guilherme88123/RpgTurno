using Domain.Model.Components.Image;
using Domain.Model.Components.Text;
using Domain.Model.MenuComponents.Frame;
using Domain.Model.Texture.Sprite.Custom.Ui.Banners;
using Domain.Model.Texture.Sprite.Custom.Ui.Icons;
using Domain.Model.Texture.Sprite.Custom.Ui.Ribbons.Small;
using System;

namespace RpgTurno.Custom.Component.Map.Banner.Finish;

public class GameFinishBannerComponent : FrameComponent
{
    private const int Width = 534;
    private const int MarginTop = 96;
    private const int MarginBottom = 80;
    private const int MarginX = 48;

    private TextComponent _title = new(positionXByCenter: true, positionYByCenter: true);
    private ImageComponent _titleBackground = new(new BlueSmallRibbonSprite(), Width - MarginX * 2, 64);

    private TextComponent _textDefeatedBoss = new(positionXByCenter: true, positionYByCenter: true);
    private TextComponent _textThanks = new(positionXByCenter: true, positionYByCenter: true);
    private TextComponent _textMoreContent = new(positionXByCenter: true, positionYByCenter: true);
    private TextComponent _textSuggestions = new(positionXByCenter: true, positionYByCenter: true);

    private ImageComponent _iconHeart = new(new HeartIconSprite(), 48, 48);

    private ButtonGameFinishBannerComponent _menuButton;

    public GameFinishBannerComponent(Action onMenuAction)
    {
        AnimationManager.Add(true, new ScrollBannerSprite());

        _menuButton = new("Main Menu", onMenuAction);

        _title.SetText("Congratulations!");
        _textDefeatedBoss.SetText("You defeated the supreme knight.");
        _textThanks.SetText("Thank you for playing Tiny RPG Alpha 0.1.0");
        _textMoreContent.SetText("More content coming soon!");
        _textSuggestions.SetText("Don't forget to leave suggestions!");

        AddChild(_titleBackground);
        AddChild(_title);
        AddChild(_menuButton);
        AddChild(_textDefeatedBoss);
        AddChild(_textThanks);
        AddChild(_textMoreContent);
        AddChild(_iconHeart);
        AddChild(_textSuggestions);

        Bounds = new(0, 0, Width, 640);
    }

    #region Position

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);

        _title.SetPosition(Bounds.Center.X, Bounds.Y + MarginTop);
        _titleBackground.SetPosition(Bounds.Center.X - _titleBackground.Bounds.Width / 2, Bounds.Y + MarginTop - 32);

        _textDefeatedBoss.SetPosition(Bounds.Center.X, GetYPositionByIndex(0));
        _textThanks.SetPosition(Bounds.Center.X, GetYPositionByIndex(1));
        _iconHeart.SetPosition(Bounds.Center.X - _iconHeart.Bounds.Width / 2, GetYPositionByIndex(2) - _iconHeart.Bounds.Height / 2);
        _textMoreContent.SetPosition(Bounds.Center.X, GetYPositionByIndex(3));
        _textSuggestions.SetPosition(Bounds.Center.X, GetYPositionByIndex(4));

        _menuButton.SetPosition(Bounds.Center.X - _menuButton.Bounds.Width / 2, Bounds.Bottom - _menuButton.Bounds.Height - MarginBottom);
    }

    private int GetYPositionByIndex(int index)
    {
        var textHeight = 48;
        return Bounds.Y + MarginTop * 2 + index * textHeight;
    }

    #endregion
}
