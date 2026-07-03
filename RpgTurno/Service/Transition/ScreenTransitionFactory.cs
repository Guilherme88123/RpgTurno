using Domain.Enum.Transition;
using Domain.Interface.Transition;
using Domain.Model.Transition;

namespace Service.Transition;

public static class ScreenTransitionFactory
{
    public static IScreenTransition Create(TransitionType type)
    {
        return type switch
        {
            TransitionType.Fade => new FadeScreenTransition(),
            TransitionType.Side => new SideScreenTransition(),
            TransitionType.Circle => new CircleScreenTransition(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
