internal class Program
{
    private static void Main()
    {
        using var game = new Pong.Pong();
        game.Run();
    }
}