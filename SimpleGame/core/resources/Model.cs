using Newtonsoft.Json;
using SimpleGame.Core.Loaders.Model;

namespace SimpleGame.Core.Resources
{
    public class Model : BasicMaterialMesh
    {
        private MtlMaterial _material;

        [JsonConstructor]
        public Model(string modelPath) : base()
        {
            var loader = new ObjModelLoader(modelPath);

            _material = loader.Materials["Default"]; // TODO

            _texture = new Texture(_material.AmbientTextureMap);
            SetupBuffers(loader.Mesh);
        }
    }
}