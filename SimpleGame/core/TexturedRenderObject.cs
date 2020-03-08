using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace SimpleGame.Core
{
    public struct TexturedVertex
    {
        public const int Size = (4 + 2) * 4;

        private readonly Vector4 _position;
        private readonly Vector2 _uv;

        public TexturedVertex(Vector4 position, Vector2 uv)
        {
            _position = position;
            _uv = uv;
        }
    }
    public class TexturedRenderObject : Renderable
    {
        private Texture _texture;

        public TexturedRenderObject(TexturedVertex[] vertices, Shader shader, string texturePath) : base(shader, vertices.Length)
        {
            GL.NamedBufferStorage(_buffer, TexturedVertex.Size * vertices.Length, vertices, BufferStorageFlags.MapWriteBit);

            GL.VertexArrayAttribBinding(_vertexArray, 0, 0);
            GL.EnableVertexArrayAttrib(_vertexArray, 0);
            GL.VertexArrayAttribFormat(_vertexArray, 0, 4, VertexAttribType.Float, false, 0);

            GL.VertexArrayAttribBinding(_vertexArray, 1, 0);
            GL.EnableVertexArrayAttrib(_vertexArray, 1);
            GL.VertexArrayAttribFormat(_vertexArray, 1, 2, VertexAttribType.Float, false, 16);

            GL.VertexArrayVertexBuffer(_vertexArray, 0, _buffer, IntPtr.Zero, TexturedVertex.Size);

            _texture = new Texture(texturePath);
        }

        public override void Bind()
        {
            base.Bind();
            _texture.Bind();
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _texture.Dispose();
            }

            GL.DeleteVertexArray(_vertexArray);
            GL.DeleteBuffer(_buffer);
            
            _disposed = true;
        }
    }
}