using Aiv.Fast2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fast2Da
{
    static class GfxManager
    {
        static Dictionary<string, Texture> textures;

        static GfxManager()
        {
            textures = new Dictionary<string, Texture>();
        }

        public static void AddTexture(string name, string filePath)
        {
            textures.Add(name, new Texture(filePath));
        }

        public static Texture GetTexture(string name)
        {
            if (textures.ContainsKey(name))
            {
                return textures[name];
            }
            return null;
        }

        public static void RemoveAll()
        {
            textures.Clear();
        }
    }
}
