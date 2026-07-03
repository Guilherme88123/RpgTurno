using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Interface.Transition;

public interface IScreenTransition
{
    bool IsRunning { get; }

    void Start(Action middleAction);
    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}
