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

    public Player(Pong game, Texture2D texture)
    {
        _texture = texture;
        _screenBounds = game.ScreenBounds;
        Reset();
    }

    public void Reset()
    {
        _sprite = new Sprite
        {
            Texture = _texture,
            Bounds = new Rectangle(new Point(IPaddle.Width * 2, _screenBounds.Height / 2), new Point(IPaddle.Width, IPaddle.Height)),
            Color = Color.White
        };
    }

    public void Update()
    {
        var ms = Mouse.GetState();

        // Clamp and make mouse Y pos middle of paddle
        _sprite.Y = MathHelper.Clamp(ms.Y - (IPaddle.Height / 2f), 0, _screenBounds.Height - IPaddle.Height);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_sprite.Texture, _sprite.Bounds, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
    }
}