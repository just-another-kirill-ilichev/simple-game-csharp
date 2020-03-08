using System;
using OpenTK.Graphics.ES30;

namespace SimpleGame.Core
{
    public abstract class Renderable : IDisposable
    {
        protected bool _disposed;
        protected readonly int _vertexArray;
        protected readonly int _buffer;
        protected readonly int _verticeCount;
        protected readonly Shader _shader;

        public Renderable(Shader shader, int vertexCount)
        {
            _verticeCount = vertexCount;
            _shader = shader;
            _vertexArray = GL.GenVertexArray();
            _buffer = GL.GenBuffer();

            GL.BindVertexArray(_vertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _buffer);

            _disposed = false;
        }

        public virtual void Bind()
        {
            _shader.Use();
            GL.BindVertexArray(_vertexArray);
        }

        public virtual void Render()
        {
            GL.DrawArrays(PrimitiveType.Triangles, 0, _verticeCount);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            GL.DeleteVertexArray(_vertexArray);
            GL.DeleteBuffer(_buffer);
            _disposed = true;
        }

        ~Renderable()
        {
            Dispose(false);
        }
    }
}