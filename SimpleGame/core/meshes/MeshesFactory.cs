using OpenTK;

namespace SimpleGame.Core.Meshes
{
    public interface IMeshesFactory<T> where T : struct
    {
        T[] CreateCube();
        T[] CreatePlane();
    }

    public class BasicMeshesFactory : IMeshesFactory<BasicVertex>
    {
        private static BasicMeshesFactory _instance;

        public static BasicMeshesFactory Instance
        {
            get
            { 
                if (_instance == null)
                {
                    _instance = new BasicMeshesFactory();
                }

                return _instance;
            }
        }

        private BasicMeshesFactory() { }

        public BasicVertex[] CreateCube()
        {
            // TODO normals
            BasicVertex[] vertices =
            {
                new BasicVertex(new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), Vector3.Zero,  new Vector2(0, 0)),
                new BasicVertex(new Vector4(-1.0f, -1.0f, 1.0f, 1.0f),  Vector3.Zero,  new Vector2(0, 1)),
                new BasicVertex(new Vector4(-1.0f, 1.0f, -1.0f, 1.0f),  Vector3.Zero,  new Vector2(1, 0)),
                new BasicVertex(new Vector4(-1.0f, 1.0f, -1.0f, 1.0f),  Vector3.Zero,  new Vector2(1, 0)),
                new BasicVertex(new Vector4(-1.0f, -1.0f, 1.0f, 1.0f),  Vector3.Zero,  new Vector2(0, 1)),
                new BasicVertex(new Vector4(-1.0f, 1.0f, 1.0f, 1.0f),   Vector3.Zero,  new Vector2(1, 1)),

                new BasicVertex(new Vector4(1.0f, -1.0f, -1.0f, 1.0f),  Vector3.Zero,  new Vector2(0, 0)),
                new BasicVertex(new Vector4(1.0f, 1.0f, -1.0f, 1.0f),   Vector3.Zero,  new Vector2(1, 0)),
                new BasicVertex(new Vector4(1.0f, -1.0f, 1.0f, 1.0f),   Vector3.Zero,  new Vector2(0, 1)),
                new BasicVertex(new Vector4(1.0f, -1.0f, 1.0f, 1.0f),   Vector3.Zero,  new Vector2(0, 1)),
                new BasicVertex(new Vector4(1.0f, 1.0f, -1.0f, 1.0f),   Vector3.Zero,  new Vector2(1, 0)),
                new BasicVertex(new Vector4(1.0f, 1.0f, 1.0f, 1.0f),    Vector3.Zero,  new Vector2(1, 1)),

                new BasicVertex(new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), Vector3.Zero,  new Vector2(0, 0)),
                new BasicVertex(new Vector4(1.0f, -1.0f, -1.0f, 1.0f),  Vector3.Zero,  new Vector2(1, 0)),
                new BasicVertex(new Vector4(-1.0f, -1.0f, 1.0f, 1.0f),  Vector3.Zero,  new Vector2(0, 1)),
                new BasicVertex(new Vector4(-1.0f, -1.0f, 1.0f, 1.0f),  Vector3.Zero,  new Vector2(0, 1)),
                new BasicVertex(new Vector4(1.0f, -1.0f, -1.0f, 1.0f),  Vector3.Zero,  new Vector2(1, 0)),
                new BasicVertex(new Vector4(1.0f, -1.0f, 1.0f, 1.0f),   Vector3.Zero,  new Vector2(1, 1)),

                new BasicVertex(new Vector4(-1.0f, 1.0f, -1.0f, 1.0f),  Vector3.Zero,  new Vector2(0, 0)),
                new BasicVertex(new Vector4(-1.0f, 1.0f, 1.0f, 1.0f),   Vector3.Zero,  new Vector2(0, 1)),
                new BasicVertex(new Vector4(1.0f, 1.0f, -1.0f, 1.0f),   Vector3.Zero,  new Vector2(1, 0)),
                new BasicVertex(new Vector4(1.0f, 1.0f, -1.0f, 1.0f),   Vector3.Zero,  new Vector2(1, 0)),
                new BasicVertex(new Vector4(-1.0f, 1.0f, 1.0f, 1.0f),   Vector3.Zero,  new Vector2(0, 1)),
                new BasicVertex(new Vector4(1.0f, 1.0f, 1.0f, 1.0f),    Vector3.Zero,  new Vector2(1, 1)),

                new BasicVertex(new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), Vector3.Zero,  new Vector2(0, 0)),
                new BasicVertex(new Vector4(-1.0f, 1.0f, -1.0f, 1.0f),  Vector3.Zero,  new Vector2(0, 1)),
                new BasicVertex(new Vector4(1.0f, -1.0f, -1.0f, 1.0f),  Vector3.Zero,  new Vector2(1, 0)),
                new BasicVertex(new Vector4(1.0f, -1.0f, -1.0f, 1.0f),  Vector3.Zero,  new Vector2(1, 0)),
                new BasicVertex(new Vector4(-1.0f, 1.0f, -1.0f, 1.0f),  Vector3.Zero,  new Vector2(0, 1)),
                new BasicVertex(new Vector4(1.0f, 1.0f, -1.0f, 1.0f),   Vector3.Zero,  new Vector2(0, 0)),

                new BasicVertex(new Vector4(-1.0f, -1.0f, 1.0f, 1.0f),  Vector3.Zero,  new Vector2(0, 0)),
                new BasicVertex(new Vector4(1.0f, -1.0f, 1.0f, 1.0f),   Vector3.Zero,  new Vector2(1, 0)),
                new BasicVertex(new Vector4(-1.0f, 1.0f, 1.0f, 1.0f),   Vector3.Zero,  new Vector2(0, 1)),
                new BasicVertex(new Vector4(-1.0f, 1.0f, 1.0f, 1.0f),   Vector3.Zero,  new Vector2(0, 1)),
                new BasicVertex(new Vector4(1.0f, -1.0f, 1.0f, 1.0f),   Vector3.Zero,  new Vector2(1, 0)),
                new BasicVertex(new Vector4(1.0f, 1.0f, 1.0f, 1.0f),    Vector3.Zero,  new Vector2(1, 1)),
            };
            return vertices;
        }

        public BasicVertex[] CreatePlane()
        {
            BasicVertex[] vertices =
            {
                new BasicVertex(new Vector4(-1.0f, -1.0f, 0.0f, 1.0f), Vector3.UnitZ, new Vector2(0.0f, 0.0f)),
                new BasicVertex(new Vector4(1.0f, -1.0f, 0.0f, 1.0f), Vector3.UnitZ, new Vector2(1.0f, 0.0f)),
                new BasicVertex(new Vector4(-1.0f, 1.0f, 0.0f, 1.0f), Vector3.UnitZ, new Vector2(0.0f, 1.0f)),
                new BasicVertex(new Vector4(-1.0f, 1.0f, 0.0f, 1.0f), Vector3.UnitZ, new Vector2(0.0f, 1.0f)),
                new BasicVertex(new Vector4(1.0f, -1.0f, 0.0f, 1.0f), Vector3.UnitZ, new Vector2(1.0f, 0.0f)),
                new BasicVertex(new Vector4(1.0f, 1.0f, 0.0f, 1.0f), Vector3.UnitZ, new Vector2(1.0f, 1.0f)),
            };

            return vertices;
        }
    }
}