using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Texture.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Entity.Base;

/// <summary>
/// Classe de entidade base (comum) para todas outras entidades da solução
/// </summary>
public class BaseEntity
{
    public int SizeX { get; set; } = 128;
    public int SizeY { get; set; } = 128;

    public int AnimationSizeX { get; set; } = 128;
    public int AnimationSizeY { get; set; } = 128;

    public Color Color { get; set; } = Color.White;

    public float PositionX { get; set; }
    public float PositionY { get; set; } 

    public DirectionType Direction { get; set; } = DirectionType.Right;

    public float ActualAngle { get; set; }
    public SpriteEffects DrawEffect { get; set; } = SpriteEffects.None;

    public bool IsDestroyed { get; set; }

    public Vector2 Center => new Vector2(PositionX + SizeX / 2, PositionY + SizeY / 2);

    public AnimationManager Animation { get; set; } = new();
    public Rectangle Rectangle => new Rectangle((int)PositionX, (int)PositionY, SizeX, SizeY);
    public Rectangle AnimationRectangle => GetAnimationRectangle();

    public virtual int FeetPadding => 0;
    public virtual Point FeetOffset => new(AnimationRectangle.Width / 2, AnimationRectangle.Height - FeetPadding);

    public CreatureStateType CreatureState;

    public virtual void Update()
    {
        UpdateDirectionDraw();

        Animation.Update(CreatureState);
    }

    private void UpdateDirectionDraw()
    {
        DrawEffect = Direction == DirectionType.Left
        ? SpriteEffects.FlipHorizontally
        : SpriteEffects.None;
    }

    public virtual void Draw()
    {
        if (Animation.IsEmpty)
        {
            GlobalVariablesDto.SpriteBatchEntities.Draw(GlobalVariablesDto.Pixel, Rectangle, Color);
            return;
        }

        Animation.Draw(AnimationRectangle, Color, ActualAngle, DrawEffect, GlobalVariablesDto.SpriteBatchEntities, Vector2.One, Vector2.Zero);
    }

    public virtual void Destroy()
    {
        IsDestroyed = true;
    }

    protected virtual Rectangle GetAnimationRectangle()
    {
        var animationPosition = GetAnimationPosition();

        return new Rectangle(
            animationPosition.X,
            animationPosition.Y,
            AnimationSizeX,
            AnimationSizeY);
    }

    private Point GetAnimationPosition()
    {
        var rectangleDiferenceX = AnimationSizeX - SizeX;
        var rectangleDiferenceY = AnimationSizeY - SizeY;

        var subtractAmountX = rectangleDiferenceX / 2;
        var subtractAmountY = rectangleDiferenceY / 2;

        var animationPositionX = PositionX - subtractAmountX;
        var animationPositionY = PositionY - subtractAmountY;

        return new Point((int)animationPositionX, (int)animationPositionY);
    }
}
