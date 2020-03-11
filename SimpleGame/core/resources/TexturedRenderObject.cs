using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;

namespace SimpleGame.Core.Resources
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

    public class TexturedRenderObject : MeshBase
    {
        private readonly int _verticesCount;
        private readonly Texture _texture;

        public TexturedRenderObject(TexturedVertex[] vertices, string texturePath)
        {
            GL.NamedBufferStorage(_buffer, TexturedVertex.Size * vertices.Length, vertices, BufferStorageFlags.MapWriteBit);

            GL.VertexArrayAttribBinding(_vertexArray, 0, 0);
            GL.EnableVertexArrayAttrib(_vertexArray, 0);
            GL.VertexArrayAttribFormat(_vertexArray, 0, 4, VertexAttribType.Float, false, 0);

            GL.VertexArrayAttribBinding(_vertexArray, 1, 0);
            GL.EnableVertexArrayAttrib(_vertexArray, 1);
            GL.VertexArrayAttribFormat(_vertexArray, 1, 2, VertexAttribType.Float, false, 16);

            GL.VertexArrayVertexBuffer(_vertexArray, 0, _buffer, IntPtr.Zero, TexturedVertex.Size);

            _verticesCount = vertices.Length;
            _texture = new Texture(texturePath);
        }

        public virtual void Render(Shader shader, Matrix4 transform, Matrix4 camera)
        {
            shader.Use();
            GL.UniformMatrix4(20, false, ref camera);
            GL.UniformMatrix4(21, false, ref transform);
            _texture.Bind();

            GL.BindVertexArray(_vertexArray);
            GL.DrawArrays(PrimitiveType.Triangles, 0, _verticesCount);
        }

        protected override void FreeManaged()
        {
            _texture.Dispose();
        }
    }
}