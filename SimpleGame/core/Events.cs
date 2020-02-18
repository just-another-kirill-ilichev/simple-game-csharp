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

    public enum ButtonState
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
        public ButtonState ButtonState { get; }

        public MouseButtonEventArgs(SDL.SDL_MouseButtonEvent e) : base(e.x, e.y, e.windowID, e.which, e.type, e.timestamp)
        {
            Clicks = e.clicks;
            Button = (MouseButton)e.button;
            ButtonState = (ButtonState)e.state;
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

    public class KeyData
    {
        public SDL.SDL_Keymod Modifiers { get; }
        public SDL.SDL_Keycode Key { get; }
        public SDL.SDL_Scancode Scancode { get; }


        public KeyData(SDL.SDL_Keysym key)
        {
            Modifiers = key.mod;
            Key = key.sym;
            Scancode = key.scancode;
        }
    }

    public class KeyboardEventArgs : SDLBaseEventArgs
    {
        public uint WindowId { get; }
        public ButtonState ButtonState { get; }
        public byte Repeat { get; }
        public KeyData KeyData { get; }

        public KeyboardEventArgs(SDL.SDL_KeyboardEvent e) : base(e.type, e.timestamp)
        {
            WindowId = e.windowID;
            ButtonState = (ButtonState)e.state;
            Repeat = e.repeat;
            KeyData = new KeyData(e.keysym);
        }
    }

    public static class Events
    {
        public static event EventHandler<SDLBaseEventArgs> Close;
        public static event EventHandler<MouseButtonEventArgs> MouseButtonDown;
        public static event EventHandler<MouseButtonEventArgs> MouseButtonUp;
        public static event EventHandler<KeyboardEventArgs> KeyDown;
        public static event EventHandler<KeyboardEventArgs> KeyUp;
        
        
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
                        MouseButtonDown?.Invoke(null, new MouseButtonEventArgs(e.button));
                        break;
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                        MouseButtonUp?.Invoke(null, new MouseButtonEventArgs(e.button));
                        break;
                    case SDL.SDL_EventType.SDL_KEYDOWN:
                        KeyDown?.Invoke(null, new KeyboardEventArgs(e.key));
                        break;
                    case SDL.SDL_EventType.SDL_KEYUP:
                        KeyUp?.Invoke(null, new KeyboardEventArgs(e.key));
                        break;
                }
            }
        }
    }
}