using OpenTK.Graphics.OpenGL4;

namespace SimpleGame.Core
{
    public class GLException : GameException
    {
        public GLException() : base("OpenGl error: " + GL.GetError())
        {

        }
    }
}