using Domain.Dto.Global;
using Domain.Model.Components.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Application.Model.MenuElements.Dropdown;

//TODO: Refatorar Dropdown quando tiver uma oportunidade de testar
public class DropdownComponent : BaseComponent
{
    public string Text { get; set; }
    public bool IsOpen { get; set; }

    public List<DropdownItemDto> ListItens { get; set; }
    public int SelectedItem { get; set; }
    public Action<DropdownItemDto> ValueUpdate { get; set; }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        var mouse = Mouse.GetState();

        bool botaoPressionado = mouse.LeftButton == ButtonState.Pressed;

        if (botaoPressionado && !GlobalVariablesDto.PreviousMouseDown && HoverState.IsHover)
        {
            ToggleOpen();
        }

        UpdateOptions();
    }

    private void UpdateOptions()
    {
        var mouse = Mouse.GetState();
        var mousePos = new Point(mouse.X, mouse.Y);

        foreach (var item in ListItens)
        {
            item.IsHover = item.Rectangle.Contains(mousePos);

            bool botaoPressionado = mouse.LeftButton == ButtonState.Pressed;

            if (botaoPressionado && !GlobalVariablesDto.PreviousMouseDown && item.IsHover)
            {
                ToggleOpen();
                SelectItem(item.Id);
            }
        }
    }

    private void ToggleOpen()
    {
        IsOpen = !IsOpen;

        if (IsOpen) UpdateOptionsRectangle();
    }

    private void SelectItem(int id)
    {
        if (SelectedItem != id)
        {
            var item = ListItens.First(x => x.Id == id);
            ValueUpdate?.Invoke(item);
        }

        SelectedItem = id;
    }

    private void UpdateOptionsRectangle()
    {
        var border = 30;

        foreach (var item in ListItens)
        {
            var x = Bounds.X + border;
            var y = Bounds.Y + Bounds.Height + Bounds.Height / 2 * item.Id;
            var width = Bounds.Width - border * 2;
            var height = Bounds.Height / 2;

            item.Rectangle = new(x, y, width, height);
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);

        if (IsOpen)
        {
            DrawDropdownOverlay(spriteBatch);
            DrawDropdownItems(spriteBatch);
        }
    }

    protected string GetText()
    {
        var optionSelected = ListItens.FirstOrDefault(x => x.Id == SelectedItem);

        if (optionSelected is null) return $"{Text}: N/A";

        return $"{Text}: {optionSelected.Text}";
    }

    private void DrawDropdownItems(SpriteBatch spriteBatch)
    {
        foreach (var item in ListItens)
        {
            var textSize = GlobalVariablesDto.FontThickPixels.MeasureString(item.Text);

            var x = item.Rectangle.X + item.Rectangle.Width / 2 - textSize.X / 2;
            var y = item.Rectangle.Y + item.Rectangle.Height / 2 - textSize.Y / 2;

            spriteBatch.DrawString(GlobalVariablesDto.FontThickPixels, item.Text, new(x, y), Color.White);
        }
    }

    private void DrawDropdownOverlay(SpriteBatch spriteBatch)
    {
        foreach (var item in ListItens)
        {
            var color = item.IsHover ? Color.DarkGray * 0.7f : Color.DarkGray;
            spriteBatch.Draw(GlobalVariablesDto.Pixel, item.Rectangle, color);
        }
    }
}

public class DropdownItemDto
{
    public int Id { get; set; }
    public string Text { get; set; }
    public Rectangle Rectangle { get; set; }
    public bool IsHover { get; set; }
    public object Value { get; set; }
}