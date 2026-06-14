using Domain.Dto.Global;
using Domain.Interface.Screen;

namespace Service.Screen;

public class ScreenManager : IScreenManager
{
    public IScreen ActualScreen => _screenStack.Peek();

    private readonly Stack<IScreen> _screenStack = new();
    private readonly Dictionary<string, Type> _screenList = new();

    public ScreenManager()
    {
        _screenList = GlobalVariablesDto.GetService<IEnumerable<IScreen>>().ToDictionary(x => x.ScreenCode, x => x.GetType());
    }

    public void ChangeScreen(string screenCode)
    {
        _screenStack.Clear();
        PushScreen(screenCode);
    }

    public void PopScreen()
    {
        if (_screenStack.Count >= 2)
        {
            _screenStack.Pop();
        }
    }

    public void PushScreen(string screenCode)
    {
        if (_screenList.ContainsKey(screenCode))
        {
            _screenList.TryGetValue(screenCode, out var screenType);

            var screen = (IScreen)GlobalVariablesDto.GetService(screenType);

            screen.Initialize();

            _screenStack.Push(screen);
        }
        else
        {
            throw new Exception($"Screen with code '{screenCode}' not found.");
        }
    }
}
