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

        public LoaderException(string file, int lineNumber, string line) : base($"An exception occurred while loading file {file};" +
            Environment.NewLine + $"In line {lineNumber}: {line}") {}

        public LoaderException(string file, int lineNumber, string line, Exception inner) : base($"An exception occurred while loading file {file};" +
            Environment.NewLine + $"In line {lineNumber}: {line}") {}

        public LoaderException(string file, int lineNumber, string line, string message) : base($"An exception occurred while loading file {file};" +
            Environment.NewLine + $"In line {lineNumber}: {line}" + 
            Environment.NewLine + message) {}

        public LoaderException(string file, int lineNumber, string line, string message, Exception inner) : base($"An exception occurred while loading file {file};" +
            Environment.NewLine + $"In line {lineNumber}: {line}" + 
            Environment.NewLine + message) {}
    }
}