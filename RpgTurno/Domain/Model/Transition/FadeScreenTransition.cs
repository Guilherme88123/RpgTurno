using Domain.Dto.Global;
using Domain.Interface.Transition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Transition;

public class FadeScreenTransition : IScreenTransition
{
    private float _alpha;
    private bool _isClosing;
    private bool _isOpening;
    private Action _middleAction;

    public bool IsRunning => _isClosing || _isOpening;

    public void Start(Action middleAction)
    {
        _alpha = 0;
        _isClosing = true;
        _middleAction = middleAction;
    }

    public void Update(GameTime gameTime)
    {
        float speed = 3f * GlobalVariablesDto.DeltaTime;

        if (_isClosing)
        {
            _alpha += speed;

            if (_alpha >= 1)
            {
                _alpha = 1;
                _isClosing = false;

                _middleAction?.Invoke();
                _middleAction = null;

                _isOpening = true;
            }
        }

        if (_isOpening)
        {
            _alpha -= speed;

            if (_alpha <= 0)
            {
                _alpha = 0;
                _isOpening = false;
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            GlobalVariablesDto.Pixel,
            new Rectangle(0, 0, GlobalOptionsDto.WidthSize, GlobalOptionsDto.HeightSize),
            Color.Black * _alpha);
    }
}
