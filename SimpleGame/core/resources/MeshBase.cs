using OpenTK.Graphics.OpenGL;

namespace SimpleGame.Core.Resources
{
    public abstract class MeshBase : Resource
    {
        protected readonly int _vertexArray;
        protected readonly int _buffer;

        public MeshBase()
        {
            _vertexArray = GL.GenVertexArray();
            _buffer = GL.GenBuffer();

            GL.BindVertexArray(_vertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _buffer);
        }

        protected override void FreeUnmanaged()
        {
            GL.DeleteVertexArray(_vertexArray);
            GL.DeleteBuffer(_buffer);
        }
    }
}