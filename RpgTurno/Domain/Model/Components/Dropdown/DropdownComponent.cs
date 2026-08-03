using Domain.Dto.Components.Dropdown;
using Domain.Dto.Global;
using Domain.Enum.Component.Button;
using Domain.Model.Components.Base;
using Domain.Model.Components.Dropdown;
using Domain.Model.Components.Text;
using Domain.Model.Particle;
using Domain.Model.Sound.Base;
using Domain.Model.Sound.Ui;
using Domain.Model.Texture.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Application.Model.MenuElements.Dropdown;

public class DropdownComponent : BaseComponent
{
    public ButtonInteractionState State { get; set; }

    public bool IsOpen { get; set; }

    private List<DropdownItemComponent> ListItens { get; set; } = new();
    public List<DropdownItemDto> ListItensDto { get; set; } = new();
    public int SelectedItemIndex { get; set; }

    public SpriteData OptionsOverlaySprite { get; set; }

    public Action<DropdownItemDto> ValueUpdate { get; set; }

    private const float DelayPressed = 0.2f;
    private float _currentDelay = DelayPressed;

    private readonly SoundEffectData ClickSoundEffect = new ButtonClickSoundEffect();
    private readonly SoundEffectData HoverSoundEffect = new ButtonHoverSoundEffect();

    public readonly TextComponent Text = new(positionXByCenter: true, positionYByCenter: true);
    private string _baseText;

    private const int SelectedIndicatorSize = 28;
    public SpriteData SelectedIndicatorSprite { get; set; }

    private readonly ParticleEmitterModel _particleEmitter = new();

    public DropdownComponent(List<DropdownItemDto> options)
    {
        InitializeOptions(options);

        HoverState.OnHoverIn += OnHoverIn;

        HoverAnimation.AffectScaleX = true;
        HoverAnimation.AffectScaleY = true;
        HoverAnimation.AffectOffsetY = true;
        HoverAnimation.AffectTextColor = true;
    }

    private void InitializeOptions(List<DropdownItemDto> options)
    {
        ListItensDto = options;

        ListItens.Clear();

        foreach (var option in ListItensDto)
            ListItens.Add(new DropdownItemComponent(this, option));
    }

    #region Update

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        Text.Update(gameTime);
        Text.Color = TextColor;
        Text.OffsetY = OffsetY;

        _particleEmitter.Update();

        AnimationManager.Update(State);

        if (IsOpen)
            UpdateOptions(gameTime);

        if (State == ButtonInteractionState.Pressed)
        {
            UpdatePressedDelay();
            return;
        }

        if (!CanClick())
            return;

        CursorManager.RequestHover();

        if (IsTryingClick())
            ExecuteClick();
    }

    private bool CanClick()
    {
        if (!IsVisible)
            return false;

        if (State == ButtonInteractionState.Pressed)
            return false;

        if (!HoverState.IsHover)
            return false;

        if (GlobalVariablesDto.PreviousMouseDown)
            return false;

        return true;
    }

    private bool IsTryingClick()
    {
        return GlobalVariablesDto.MouseState.LeftButton == ButtonState.Pressed;
    }

    private void ExecuteClick()
    {
        State = ButtonInteractionState.Pressed;
        ClickSoundEffect?.Play();
        _particleEmitter.Emit(GlobalVariablesDto.MousePoint, Color);

        _currentDelay = DelayPressed;

        SetPositionText();
    }

    private void UpdatePressedDelay()
    {
        _currentDelay -= GlobalVariablesDto.DeltaTime;

        if (_currentDelay < 0)
        {
            ToggleOpen();
            ReloadText();

            State = ButtonInteractionState.Regular;
            AnimationManager.Update(State);
            SetPositionText();
        }
    }

    private void ToggleOpen()
    {
        IsOpen = !IsOpen;

        if (IsOpen)
            UpdateOptionsRectangle();
    }

    private void UpdateOptions(GameTime gameTime)
    {
        ListItens.ForEach(x => x.Update(gameTime));
    }

    public void SelectItem(int id)
    {
        if (SelectedItemIndex != id)
        {
            var item = ListItensDto.First(x => x.Id == id);
            ValueUpdate?.Invoke(item);
        }

        SelectedItemIndex = id;

        ReloadText();
        ToggleOpen();
    }

    private void UpdateOptionsRectangle()
    {
        var border = 32;
        var optionHeight = Bounds.Height / 2;

        foreach (var item in ListItens)
        {
            var x = Bounds.X + border;
            var y = Bounds.Bottom + optionHeight * item.Id;
            var width = Bounds.Width - border * 2;

            item.SetBounds(width, optionHeight);
            item.SetPosition(x, y);
        }
    }

    #endregion

    #region Draw

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        Text.Draw(spriteBatch);

        if (IsOpen)
        {
            DrawDropdownOverlay(spriteBatch);
            DrawDropdownItems(spriteBatch);
        }

        _particleEmitter.Draw();
    }


    private void DrawDropdownItems(SpriteBatch spriteBatch)
    {
        foreach (var item in ListItens)
        {
            item.Draw(spriteBatch);

            if (item.Id == SelectedItemIndex && SelectedIndicatorSprite is not null)
                DrawSelectedOptionIndicator(spriteBatch, item);
        }
    }

    private void DrawSelectedOptionIndicator(SpriteBatch spriteBatch, DropdownItemComponent selectedOption)
    {
        Rectangle spriteRect = new(
            selectedOption.Bounds.X + SelectedIndicatorSize, 
            selectedOption.Bounds.Center.Y - SelectedIndicatorSize / 2 - 5, 
            SelectedIndicatorSize, 
            SelectedIndicatorSize);

        SelectedIndicatorSprite.Draw(spriteRect, Color.White, 0f, SpriteEffects.None, spriteBatch, Vector2.One, Vector2.Zero);
    }

    private void DrawDropdownOverlay(SpriteBatch spriteBatch)
    {
        if (OptionsOverlaySprite is null || ListItens.Count == 0)
            return;

        var exampleItem = ListItens.First();

        var heightGap = exampleItem.Bounds.Height / 2;

        var x = exampleItem.Bounds.Left;
        var y = Bounds.Bottom - heightGap;
        var width = exampleItem.Bounds.Width;
        var height = ListItens.Sum(x => x.Bounds.Height) + heightGap * 2;

        var overlayRectangle = new Rectangle(x, y, width, height);

        OptionsOverlaySprite.Draw(overlayRectangle, Color.White, 0f, SpriteEffects.None, spriteBatch, Vector2.One, Vector2.Zero);
    }

    #endregion

    #region Position

    public override void SetPosition(int positionX, int positionY)
    {
        base.SetPosition(positionX, positionY);
        SetPositionText(positionX, positionY);
    }

    private void SetPositionText()
    {
        SetPositionText(Bounds.X, Bounds.Y);
    }

    private void SetPositionText(int positionX, int positionY)
    {
        if (State == ButtonInteractionState.Pressed)
            positionY += 10;

        Text.SetPosition(positionX + Bounds.Width / 2, positionY + Bounds.Height / 2);
    }

    #endregion

    #region Text

    public void SetText(string text)
    {
        _baseText = text;
        ReloadText();
    }

    public void ReloadText()
    {
        Text.SetText(GetText());
        SetPositionText();
    }

    protected string GetText()
    {
        var optionSelected = ListItensDto.FirstOrDefault(x => x.Id == SelectedItemIndex);

        if (optionSelected is null)
            return $"{_baseText}: N/A";

        return $"{_baseText}: {optionSelected.Text}";
    }

    #endregion

    #region Hover

    private void OnHoverIn()
    {
        HoverSoundEffect.Play();
        _particleEmitter.Emit(GlobalVariablesDto.MousePoint, Color, 2);
    }

    #endregion
}