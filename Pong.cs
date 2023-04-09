using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong;

public sealed class Pong : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _texture;
    private SpriteFont _font;

    public Player Player { get; private set; }
    public int PlayerScore { get; set; }
    public AI AI { get; private set; }
    public int AIScore { get; set; }
    public Ball Ball { get; private set; }
    public Rectangle ScreenBounds { get; private set; }

    public Pong()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        IsFixedTimeStep = true;

        _font = Content.Load<SpriteFont>("PongFont");
        ScreenBounds = GraphicsDevice.Viewport.Bounds;

        var texture = new Texture2D(GraphicsDevice, 1, 1);
        texture.SetData(new Color[] { Color.White });
        _texture = texture;

        Player = new Player(this, texture);
        AI = new AI(this, texture);
        Ball = new Ball(this, texture);
    }

    protected override void Update(GameTime gameTime)
    {
        Ball.Update(gameTime);
        AI.Update(gameTime);
        Player.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        DrawUI(_spriteBatch);
        Ball.Draw(_spriteBatch);
        AI.Draw(_spriteBatch);
        Player.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void DrawUI(SpriteBatch spriteBatch)
    {
        int centre = ScreenBounds.Width / 2;
        int count = ScreenBounds.Height / 8;

        for (int i = 1; i < count; i += 2)
        {
            spriteBatch.Draw(_texture, new Vector2(centre, i * 8), null, Color.White, 0, new Vector2(0.5f), 8, SpriteEffects.None, 0);
        }

        spriteBatch.DrawString(_font, PlayerScore.ToString(), new Vector2(150, 0), Color.White);
        spriteBatch.DrawString(_font, AIScore.ToString(), new Vector2(600, 0), Color.White);
    }

    public void Reset()
    {
        Player.Reset();
        AI.Reset();
        Ball.Reset();
    }
}