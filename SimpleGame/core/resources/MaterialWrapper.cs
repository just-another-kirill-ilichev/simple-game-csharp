using System.IO;
using Newtonsoft.Json;

namespace SimpleGame.Core.Resources
{
    public class MaterialWrapper : Resource
    {
        public Jitter.Dynamics.Material Material { get; }

        public MaterialWrapper(string materialDefPath)
        {
            var fileContent = File.ReadAllText(materialDefPath);

            Material = JsonConvert.DeserializeObject(fileContent) as Jitter.Dynamics.Material;
        }
    }
}