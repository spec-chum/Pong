namespace Pong;

internal static class Program
{
    private static void Main()
    {
        using var game = new Pong();
        game.Run();
    }
}