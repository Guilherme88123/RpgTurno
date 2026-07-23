using Domain.Dto.Global;
using Microsoft.Xna.Framework;

namespace Domain.Model.Components.Base.Hover;

public class HoverAnimation
{
    public bool AffectScaleX { get; set; }
    public bool AffectScaleY { get; set; }
    public bool AffectRotation { get; set; }
    public bool AffectColor { get; set; }
    public bool AffectTextColor { get; set; }
    public bool AffectOffsetX { get; set; }
    public bool AffectOffsetY { get; set; }

    public float DefaultScaleX { get; set; } = 1f;
    public float DefaultScaleY { get; set; } = 1f;
    public float DefaultRotation { get; set; } = MathHelper.ToRadians(0);
    public float DefaultOffsetX { get; set; } = 0;
    public float DefaultOffsetY { get; set; } = 0;
    public Color DefaultColor { get; set; } = Color.White;
    public Color DefaultTextColor { get; set; } = Color.Black;

    public float HoverScaleX { get; set; } = 1.2f;
    public float HoverScaleY { get; set; } = 1.2f;
    public float HoverRotation { get; set; } = MathHelper.ToRadians(30);
    public float HoverOffsetX { get; set; } = -5;
    public float HoverOffsetY { get; set; } = -5;
    public Color HoverColor { get; set; } = Color.Yellow;
    public Color HoverTextColor { get; set; } = Color.Yellow;

    public float AnimationSpeed { get; set; } = 12f;

    private float _progress;

    public void Update(BaseComponent component, GameTime gameTime)
    {
        float deltaGameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (component.HoverState.IsHover)
            _progress += AnimationSpeed * deltaGameTime;
        else
            _progress -= AnimationSpeed * deltaGameTime;

        _progress = Math.Clamp(_progress, 0f, 1f);

        Apply(component);
    }

    private void Apply(BaseComponent component)
    {
        if (AffectScaleX)
            ApplyScaleX(component);

        if (AffectScaleY)
            ApplyScaleY(component);

        if (AffectRotation)
            ApplyRotation(component);

        if (AffectOffsetX)
            ApplyOffsetX(component);

        if (AffectOffsetY)
            ApplyOffsetY(component);

        if (AffectColor)
            ApplyColor(component);

        if (AffectTextColor)
            ApplyTextColor(component);
    }

    private void ApplyScaleX(BaseComponent component)
    {
        component.ScaleX = MathHelper.Lerp(DefaultScaleX, HoverScaleX, _progress);
    }

    private void ApplyScaleY(BaseComponent component)
    {
        component.ScaleY = MathHelper.Lerp(DefaultScaleY, HoverScaleY, _progress);
    }

    private void ApplyRotation(BaseComponent component)
    {
        component.Rotation = MathHelper.Lerp(DefaultRotation, HoverRotation, _progress);
    }

    private void ApplyOffsetX(BaseComponent component)
    {
        component.OffsetX = MathHelper.Lerp(DefaultOffsetX, HoverOffsetX, _progress);
    }

    private void ApplyOffsetY(BaseComponent component)
    {
        component.OffsetY = MathHelper.Lerp(DefaultOffsetY, HoverOffsetY, _progress);
    }

    private void ApplyColor(BaseComponent component)
    {
        component.Color = new Color(
            (int)MathHelper.Lerp(DefaultColor.R, HoverColor.R, _progress),
            (int)MathHelper.Lerp(DefaultColor.G, HoverColor.G, _progress),
            (int)MathHelper.Lerp(DefaultColor.B, HoverColor.B, _progress),
            (int)MathHelper.Lerp(DefaultColor.A, HoverColor.A, _progress));
    }

    private void ApplyTextColor(BaseComponent component)
    {
        component.TextColor = new Color(
            (int)MathHelper.Lerp(DefaultTextColor.R, HoverTextColor.R, _progress),
            (int)MathHelper.Lerp(DefaultTextColor.G, HoverTextColor.G, _progress),
            (int)MathHelper.Lerp(DefaultTextColor.B, HoverTextColor.B, _progress),
            (int)MathHelper.Lerp(DefaultTextColor.A, HoverTextColor.A, _progress));
    }
}
