using Application.Model.MenuElements.Base;

namespace Domain.Model.MenuComponents.Frame;

public class FrameComponent : BaseComponent
{
    private List<BaseComponent> _children = new();

    public FrameComponent()
    {
        CanHover = false;
    }

    public void AddChild(BaseComponent child)
    {
        _children.Add(child);
    }

    public override void Update()
    {
        FixVisibilityByFather();

        base.Update();
        _children.ForEach(child => child.Update());
    }

    private void FixVisibilityByFather()
    {
        _children.ForEach(child => child.IsVisible = IsVisible);
    }

    public override void Draw()
    {
        base.Draw();
        _children.ForEach(child => child.Draw());
    }
}
