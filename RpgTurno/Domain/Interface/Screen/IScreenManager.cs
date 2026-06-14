namespace Domain.Interface.Screen;

public interface IScreenManager
{
    IScreen ActualScreen { get; }

    void ChangeScreen(string screenCode);
    void PushScreen(string screenCode);
    void PopScreen();
}
