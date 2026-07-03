using Domain.Dto.Global;
using Domain.Interface.Transition;
using Domain.Model.Components.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Transition;

public class SideScreenTransition : IScreenTransition
{
    private float _progress;

    private bool _isClosing;
    private bool _isOpening;

    private Action _onMiddleTransition;

    public bool IsRunning => _isClosing;

    public void Start(Action onMiddleTransition)
    {
        _progress = 0;

        _isClosing = true;
        _isOpening = false;

        _onMiddleTransition = onMiddleTransition;
    }

    public void Update(GameTime gameTime)
    {
        float speed = 2.5f * GlobalVariablesDto.DeltaTime;

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
            _progress += speed;

            if (_progress >= 2f)
            {
                _progress = 0;

                _isOpening = false;
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (!IsRunning)
            return;

        int screenWidth = GlobalOptionsDto.WidthSize;
        int screenHeight = GlobalOptionsDto.HeightSize;

        Rectangle rectangle;

        if (_isClosing)
        {
            rectangle = new Rectangle(
                0,
                0,
                (int)(screenWidth * _progress),
                screenHeight);
        }
        else
        {
            float openProgress = _progress - 1f;

            rectangle = new Rectangle(
                (int)(screenWidth * openProgress),
                0,
                screenWidth,
                screenHeight);
        }

        spriteBatch.Draw(
            GlobalVariablesDto.Pixel,
            rectangle,
            Color.Black);
    }
}