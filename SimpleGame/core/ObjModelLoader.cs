using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using OpenTK;

namespace SimpleGame.Core
{
    public struct ModelVertex
    {
        public const int Size = (4 + 3 + 2) * 4;
        private readonly Vector4 Position;
        private readonly Vector2 UV;
        private readonly Vector3 Normal;

        public ModelVertex(Vector4 position, Vector3 normal, Vector2 uv)
        {
            Position = position;
            UV = uv;
            Normal = normal;
        }
    }

    public class ObjModelLoader
    {
        private readonly List<ModelVertex> _data;
        private readonly List<Vector4> _vertices;
        private readonly List<Vector3> _normals;
        private readonly List<Vector2> _uvs;

        public ModelVertex[] Mesh { get => _data.ToArray(); }

        public ObjModelLoader(string path)
        {
            _data = new List<ModelVertex>();
            _vertices = new List<Vector4>();
            _normals = new List<Vector3>();
            _uvs = new List<Vector2>();

            ParseFile(path);
        }

        private void ParseFile(string path)
        {
            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
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
                throw new GameException();

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
                throw new GameException();

            _uvs.Add(new Vector2(numbersBuffer[0], numbersBuffer[1]));
        }

        private void ParseNormal(string[] tokens)
        {
            float[] numbersBuffer = tokens.Skip(1)
                .Select(token => float.Parse(token, CultureInfo.InvariantCulture.NumberFormat))
                .ToArray();

            if (numbersBuffer.Length != 3)
                throw new GameException();

            _normals.Add(new Vector3(numbersBuffer[0], numbersBuffer[1], numbersBuffer[2]));
        }

        private ModelVertex ParseVertexDef(string token)
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

            return new ModelVertex(tempVertex, tempNormal, tempUV);
        }

        private void ParseFace(string[] tokens)
        {
            var vertexDefs = tokens.Skip(1)
                .Select(token => ParseVertexDef(token))
                .ToArray();

            _data.AddRange(vertexDefs);
        }
    }
}