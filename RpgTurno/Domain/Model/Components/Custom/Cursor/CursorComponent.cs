using Domain.Const.Sprite;
using Domain.Dto.Global;
using Domain.Enum.Component.Cursor;
using Domain.Model.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Domain.Model.Components.Custom.Cursor;

public class CursorComponent
{
    private readonly AnimationManager _animationManager;
    private readonly int _hotspotX;
    private readonly int _hotspotY;
    private readonly int _size;

    private CursorStateType _state;

    public CursorComponent()
    {
        var normal = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.NormalCursor);
        var hover = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.HoverCursor);
        var block = GlobalVariablesDto.Content.Load<Texture2D>(SpriteConst.BlockCursor);

        _animationManager = new();
        _animationManager.AddAnimation(CursorStateType.Normal, new AnimationClip(normal));
        _animationManager.AddAnimation(CursorStateType.Hover, new AnimationClip(hover));
        _animationManager.AddAnimation(CursorStateType.Block, new AnimationClip(block));

        _state = CursorStateType.Normal;
        _size = normal.Width;
    }

    public void Draw()
    {
        _animationManager.Update(_state);

        var mouse = GlobalVariablesDto.MouseState;
        var cursorRectangle = new Rectangle(mouse.X - _hotspotX, mouse.Y - _hotspotY, _size, _size);

        _animationManager.Draw(cursorRectangle, Color.White, 0f, SpriteEffects.None, GlobalVariablesDto.SpriteBatchInterface);
    }

    public void SetCursorState(CursorStateType state)
    {
        _state = state;
    }
}
