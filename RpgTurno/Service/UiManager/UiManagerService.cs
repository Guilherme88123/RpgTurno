using Domain.Interface.UiManager;
using Domain.Model.Components.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

    public void UpdateComponents(GameTime gameTime)
    {
        var enableComponents = _components.Where(x => x.IsEnable).ToList();
        enableComponents.ForEach(component => component.Update(gameTime));
    }

    #endregion

    #region Drawing

    public void DrawComponents(SpriteBatch spriteBatch)
    {
        var visibleComponents = _components.Where(x => x.IsVisible).ToList();
        visibleComponents.ForEach(component => component.Draw(spriteBatch));
    }

    #endregion
}
