using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Pong;

public sealed class Ball
{
    private const int Size = 16;
    private const float MinVelocity = 60f;

    private readonly Pong _game;
    private readonly Texture2D _texture;
    private readonly Rectangle _screenBounds;
    private Sprite _sprite;
    private Vector2 _velocity;
    private float _speed = 400;

    public Sprite Sprite => _sprite;

    public Ball(Pong game, Texture2D texture)
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
            Bounds = new Rectangle(_screenBounds.Center, new Point(Size)),
            Color = Color.White,
        };

        float angle = RandomHelpers.NextSingle(-1, 1) * (MathF.PI / 8f);
        _velocity.X = _speed * MathF.Cos(angle);
        _velocity.Y = _speed * MathF.Sin(angle);
    }

    public void Update(GameTime gameTime)
    {
        if (_sprite.Bounds.Left <= _screenBounds.Left)
        {
            _game.AIScore++;
            _speed = 400;
            _game.Reset();
        }
        else if (_sprite.Bounds.Right >= _screenBounds.Right)
        {
            _game.PlayerScore++;
            _speed = 400;
            _game.Reset();
        }

        if (_sprite.Bounds.Top <= _screenBounds.Top)
        {
            _sprite.Y = 0;
            _velocity.Y *= -1;

            // make sure we don't stick to top
            _velocity.Y = Math.Max(MinVelocity, _velocity.Y);
        }
        else if (_sprite.Bounds.Bottom >= _screenBounds.Bottom)
        {
            _sprite.Y = _screenBounds.Bottom - _sprite.Bounds.Height;
            _velocity.Y *= -1;

            // make sure we don't stick to bottom
            _velocity.Y = Math.Min(-MinVelocity, _velocity.Y);
        }

        if (_sprite.Bounds.Intersects(_game.Player.Sprite.Bounds))
        {
            _sprite.X = _game.Player.Sprite.Bounds.Right;
            BounceOffPaddle(_game.Player);
        }
        else if (_sprite.Bounds.Intersects(_game.AI.Sprite.Bounds))
        {
            _sprite.X = _game.AI.Sprite.Bounds.Left - _sprite.Bounds.Width;
            BounceOffPaddle(_game.AI);
        }

        _sprite.Position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    private void BounceOffPaddle(IPaddle paddle)
    {
        float yDiff = _sprite.Bounds.Center.Y - paddle.Sprite.Bounds.Center.Y;
        float normalizedYDiff = yDiff / (IPaddle.Height / 2);
        float angle = normalizedYDiff * (MathF.PI / 4f);

        float randomness = RandomHelpers.NextSingle(-0.1f, 0.1f);
        angle += randomness;

        // Slowly increase speed and calculate new velocity
        _speed += 10f;
        _velocity.X = _speed * MathF.Cos(angle) * MathF.Sign(-_velocity.X);
        _velocity.Y = _speed * MathF.Sin(angle);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_sprite.Texture, _sprite.Bounds, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
    }
}