namespace FinalProject
{
#if WINDOWS || XBOX

    internal static class Program
    {
        private static void Main(string[] args)
        {
            using (GameMain game = new GameMain())
            {
                game.Run();
            }
        }
    }

#endif
}