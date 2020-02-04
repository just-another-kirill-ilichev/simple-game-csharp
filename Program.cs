using System;

using SimpleGame.Core;

namespace SimpleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new SDLApplication();
            app.Run();
            app.Dispose();
        }
    }
}
