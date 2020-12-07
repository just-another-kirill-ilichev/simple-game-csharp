using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleGame.Core.Loaders.Model
{
    public struct MtlMaterial
    {
        public string AmbientTextureMap { get; set; } // TODO
        public string DiffuseTextureMap { get; set; }
        public string SpecularTextureMap { get; set; }
    }

    class MtlMaterialLoader
    {
        private readonly string _path;
        private uint _line;
        private string _currentMaterial;
        public Dictionary<string, MtlMaterial> Materials { get; }
        

        public MtlMaterialLoader(string path)
        {
            _path = path;
            Materials = new Dictionary<string, MtlMaterial>();

            ParseFile();
        }

        private void ParseFile()
        {
            var fileContent = File.ReadAllLines(_path);
            _line = 1;

            foreach (var line in fileContent)
            {
                var tokens = line.Split('#')[0] // Skip comments
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);
                
                if (tokens.Length == 0)
                    continue;

                ParseLine(tokens);
                _line++;
            }
        }

        private void ParseLine(string[] tokens)
        {
            switch (tokens[0])
            {
                case "newmtl": NewMaterial(tokens); break;
            }
        }

        private void NewMaterial(string[] tokens)
        {
            if (tokens.Length != 2)
                throw new LoaderException(_path, $"Cannot parse line {_line} - invalid number of tokens");
            
            var materialName = tokens[1];

            Materials.Add(materialName, new MtlMaterial());
            _currentMaterial = materialName;
        }


    }
}