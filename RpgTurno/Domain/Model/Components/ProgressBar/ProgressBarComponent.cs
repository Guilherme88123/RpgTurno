using Domain.Model.Components.Base;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Components.ProgressBar;

public class ProgressBarComponent : BaseComponent
{
    public int MaxValue { get; private set; }
    public int CurrentValue { get; private set; }

    private readonly SpriteData _fillSprite;

    private readonly int _fixedSlice;

    public ProgressBarComponent(SpriteData fillSprite, int maxValue, int currentValue, int fixedSlice = 0)
    {
        _fillSprite = fillSprite;
        MaxValue = maxValue;
        CurrentValue = currentValue;
        _fixedSlice = fixedSlice;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        DrawFillSprite(spriteBatch);
    }

    private void DrawFillSprite(SpriteBatch spriteBatch)
    {
        var fillRectangle = GetFillRectangle();

        _fillSprite.Draw(fillRectangle, Color, Rotation, SpriteEffects, spriteBatch);
    }

    private Rectangle GetFillRectangle()
    {
        //TODO: Revisar, pois aparentemente o tamanho do Fill está bugado - X
        float percent = (float)CurrentValue / MaxValue;
        int fillWidth = (int)((Bounds.Width - _fixedSlice) * percent);

        return new Rectangle(
            Bounds.X + _fixedSlice / 2, 
            Bounds.Y, 
            fillWidth, 
            Bounds.Height);
    }

    public void SetValues(int maxValue, int currentValue)
    {
        MaxValue = maxValue;
        CurrentValue = currentValue;
    }
}
