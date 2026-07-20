using Domain.Model.Components.Text;
using Domain.Model.MenuComponents.Frame;
using Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Banners;
using System;

namespace RpgTurno.Custom.Component.Play.Banners.Finish;

public class BattleFinishBannerComponent : FrameComponent
{
    private bool _isGameOver;

    private const int MarginY = 80;

    private TextComponent _title = new(positionXByCenter: true, positionYByCenter: true);

    private FinishButtonBannerComponent _mapButton;
    private FinishButtonBannerComponent _retryButton;

    public BattleFinishBannerComponent(Action onMapAction, Action onRetryAction)
    {
        AnimationManager.Add(true, new SpecialPaperBannerSprite());

        _mapButton = new("Return to Map", onMapAction);
        _retryButton = new("Play Again", onRetryAction);

        AddChild(_title);
        AddChild(_mapButton);
        AddChild(_retryButton);

        Bounds = new(0, 0, 534, 640);
    }

    public void SetFinishBattleStatus(bool isGameOver)
    {
        _isGameOver = isGameOver;

        if (isGameOver)
            SetGameOverStatus();
        else
            SetWinStatus();

        ReloadPosition();
    }

    private void SetGameOverStatus()
    {
        _title.SetText("Game Over");

        _mapButton.IsVisible = true;
        _retryButton.IsVisible = true;
    }

    private void SetWinStatus()
    {
        _title.SetText("Victory");

        _mapButton.IsVisible = true;
        _retryButton.IsVisible = false;
    }

    #region Position

    private void ReloadPosition()
    {
        SetPosition(Bounds.X, Bounds.Y);
    }

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);

        _title.SetPosition(Bounds.Center.X, Bounds.Y + MarginY);

        if (_isGameOver)
            SetGameOverPosition();
        else
            SetWinPosition();
    }

    private void SetGameOverPosition()
    {
        _mapButton.SetPosition(Bounds.Center.X - _mapButton.Bounds.Width, Bounds.Bottom - _mapButton.Bounds.Height - MarginY);
        _retryButton.SetPosition(Bounds.Center.X, Bounds.Bottom - _mapButton.Bounds.Height - MarginY);
    }

    private void SetWinPosition()
    {
        _mapButton.SetPosition(Bounds.Center.X - _mapButton.Bounds.Width / 2, Bounds.Bottom - _mapButton.Bounds.Height - MarginY);
    }

    #endregion
}
