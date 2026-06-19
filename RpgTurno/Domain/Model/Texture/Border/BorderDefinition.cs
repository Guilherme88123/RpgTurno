namespace Domain.Model.Sprite.Border;

public record BorderDefinition
{
    public int Top { get; set; }
    public int Down { get; set; }
    public int Left { get; set; }
    public int Right { get; set; }

    public BorderDefinition(int top, int down, int left, int right)
    {
        Top = top; 
        Down = down; 
        Left = left; 
        Right = right;
    }
}
