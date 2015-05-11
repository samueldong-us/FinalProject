using System;

namespace FinalProject
{
#if WINDOWS || XBOX

    internal static class Program
    {
        [STAThread]
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