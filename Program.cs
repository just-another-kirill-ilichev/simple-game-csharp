using System;

using SimpleGame.Core;

namespace SimpleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Window window = new Window("test", 800, 600);

            bool isRuninng = true;

            Events.Close += (o, e) => isRuninng = false;

            while (isRuninng)
            {
                Events.Process();
            }
        }
    }
}
