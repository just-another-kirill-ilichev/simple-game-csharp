using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;

namespace SimpleGame.Core.Resources
{
    class VertexArray<T> : Resource where T : struct
    {
        private readonly int _vertexArray;
        private readonly int _vertexBuffer;
        private readonly int _elementBuffer;
        private int _count;

        public VertexArray()
        {
            _vertexArray = GL.GenVertexArray();
            _vertexBuffer = GL.GenBuffer();
            _elementBuffer = GL.GenBuffer();
        }

        public void SetVerticesData(T[] vertices)
        {
            CheckDisposed();
            Bind();

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, Marshal.SizeOf(default(T)) * vertices.Length, vertices, BufferUsageHint.StaticDraw);
        
            Unbind();
        }

        public void SetIndicesData(uint[] indices)
        {
            CheckDisposed();
            Bind();

            _count = indices.Length;

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBuffer);
            GL.BufferData(BufferTarget.ElementArrayBuffer, sizeof(uint) * indices.Length, indices, BufferUsageHint.StaticDraw);
        
            Unbind();
        }

        public void SetVertexAttribute(int index, int size, VertexAttribPointerType type, int stride, int offset)
        {
            CheckDisposed();
            Bind();
            
            GL.VertexAttribPointer(index, size, type, false, stride, offset);
            GL.EnableVertexAttribArray(index);
        
            Unbind();
        }

        public void SetVertexAttributes()
        {
            // TODO get all fields from structured via reflection 
            throw new NotImplementedException();
        }

        public void DrawElements(PrimitiveType type)
        {
            CheckDisposed();
            Bind();
            GL.DrawElements(type, _count, DrawElementsType.UnsignedInt, 0);
        }

        private void Bind()
        {
            GL.BindVertexArray(_vertexArray);
        }

        private void Unbind()
        {
            GL.BindVertexArray(0);
        }

        protected override void FreeUnmanaged()
        {
            GL.DeleteVertexArray(_vertexArray);
            GL.DeleteBuffer(_vertexBuffer);
            GL.DeleteBuffer(_elementBuffer);
        }
    }
}