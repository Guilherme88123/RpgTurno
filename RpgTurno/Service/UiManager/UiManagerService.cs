using Application.Model.MenuElements.Base;
using Domain.Interface.UiManager;

namespace Service.UiManager;

public class UiManagerService : IUiManagerService
{
    private readonly List<BaseComponent> _components = new();

    #region Adding

    public void AddComponent(BaseComponent component)
    {
        _components.Add(component);
    }

    public void AddComponent(List<BaseComponent> components)
    {
        _components.AddRange(components);
    }

    #endregion

    #region Clearing

    public void ClearComponents()
    {
        _components.Clear();
    }

    #endregion

    #region Updating

    public void UpdateComponents()
    {
        _components.ForEach(component => component.Update());
    }

    #endregion

    #region Drawing

    public void DrawComponents()
    {
        _components.ForEach(component => component.Draw());
    }

    #endregion
}
