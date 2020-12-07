using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using OpenTK;
using SimpleGame.Core.Meshes;
using SimpleGame.Core.Resources;

namespace SimpleGame.Core.Loaders.Model
{
    public class ObjModelLoader
    {
        private readonly List<BasicVertex> _data;
        private readonly uint[] _indices;
        private readonly List<Vector4> _vertices;
        private readonly List<Vector3> _normals;
        private readonly List<Vector2> _uvs;

        private readonly string _path;
        private string _line;
        private int _lineNumber;

        private readonly Dictionary<string, MtlMaterial> _materials;

        public IReadOnlyDictionary<string, MtlMaterial> Materials => _materials;

        public BasicVertex[] Mesh { get => _data.ToArray(); }

        public ObjModelLoader(string path)
        {
            _data = new List<BasicVertex>();
            _vertices = new List<Vector4>();
            _normals = new List<Vector3>();
            _uvs = new List<Vector2>();

            _materials = new Dictionary<string, MtlMaterial>() // TODO
            {
                {"Default", new MtlMaterial() { AmbientTextureMap = "../resources/dotted.png" }}
            };

            ParseFile(path);
        }

        private void ParseFile(string path)
        {
            var lines = File.ReadAllLines(path);
            _lineNumber = 0;


            foreach (var line in lines)
            {
                _lineNumber++;
                _line = line;

                var tokens = line.Split('#')[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (tokens.Length == 0)
                    continue;

                switch (tokens[0])
                {
                    case "v":
                        ParseVertex(tokens);
                        break;
                    case "vt":
                        ParseUV(tokens);
                        break;
                    case "vn":
                        ParseNormal(tokens);
                        break;
                    case "f":
                        ParseFace(tokens);
                        break;
                    case "mtllib":
                        ParseMtl(tokens);
                        break;
                    default:
                        break;
                }
            }
        }

        private void ParseVertex(string[] tokens)
        {
            float[] numbersBuffer = tokens.Skip(1)
                .Select(token => float.Parse(token, CultureInfo.InvariantCulture.NumberFormat))
                .ToArray();

            if (numbersBuffer.Length != 4 && numbersBuffer.Length != 3)
                throw new LoaderException(_path, _lineNumber, _line, "Invalid number of tokens to create vertex");

            if (numbersBuffer.Length == 4)
                _vertices.Add(new Vector4(numbersBuffer[0], numbersBuffer[1], numbersBuffer[2], numbersBuffer[3]));
            else
                _vertices.Add(new Vector4(numbersBuffer[0], numbersBuffer[1], numbersBuffer[2], 1.0f));
        }

        private void ParseUV(string[] tokens)
        {
            float[] numbersBuffer = tokens.Skip(1)
                .Select(token => float.Parse(token, CultureInfo.InvariantCulture.NumberFormat))
                .ToArray();

            if (numbersBuffer.Length != 2)
                throw new ArgumentException(); // TODO

            _uvs.Add(new Vector2(numbersBuffer[0], numbersBuffer[1]));
        }

        private void ParseNormal(string[] tokens)
        {
            float[] numbersBuffer = tokens.Skip(1)
                .Select(token => float.Parse(token, CultureInfo.InvariantCulture.NumberFormat))
                .ToArray();

            if (numbersBuffer.Length != 3)
                throw new ArgumentException(); // TODO

            _normals.Add(new Vector3(numbersBuffer[0], numbersBuffer[1], numbersBuffer[2]));
        }

        private BasicVertex ParseVertexDef(string token)
        {
            Vector4 tempVertex = Vector4.Zero;
            Vector3 tempNormal = Vector3.Zero;
            Vector2 tempUV = Vector2.Zero;

            var vertexDef = token.Split('/');

            tempVertex = _vertices[int.Parse(vertexDef[0]) - 1];

            if (vertexDef.Length > 1 && !string.IsNullOrWhiteSpace(vertexDef[1]))
                tempUV = _uvs[int.Parse(vertexDef[1]) - 1];

            if (vertexDef.Length > 2)
                tempNormal = _normals[int.Parse(vertexDef[2]) - 1];

            return new BasicVertex(tempVertex, tempNormal, tempUV);
        }

        private (int, int, int) ParseIndices(string token)
        {
            var idx = token.Split('/');

            return (int.Parse(idx[0]), int.Parse(idx[1]), int.Parse(idx[2])); // TODO
        }

        private void ParseFace(string[] tokens)
        {
            var vertexDefs = tokens.Skip(1)
                .Select(token => ParseVertexDef(token))
                .ToArray();


            //var idxes = tokens.Skip(1).Select(token => ParseIndices(token));
            //_indices.AddRange(idxes);

            _data.AddRange(vertexDefs);
        }

        void ParseMtl(string[] tokens)
        {
            if (tokens.Length != 2)
                throw new ArgumentException(); // TODO

            var path = tokens[1];
            //var materials = new MtlMaterialLoader(path).Materials;

            /*foreach(var material in materials)
            {
                _materials.Add(material.Key, material.Value);
            }*/
        }

        internal BasicMaterialMesh BuildModel()
        {
            var result = new BasicMaterialMesh();

            var material = Materials["Default"];
            result.SetupTexture(new Texture(material.AmbientTextureMap));

            result.SetupBuffers(Mesh); // TODO

            return result;
        }
    }
}