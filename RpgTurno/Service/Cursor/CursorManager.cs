using Domain.Const.Cursor;
using Domain.Enum.Component.Cursor;
using Domain.Interface.Cursor;
using Domain.Model.Components.Cursor;
using Domain.Model.Texture.Sprite.Custom.Ui.Cursor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Service.Cursor;

public class CursorManager : ICursorManager
{
    public CursorStateType CursorState => _cursor.State;

    private readonly CursorComponent _cursor = new(new NormalCursorSprite(), new HoverCursorSprite(), new BlockCursorSprite());

    private CursorStateType _requestedState;
    private int _requestedPriority;

    public void BeginFrame()
    {
        _requestedState = CursorStateType.Normal;
        _requestedPriority = CursorPriority.Normal;
    }

    public void EndFrame()
    {
        _cursor.SetCursorState(_requestedState);
    }

    public void Request(CursorStateType state)
    {
        int priority = GetPriority(state);

        if (priority <= _requestedPriority)
            return;

        _requestedState = state;
        _requestedPriority = priority;
    }

    private static int GetPriority(CursorStateType state)
    {
        return state switch
        {
            CursorStateType.Normal => CursorPriority.Normal,
            CursorStateType.Hover => CursorPriority.Hover,
            CursorStateType.Block => CursorPriority.Block,
            _ => CursorPriority.Normal
        };
    }

    public void RequestHover()
    {
        Request(CursorStateType.Hover);
    }

    public void RequestBlock()
    {
        Request(CursorStateType.Block);
    }

    public void SetState(CursorStateType state)
    {
        _cursor.SetCursorState(state);
    }

    public void Update(GameTime gameTime)
    {
        _cursor.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _cursor.Draw(spriteBatch);
    }
}
