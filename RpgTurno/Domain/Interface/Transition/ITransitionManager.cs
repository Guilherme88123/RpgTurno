using Domain.Enum.Transition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Interface.Transition;

public interface ITransitionManager
{
    bool IsTransitionRunning { get; }

    void StartTransition(TransitionType type, Action onMiddleTransition);
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}
