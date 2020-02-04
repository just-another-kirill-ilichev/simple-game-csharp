using System;
using SDL2;

namespace SimpleGame.Core
{
    public static class Keyboard
    {

    }

    public class SDLBaseEventArgs : EventArgs
    {
        public SDL.SDL_EventType Type { get; }
        public uint Timestamp { get; }

        public SDLBaseEventArgs(SDL.SDL_EventType type, uint timestamp)
        {
            Type = type;
            Timestamp = timestamp;
        }
    }

    public enum MouseButtonState
    {
        Pressed = (int)SDL.SDL_PRESSED,
        Released = (int)SDL.SDL_RELEASED
    }

    public enum MouseButton
    {
        Left = (int)SDL.SDL_BUTTON_LEFT,
        Middle = (int)SDL.SDL_BUTTON_MIDDLE,
        Right = (int)SDL.SDL_BUTTON_RIGHT,
        X1 = (int)SDL.SDL_BUTTON_X1,
        X2 = (int)SDL.SDL_BUTTON_X2,
    }

    public class MouseButtonEventArgs : SDLBaseEventArgs
    {
        public byte Clicks { get; }
        public int X { get; }
        public int Y { get; }
        public MouseButton Button { get; }
        public MouseButtonState State { get; }
        public uint WindowId { get; }
        public uint MouseId { get; }

        public MouseButtonEventArgs(SDL.SDL_MouseButtonEvent e) : base(e.type, e.timestamp)
        {
            Clicks = e.clicks;
            X = e.x;
            Y = e.y;
            Button = (MouseButton)e.button;
            State = (MouseButtonState)e.state;
            WindowId = e.windowID;
            MouseId = e.which;
        }
    }

    public static class Events
    {
        public static event EventHandler<SDLBaseEventArgs> Close;
        public static event EventHandler<MouseButtonEventArgs> MouseDown;


        public static void Process()
        {
            while (SDL.SDL_PollEvent(out var e) != 0)
            {
                switch (e.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        Close?.Invoke(null, new SDLBaseEventArgs(e.type, e.quit.timestamp));
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                        MouseDown?.Invoke(null, new MouseButtonEventArgs(e.button));
                        break;
                }
            }
        }
    }
}