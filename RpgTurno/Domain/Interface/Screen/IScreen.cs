using Microsoft.Xna.Framework;

namespace Domain.Interface.Screen;

public interface IScreen
{
    public string ScreenCode { get; }

    void Initialize();
    void OnGoTo(string originScreenCode);
    void Update(GameTime gameTime);
    void Draw();
    void Exit();
}
