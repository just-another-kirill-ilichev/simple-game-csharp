using OpenTK;

namespace SimpleGame.Core.Meshes
{
    public struct BasicVertex
    {
        public const int Size = (4 + 3 + 2) * 4;
        private readonly Vector4 _position;
        private readonly Vector2 _uv;
        private readonly Vector3 _normal;

        public BasicVertex(Vector4 position, Vector3 normal, Vector2 uv)
        {
            _position = position;
            _uv = uv;
            _normal = normal;
        }
    }
}