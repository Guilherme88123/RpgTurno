using Domain.Dto.Session;
using Domain.Model.Components.Image;
using Domain.Model.Components.Text;
using Domain.Model.MenuComponents.Frame;
using Domain.Model.Texture.Sprite.Custom.Ui.Banners;
using Domain.Model.Texture.Sprite.Custom.Ui.Ribbons.Small;
using System;

namespace RpgTurno.Custom.Component.Play.Banners.Finish;

public class BattleFinishBannerComponent : FrameComponent
{
    private bool _isGameOver;
    private PlayStatistics _statistics;

    private const int Width = 534;
    private const int MarginY = 80;
    private const int MarginX = 32;

    private TextComponent _title = new(positionXByCenter: true, positionYByCenter: true);
    private ImageComponent _titleBackground = new(new BlueSmallRibbonSprite(), Width - MarginX * 2, 64);

    private TextComponent _defeatedEnemiesText = new(positionXByCenter: true, positionYByCenter: true);
    private TextComponent _totalXpEarnedText = new(positionXByCenter: true, positionYByCenter: true);
    private TextComponent _battleDurationText = new(positionXByCenter: true, positionYByCenter: true);

    private FinishButtonBannerComponent _mapButton;
    private FinishButtonBannerComponent _retryButton;

    public BattleFinishBannerComponent(Action onMapAction, Action onRetryAction)
    {
        AnimationManager.Add(true, new SpecialPaperBannerSprite());

        _mapButton = new("Return to Map", onMapAction);
        _retryButton = new("Play Again", onRetryAction);

        AddChild(_titleBackground);
        AddChild(_title);
        AddChild(_defeatedEnemiesText);
        AddChild(_totalXpEarnedText);
        AddChild(_battleDurationText);
        AddChild(_mapButton);
        AddChild(_retryButton);

        Bounds = new(0, 0, Width, 640);
    }

    public void SetFinishBattleStatus(bool isGameOver, PlayStatistics statistics)
    {
        _isGameOver = isGameOver;
        _statistics = statistics;

        _defeatedEnemiesText.SetText($"Defeated Enemies: {_statistics.DefeatedEnemies}");
        _totalXpEarnedText.SetText($"Total Experience Earned: {_statistics.TotalExperience}");
        _battleDurationText.SetText($"Battle Duration: {_statistics.Duration:mm\\:ss}");

        if (isGameOver)
            SetGameOverStatus();
        else
            SetWinStatus();

        ReloadPosition();
    }

    private void SetGameOverStatus()
    {
        _title.SetText("Game Over");
        _titleBackground.SetImage(new RedSmallRibbonSprite());

        _mapButton.IsVisible = true;
        _retryButton.IsVisible = true;
    }

    private void SetWinStatus()
    {
        _title.SetText("Victory");
        _titleBackground.SetImage(new BlueSmallRibbonSprite());

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
        _titleBackground.SetPosition(Bounds.Center.X - _titleBackground.Bounds.Width / 2, Bounds.Y + MarginY - 32);

        _defeatedEnemiesText.SetPosition(Bounds.Center.X, GetYPositionByIndex(0));
        _totalXpEarnedText.SetPosition(Bounds.Center.X, GetYPositionByIndex(1));
        _battleDurationText.SetPosition(Bounds.Center.X, GetYPositionByIndex(2));

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

    private int GetYPositionByIndex(int index)
    {
        var textHeight = 32;
        return Bounds.Y + MarginY * 3 + index * textHeight; 
    }

    #endregion
}
