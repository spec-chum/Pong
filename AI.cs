using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Pong;

public sealed class AI : IPaddle
{
    private const int Speed = 4;
    private const float PaddleOffset = 1f;

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
            Position = new Vector2(_screenBounds.Width - IPaddle.Width * 2, _screenBounds.Height / 2),
            Origin = new Vector2(0.5f),
            Scale = new Vector2(IPaddle.Width, IPaddle.Height),
            Color = Color.White
        };
    }

    public void Update(GameTime gameTime)
    {
        float direction = MathF.Sign(_game.Ball.Sprite.Y - _sprite.Y);
        float targetY = _game.Ball.Sprite.Y + (direction * RandomHelpers.NextSingle(-0.5f, 0.5f) * PaddleOffset);
        targetY = MathHelper.Clamp(targetY, IPaddle.Height / 2, _game.ScreenBounds.Height - IPaddle.Height / 2);
        _sprite.Y = MathHelper.Lerp(_sprite.Y, targetY, (float)(Speed * gameTime.ElapsedGameTime.TotalSeconds));
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_sprite.Texture, _sprite.Position, null, _sprite.Color, _sprite.Rotation, _sprite.Origin, _sprite.Scale, _sprite.Effects, _sprite.LayerDepth);
    }
}