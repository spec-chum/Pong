namespace Pong;

public interface IPaddle
{
    const int Width = 16;
    const int Height = 80;

    Sprite Sprite { get; }
}