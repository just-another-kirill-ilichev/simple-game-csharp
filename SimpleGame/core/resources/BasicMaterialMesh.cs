using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using SimpleGame.Core.Meshes;

namespace SimpleGame.Core.Resources
{
    public abstract class BasicMaterialMesh : MeshBase
    {
        private int _verticesCount;
        protected Texture _texture;

        protected void SetupBuffers(BasicVertex[] data)
        {
            var temp = Array.ConvertAll(data, item => item as object);
            SetupBuffers(temp);
        }

        protected override void SetupBuffers(object[] data)
        {
            base.SetupBuffers(data);

            var vertices = Array.ConvertAll(data, item => (BasicVertex)item);

            GL.NamedBufferStorage(_buffer, BasicVertex.Size * vertices.Length, vertices, BufferStorageFlags.MapWriteBit);
        
            GL.VertexArrayAttribBinding(_vertexArray, 0, 0);
            GL.EnableVertexArrayAttrib(_vertexArray, 0);
            GL.VertexArrayAttribFormat(_vertexArray, 0, 4, VertexAttribType.Float, false, 0);

            GL.VertexArrayAttribBinding(_vertexArray, 1, 0);
            GL.EnableVertexArrayAttrib(_vertexArray, 1);
            GL.VertexArrayAttribFormat(_vertexArray, 1, 2, VertexAttribType.Float, false, 16);

            GL.VertexArrayVertexBuffer(_vertexArray, 0, _buffer, IntPtr.Zero, BasicVertex.Size);
        
            _verticesCount = vertices.Length;
        }

        public virtual void Render(Shader shader, Matrix4 modelTransform, Matrix4 projectionViewTransform)
        {
            CheckDisposed();
            shader.Use();

            GL.UniformMatrix4(20, false, ref projectionViewTransform);
            GL.UniformMatrix4(21, false, ref modelTransform);
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