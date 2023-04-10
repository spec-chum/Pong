using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Pong;

public sealed class AI : IPaddle
{
    private const int Speed = 4 * 60;

    private readonly Pong _game;
    private readonly Texture2D _texture;
    private readonly Rectangle _screenBounds;
    private Sprite _sprite;

    public Sprite Sprite => _sprite;

    public AI(Pong game, Texture2D texture)
    {
        _texture = texture;
        _game = game;
        _screenBounds = game.ScreenBounds;
        Reset();
    }

    public void Reset()
    {
        _sprite = new Sprite
        {
            Texture = _texture,
            Bounds = new Rectangle(new Point(_screenBounds.Width - (IPaddle.Width * 2), _screenBounds.Height / 2), new Point(IPaddle.Width, IPaddle.Height)),
            Color = Color.White
        };
    }

    public void Update(GameTime gameTime)
    {
        float direction = Math.Sign(_game.Ball.Sprite.Bounds.Center.Y - _sprite.Bounds.Center.Y);
        float targetY = _game.Ball.Sprite.Bounds.Center.Y + (IPaddle.Height / 2 * direction);

        float randomness = RandomHelpers.NextSingle(-1f, 1f);
        targetY += randomness;

        // Clamp and move towards target
        float speedDelta = (float)(Speed * gameTime.ElapsedGameTime.TotalSeconds);
        targetY = MathHelper.Clamp(targetY, 0, _screenBounds.Height - IPaddle.Height);
        _sprite.Y = (float)(Math.Abs(targetY - _sprite.Y) <= speedDelta ? targetY : _sprite.Y + (Math.Sign(targetY - _sprite.Y) * speedDelta));
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_sprite.Texture, _sprite.Bounds, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
    }
}