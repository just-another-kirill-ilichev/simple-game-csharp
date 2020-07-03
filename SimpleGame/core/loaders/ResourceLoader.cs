using System.Collections.Generic;
using SimpleGame.Core.Loaders.Model;
using SimpleGame.Core.Resources;
using SimpleGame.Core.Meshes;

namespace SimpleGame.Core.Loaders
{
    public struct ResourceData
    {
        public string Type { get; set; }
        public Dictionary<string, object> Args;
    }

    class ResourceLoader
    {
        private static ResourceLoader _instance;

        public static ResourceLoader Instance
        {
            get
            { 
                if (_instance == null)
                {
                    _instance = new ResourceLoader();
                }

                return _instance;
            }
        } 

        public Resource LoadResource(ResourceData data)
        {
            switch (data.Type)
            {
                case "model/obj": return LoadObjModel(data);
                // case "model/plane": return BasicMeshesFactory.Instance.CreatePlane();
                // case "model/cube": return BasicMeshesFactory.Instance.CreateCube();
                case "shader/glsl": return LoadGLSLShader(data);
                default: throw new LoaderException(); // TODO
            }
        }

        private BasicMaterialMesh LoadObjModel(ResourceData data)
        {
            var path = data.Args.GetValueOrDefault("path") as string ?? throw new LoaderException();

            return new ObjModelLoader(path).BuildModel();
        }

        private Resource LoadGLSLShader(ResourceData data)
        {
            var fragmentPath = data.Args.GetValueOrDefault("fragmentPath") as string ?? throw new LoaderException();
            var vertexPath = data.Args.GetValueOrDefault("vertexPath") as string ?? throw new LoaderException();

            return new Shader(fragmentPath, vertexPath);
        }
    }
}