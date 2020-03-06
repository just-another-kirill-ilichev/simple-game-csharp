using System.IO;
using OpenTK.Graphics.OpenGL4;


namespace SimpleGame.Core
{
    public class Shader : Resource
    {
        private int _program;

        public Shader(Application owner, string fragmentPath, string vertexPath) : base(owner)
        {
            var vertexShader = CompileShader(ShaderType.VertexShader, vertexPath);
            var fragmentShader = CompileShader(ShaderType.FragmentShader, fragmentPath);

            _program = GL.CreateProgram();
            GL.AttachShader(_program, vertexShader);
            GL.AttachShader(_program, fragmentShader);
            GL.LinkProgram(_program);

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            int status;
            GL.GetProgram(_program, GetProgramParameterName.LinkStatus, out status);

            if (status == 0)
            {
                throw new GameException (GL.GetProgramInfoLog(_program));
            }
        }

        private int CompileShader(ShaderType type, string path)
        {
            var shaderSource = File.ReadAllText(path);
            var shader = GL.CreateShader(type);
            GL.ShaderSource(shader, shaderSource);
            GL.CompileShader(shader);

            int status;
            GL.GetShader(shader, ShaderParameter.CompileStatus, out status);

            if (status == 0)
            {
                throw new GameException(GL.GetShaderInfoLog(shader));
            }

            return shader;
        }

        public void Use()
        {
            GL.UseProgram(_program);
        }

        protected override void FreeUnmanaged()
        {
            GL.DeleteProgram(_program);
        }
    }
}