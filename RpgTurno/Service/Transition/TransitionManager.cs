using Domain.Enum.Transition;
using Domain.Interface.Transition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Service.Transition;

public class TransitionManager : ITransitionManager
{
    private IScreenTransition _screenTransition;

    public bool IsTransitionRunning => _screenTransition?.IsRunning ?? false;

    public void StartTransition(TransitionType type, Action onMiddleTransition)
    {
        if (IsTransitionRunning)
            return;

        _screenTransition = ScreenTransitionFactory.Create(type);
        _screenTransition.Start(onMiddleTransition);
    }

    public void Update(GameTime gameTime)
    {
        _screenTransition?.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _screenTransition?.Draw(spriteBatch);
    }
}
