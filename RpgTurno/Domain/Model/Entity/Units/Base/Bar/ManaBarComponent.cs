using Domain.Model.Components.ProgressBar;
using Domain.Model.Texture.Sprite.Custom.Sprite.Ui.Bars;
using Microsoft.Xna.Framework;

namespace Domain.Model.Entity.Units.Base.Bar;

public class ManaBarComponent : ProgressBarComponent
{
    public ManaBarComponent(int maxValue, int currentValue) : base(new SmallBarBlueFillSprite(), maxValue, currentValue, 16)
    {
        AnimationManager.Add(true, new SmallBarBaseSprite());

        Bounds = new Rectangle(0, 0, 120, 32);
    }
}