using Domain.Const.Text;
using Domain.Dto.Global;
using Domain.Dto.Language;
using Domain.Application.Components.Text;
using Domain.Application.MenuComponents.Frame;
using Domain.Application.Texture.Sprite.Custom.Ui.Banners;
using Infrastructure.ColorInfra;
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace RpgTurno.Custom.Component.Play.Wave;

public class WaveTransitionIndicatorComponent : FrameComponent
{
    private TextComponent _fromWaveText = new(positionXByCenter: true, positionYByCenter: true);
    private TextComponent _toWaveText = new(positionXByCenter: true, positionYByCenter: true);

    public WaveTransitionIndicatorComponent()
    {
        AnimationManager.Add(true, new PaperBannerSprite());

        AddChild(_fromWaveText);
        AddChild(_toWaveText);

        Bounds = new(0, 0, 288, 192);
    }

    public void SetWaveText(string fromWave, string toWave)
    {
        string fromWaveContent = string.IsNullOrEmpty(fromWave) 
            ? string.Empty 
            : $"{LanguageManager.Get(TextConst.Wave)} {fromWave} {LanguageManager.Get(TextConst.Cleared)}!";

        _fromWaveText.SetText(fromWaveContent);
        _toWaveText.SetText($"{LanguageManager.Get(TextConst.GoingToWave)} {toWave}...");

        SetPosition(Bounds.X, Bounds.Y);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        UpdateBounceEffect();
    }

    private void UpdateBounceEffect()
    {
        var bounce = GlobalVariablesDto.GetBounceValue(bounceAmplitude: 0.4f, bounceSpeed: 2f);

        OffsetY += bounce;
        TextColor = ColorHelper.GetFadeColor(Color.Black, Color.Red, bounce * 3);

        _fromWaveText.OffsetY = OffsetY;
        _toWaveText.OffsetY = OffsetY;
        _toWaveText.Color = TextColor;
    }

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);

        if (string.IsNullOrEmpty(_fromWaveText.Text))
        {
            _toWaveText.SetPosition(Bounds.Center.X, Bounds.Y + Bounds.Height / 2);
            return;
        }

        _fromWaveText.SetPosition(Bounds.Center.X, Bounds.Y + Bounds.Height / 3);
        _toWaveText.SetPosition(Bounds.Center.X, Bounds.Y + Bounds.Height / 3 * 2);
    }
}
