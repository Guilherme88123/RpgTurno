using Domain.Application.Components.ProgressBar;
using Domain.Application.Texture.Sprite.Custom.Ui.Bars;
using Microsoft.Xna.Framework;

namespace Domain.Application.Entity.Units.Base.Bar;

public class HealthBarComponent : ProgressBarComponent
{
    public HealthBarComponent(int maxValue, int currentValue) : base(new SmallBarRedFillSprite(), maxValue, currentValue, 16)
    {
        AnimationManager.Add(true, new SmallBarBaseSprite());

        Bounds = new Rectangle(0, 0, 120, 32);
    }
}