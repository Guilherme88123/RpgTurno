using Domain.Dto.Global;
using Domain.Interface.Transition;
using Domain.Model.Components.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Transition;

public class CircleScreenTransition : IScreenTransition
{
    private float _progress;
    private bool _isClosing;
    private bool _isOpening;
    private Action _onMiddleTransition;

    public bool IsRunning => _isClosing || _isOpening;

    public void Start(Action onMiddleTransition)
    {
        _progress = 0;

        _isClosing = true;
        _isOpening = false;

        _onMiddleTransition = onMiddleTransition;
    }

    public void Update(GameTime gameTime)
    {
        float speed = 1f * GlobalVariablesDto.DeltaTime;

        if (_isClosing)
        {
            _progress += speed;

            if (_progress >= 1f)
            {
                _progress = 1f;
                _isClosing = false;

                _onMiddleTransition?.Invoke();
                _onMiddleTransition = null;

                _isOpening = true;
            }
        }
        else if (_isOpening)
        {
            _progress -= speed;

            if (_progress <= 0f)
            {
                _progress = 0f;
                _isOpening = false;
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!IsRunning)
            return;

        int width = GlobalOptionsDto.WidthSize;
        int height = GlobalOptionsDto.HeightSize;

        Vector2 center = new(width / 2f, height / 2f);

        float maxRadius = MathF.Sqrt(width * width + height * height);

        float radius = MathHelper.Lerp(maxRadius, 0, _progress);

        DrawCircleMask(spriteBatch, center, radius);
    }

    private void DrawCircleMask(SpriteBatch spriteBatch, Vector2 center, float radius)
    {
        int width = GlobalOptionsDto.WidthSize;
        int height = GlobalOptionsDto.HeightSize;

        int left = (int)(center.X - radius);
        int right = (int)(center.X + radius);

        spriteBatch.Draw(
            GlobalVariablesDto.Pixel,
            new Rectangle(0, 0, width, Math.Max(0, (int)(center.Y - radius))),
            Color.Black);

        spriteBatch.Draw(
            GlobalVariablesDto.Pixel,
            new Rectangle(0, (int)(center.Y + radius), width,
                Math.Max(0, height - (int)(center.Y + radius))),
            Color.Black);

        if (left > 0)
        {
            spriteBatch.Draw(
                GlobalVariablesDto.Pixel,
                new Rectangle(
                    0,
                    (int)(center.Y - radius),
                    left,
                    (int)(radius * 2)),
                Color.Black);
        }

        if (right < width)
        {
            spriteBatch.Draw(
                GlobalVariablesDto.Pixel,
                new Rectangle(
                    right,
                    (int)(center.Y - radius),
                    width - right,
                    (int)(radius * 2)),
                Color.Black);
        }

        for (int y = -(int)radius; y <= radius; y++)
        {
            float x = MathF.Sqrt(radius * radius - y * y);

            int circleLeft = (int)(center.X - x);
            int circleRight = (int)(center.X + x);

            int screenY = (int)center.Y + y;

            if (screenY < 0 || screenY >= height)
                continue;

            if (circleLeft > 0)
            {
                spriteBatch.Draw(
                    GlobalVariablesDto.Pixel,
                    new Rectangle(0, screenY, circleLeft, 1),
                    Color.Black);
            }

            if (circleRight < width)
            {
                spriteBatch.Draw(
                    GlobalVariablesDto.Pixel,
                    new Rectangle(circleRight, screenY, width - circleRight, 1),
                    Color.Black);
            }
        }
    }
}