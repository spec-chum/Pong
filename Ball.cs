using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Pong;

public sealed class Ball
{
    private const int Size = 16;
    private const float Speed = 400;

    private readonly Pong _game;
    private readonly Texture2D _texture;
    private readonly Rectangle _screenBounds;
    private Sprite _sprite;
    private Vector2 _velocity;

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
            Position = _screenBounds.Center.ToVector2(),
            Origin = new Vector2(0.5f),
            Scale = new Vector2(Size),
            Color = Color.White,
        };

        float angle = RandomHelpers.NextSingle(-1, 1) * (MathF.PI / 8f);
        _velocity.X = Speed * MathF.Cos(angle);
        _velocity.Y = Speed * MathF.Sin(angle);
    }

    public void Update(GameTime gameTime)
    {
        const int halfSize = Size / 2;

        if (_sprite.Bounds.Left <= _screenBounds.Left)
        {
            _game.AIScore++;
            _game.Reset();
        }
        else if (_sprite.Bounds.Right >= _screenBounds.Right)
        {
            _game.PlayerScore++;
            _game.Reset();
        }

        if (_sprite.Bounds.Top <= _screenBounds.Top)
        {
            _sprite.Y = halfSize;
            _velocity.Y *= -1;
        }
        else if (_sprite.Bounds.Bottom >= _screenBounds.Bottom)
        {
            _sprite.Y = _screenBounds.Bottom - halfSize;
            _velocity.Y *= -1;
        }

        if (_sprite.Bounds.Intersects(_game.Player.Sprite.Bounds))
        {
            _sprite.X = _game.Player.Sprite.Bounds.Right + halfSize;
            BounceOffPaddle(_game.Player);
        }
        else if (_sprite.Bounds.Intersects(_game.AI.Sprite.Bounds))
        {
            _sprite.X = _game.AI.Sprite.Bounds.Left - halfSize;
            BounceOffPaddle(_game.AI);
        }

        _sprite.Position += _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    private void BounceOffPaddle(IPaddle paddle)
    {
        float yDiff = _sprite.Y - paddle.Sprite.Y;
        float normalizedYDiff = yDiff / (IPaddle.Height / 2f);
        float angle = normalizedYDiff * (MathF.PI / 4f);

        float randomness = RandomHelpers.NextSingle(-0.1f, 0.1f);
        angle += randomness;

        _velocity.X = Speed * MathF.Cos(angle) * MathF.Sign(-_velocity.X);
        _velocity.Y = Speed * MathF.Sin(angle);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Sprite.Texture, Sprite.Position, null, Sprite.Color, Sprite.Rotation, Sprite.Origin, Sprite.Scale, Sprite.Effects, Sprite.LayerDepth);
    }
}