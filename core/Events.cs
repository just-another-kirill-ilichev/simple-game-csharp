using System;
using SDL2;

namespace SimpleGame.Core
{
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

    public class MouseBaseEventArgs : SDLBaseEventArgs
    {
        public int X { get; }
        public int Y { get; }
        public uint WindowId { get; }
        public uint MouseId { get; }

        public MouseBaseEventArgs(int x, int y, uint windowId, uint mouseId, SDL.SDL_EventType type, uint timestamp) : base(type, timestamp)
        {
            X = x;
            Y = y;
            WindowId = windowId;
            MouseId = mouseId;
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

    public class MouseButtonEventArgs : MouseBaseEventArgs
    {
        public byte Clicks { get; }
        public MouseButton Button { get; }
        public MouseButtonState State { get; }

        public MouseButtonEventArgs(SDL.SDL_MouseButtonEvent e) : base(e.x, e.y, e.windowID, e.which, e.type, e.timestamp)
        {
            Clicks = e.clicks;
            Button = (MouseButton)e.button;
            State = (MouseButtonState)e.state;
        }
    }

    public class MouseMotionEventArgs : MouseBaseEventArgs
    {
        public int XRel { get; }
        public int YRel { get; }
        MouseMotionEventArgs(SDL.SDL_MouseMotionEvent e) : base(e.x, e.y, e.windowID, e.which, e.type, e.timestamp)
        {
            XRel = e.xrel;
            YRel = e.yrel;
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