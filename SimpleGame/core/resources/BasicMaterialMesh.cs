using System;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using SimpleGame.Core.Meshes;

namespace SimpleGame.Core.Resources
{
    class BasicMaterialMesh : Resource
    {
        protected Texture _texture;
        protected VertexArray<BasicVertex> _vao;

        internal BasicMaterialMesh()
        {
            _vao = new VertexArray<BasicVertex>();
        }

        public void SetupTexture(Texture texture)
        {
            _texture = texture; // TODO
        }

        public void SetupBuffers(BasicVertex[] data)
        {
            var vertices = Array.ConvertAll(data, item => (BasicVertex)item);

            _vao.SetVerticesData(vertices);
            _vao.SetIndicesData((uint[])(object)Enumerable.Range(0, vertices.Length).ToArray()); // TODO

            _vao.SetVertexAttribute(0, 4, VertexAttribPointerType.Float, BasicVertex.Size, 0);
            _vao.SetVertexAttribute(1, 2, VertexAttribPointerType.Float, BasicVertex.Size, 16);
        }

        public virtual void Render(Shader shader, Matrix4 modelTransform, Matrix4 projectionViewTransform)
        {
            CheckDisposed();
            
            shader.Use();
            shader.SetUniform(20, false, ref projectionViewTransform);
            shader.SetUniform(21, false, ref modelTransform);

            _texture.Bind();
            _vao.DrawElements(PrimitiveType.Triangles);
        }

        protected override void FreeManaged()
        {
            _texture.Dispose();
            _vao.Dispose();
        }
    }
}