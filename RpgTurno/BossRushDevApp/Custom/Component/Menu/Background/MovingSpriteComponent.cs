using Domain.Dto.Global;
using Domain.Enum;
using Domain.Model.Components.Image;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RpgTurno.Custom.Component.Menu.Background;

public class MovingSpriteComponent : ImageComponent
{
    public bool IsDestroyed { get; private set; }

    private readonly float _speed;
    private readonly DirectionType _direction;
    private readonly int _width;

    private float _positionX;

    public MovingSpriteComponent(SpriteData sprite, float speed, DirectionType direction, int positionX, int positionY) : base(sprite, sprite.Width, sprite.Height)
    {
        _speed = speed;
        _direction = direction;
        _width = sprite.Width;

        if (_direction == DirectionType.Left)
            SpriteEffects = SpriteEffects.FlipHorizontally;

        SetPosition(positionX, positionY);

        _positionX = Bounds.X;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        UpdatePostion(gameTime);

        if (IsOutOfScreen())
            MarkAsDestroyed();
    }

    private void UpdatePostion(GameTime gameTime)
    {
        _positionX += _speed * (int)_direction * (float)gameTime.ElapsedGameTime.TotalSeconds;

        SetPosition((int)_positionX, Bounds.Y);
    }

    private bool IsOutOfScreen()
    {
        return IsLimitBreak(GetSpriteLimit(), GetScreenLimit());
    }

    private int GetSpriteLimit()
    {
        return _direction == DirectionType.Left ? Bounds.Right : Bounds.Left;
    }

    private int GetScreenLimit()
    {
        return _direction == DirectionType.Left ? 0 : GlobalOptionsDto.WidthSize;
    }

    private bool IsLimitBreak(int spriteLimit, int screenLimit)
    {
        if (_direction == DirectionType.Left)
            return spriteLimit < screenLimit;

        return spriteLimit > screenLimit;
    }

    private void MarkAsDestroyed()
    {
        IsDestroyed = true;
    }
}
