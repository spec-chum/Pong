using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong;

public sealed class Player : IPaddle
{
    private readonly Texture2D _texture;
    private readonly Rectangle _screenBounds;
    private Sprite _sprite;

    public Sprite Sprite => _sprite;

    public Player(Pong gameManager, Texture2D texture)
    {
        _texture = texture;
        _screenBounds = gameManager.ScreenBounds;
        Reset();
    }

    public void Reset()
    {
        _sprite = new Sprite
        {
            Texture = _texture,
            Position = new Vector2(IPaddle.Width * 2, _screenBounds.Height / 2),
            Origin = new Vector2(0.5f),
            Scale = new Vector2(IPaddle.Width, IPaddle.Height),
            Color = Color.White
        };
    }

    public void Update()
    {
        var ms = Mouse.GetState();
        _sprite.Y = MathHelper.Clamp((float)ms.Y, IPaddle.Height / 2, _screenBounds.Height - IPaddle.Height / 2);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_sprite.Texture, _sprite.Position, null, _sprite.Color, _sprite.Rotation, _sprite.Origin, _sprite.Scale, _sprite.Effects, _sprite.LayerDepth);
    }
}