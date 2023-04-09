using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong;

public struct Sprite
{
    private Vector2 _position;
    private Vector2 _origin;
    private Vector2 _scale;
    private Rectangle _bounds;

    public Texture2D Texture { get; init; }
    public Color Color { get; set; }
    public float Rotation { get; set; }
    public SpriteEffects Effects { get; set; }
    public float LayerDepth { get; set; }
    public Rectangle Bounds => _bounds;

    public Vector2 Position
    {
        get => _position;
        set
        {
            _position = value;
            UpdateBoundsLocation();
        }
    }

    public float X
    {
        get => _position.X;
        set
        {
            _position.X = value;
            UpdateBoundsLocation();
        }
    }

    public float Y
    {
        get => _position.Y;
        set
        {
            _position.Y = value;
            UpdateBoundsLocation();
        }
    }

    public Vector2 Origin
    {
        get => _origin;
        set
        {
            _origin = value;
            UpdateBoundsLocation();
        }
    }

    public Vector2 Scale
    {
        get => _scale;
        set
        {
            _scale = value;
            UpdateBoundsSize();
        }
    }

    private void UpdateBoundsLocation()
    {
        _bounds.Location = (_position - (_scale * _origin)).ToPoint();
    }

    private void UpdateBoundsSize()
    {
        _bounds.Size = _scale.ToPoint();
    }
}