using System.Linq;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using OpenTK.Graphics.OpenGL4;


namespace SimpleGame.Core.Resources
{
    public class Shader : Resource
    {
        private int _program;

        [JsonConstructor]
        public Shader(string fragmentPath, string vertexPath) :
            this(new List<(ShaderType type, string path)>() {
                    (ShaderType.FragmentShader, fragmentPath), (ShaderType.VertexShader, vertexPath)
                })
        { }

        private Shader(IEnumerable<(ShaderType type, string path)> shaders)
        {
            _program = GL.CreateProgram();

            var compiledShaders = shaders.Select(x => CompileShader(x.type, x.path));

            foreach (var shader in compiledShaders)
            {
                GL.AttachShader(_program, shader);
            }

            GL.LinkProgram(_program);

            foreach (var shader in compiledShaders)
            {
                GL.DetachShader(_program, shader);
                GL.DeleteShader(shader);
            }

            GL.GetProgram(_program, GetProgramParameterName.LinkStatus, out int status);

            if (status == 0) // GL_FALSE
            {
                throw new GameException(GL.GetProgramInfoLog(_program));
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

            if (status == 0 /* GL_FALSE */)
            {
                throw new GameException(GL.GetShaderInfoLog(shader));
            }

            return shader;
        }

        public int GetUniformLocaltion(string name)
        {
            CheckDisposed();
            return GL.GetUniformLocation(_program, name);
        }


        public int GetAttributeLocation(string name)
        {
            CheckDisposed();
            return GL.GetAttribLocation(_program, name);
        }


        public void Use()
        {
            CheckDisposed();
            GL.UseProgram(_program);
        }

        protected override void FreeUnmanaged()
        {
            GL.DeleteProgram(_program);
        }
    }
}