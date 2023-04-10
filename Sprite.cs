using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong;

public struct Sprite
{
    private Rectangle _bounds;

    public Texture2D Texture { get; init; }
    public Color Color { get; set; }

    public Vector2 Position
    {
        get => _bounds.Location.ToVector2();
        set => _bounds.Location = value.ToPoint();
    }

    public Rectangle Bounds
    {
        get => _bounds;
        set => _bounds = value;
    }

    public float X
    {
        get => _bounds.X;
        set => _bounds.X = (int)value;
    }

    public float Y
    {
        get => _bounds.Y;
        set => _bounds.Y = (int)value;
    }
}