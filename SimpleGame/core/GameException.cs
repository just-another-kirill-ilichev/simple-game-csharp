using System;

namespace SimpleGame.Core
{
    public class GameException : Exception
    {
        public GameException() : base()
        {

        }

        public GameException(string message) : base(message)
        {

        }

        public GameException(String message, Exception inner) : base(message, inner)
        {
            
        }
    }
}