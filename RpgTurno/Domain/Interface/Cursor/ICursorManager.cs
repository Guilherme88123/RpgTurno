using Domain.Enum.Component.Cursor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Interface.Cursor;

public interface ICursorManager
{
    CursorStateType CursorState { get; }

    void BeginFrame();
    void EndFrame();

    void Request(CursorStateType state);
    void RequestHover();
    void RequestBlock();
    void SetState(CursorStateType state);

    void Update(GameTime gameTime);
    void Draw(SpriteBatch spriteBatch);
}
