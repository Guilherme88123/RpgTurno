using Microsoft.Xna.Framework;

namespace Domain.Interface.Screen;

public interface IScreen
{
    public string ScreenCode { get; }

    void Initialize();
    void Update();
    void Draw();
    void Exit();
}
