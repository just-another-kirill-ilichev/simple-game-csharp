using System;

namespace SimpleGame.Core.Loaders
{
    public class LoaderException : GameException
    {
        public LoaderException() : base() {}
        
        public LoaderException(string file, string message) : base($"An exception occurred while loading file {file};" +
            Environment.NewLine + message) {}

        public LoaderException(string file, string message, Exception inner) : base($"An exception occurred while loading file {file};" +
            Environment.NewLine + message, inner) {}
    }
}