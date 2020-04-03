using SimpleGame.Core.Meshes;

namespace SimpleGame.Core.Resources
{
    public class Sprite : BasicMaterialMesh
    {
        public Sprite(string texturePath) : base()
        {
            _texture = new Texture(texturePath);

            SetupBuffers(BasicMeshesFactory.Instance.CreatePlane());
        }
    }
}