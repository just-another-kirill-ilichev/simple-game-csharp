using OpenTK.Graphics.ES30;

namespace SimpleGame.Core
{
    public class GLException : GameException
    {
        public GLException() : base("OpenGL error: " + GL.GetError())
        {

        }
    }
}