using Raylib_cs;
using static Raylib_cs.Raylib;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace CSLib
{
    class TextureAtlas
    {
        static Texture2D texture;
        public static Dictionary<string, Rectangle> textures;
        public string[] AllTextureNames()
        {
            return textures.Keys.ToArray();
        }
        public static void LoadFile(string fn)
        {
            textures = new Dictionary<string, Rectangle>();
            string[] lines = File.ReadLines(fn).ToArray();
            string imagefn = lines[0];
            string size = lines[1].Replace("size: ", "");
            string currName = null;
            Rectangle currRect = new Rectangle();
            for(int i = 2; i < lines.Length; i++) 
            {
                string line = lines[i];
                if(!line.StartsWith("  ")) {
                    if(currName != null) {
                        textures.Add(currName,currRect);
                    }
                    currRect = new Rectangle();
                    currName = line.Replace("\n", "");
                } else if(line.StartsWith("  xy")) {
                    string[] tmp = line.Replace("  xy: ", "").Replace(" ", "").Split(",");
                    currRect.x = int.Parse(tmp[0]);
                    currRect.y = int.Parse(tmp[1]);
                } else if(line.StartsWith("  size")) {
                    string[] tmp = line.Replace("  size: ", "").Replace(" ", "").Split(",");
                    currRect.width = int.Parse(tmp[0]);
                    currRect.height = int.Parse(tmp[1]);
                }
            }
            textures.Add(currName, currRect);
            texture = LoadTexture(fn.Substring(0, fn.LastIndexOf("/")) + "/" + imagefn);
        }
        public static void DrawTexture(string id, Vec2 pos)
        {
            DrawTextureRec(texture, textures[id], new System.Numerics.Vector2((float)pos.x, (float)pos.y), Color.WHITE);
        }
    }
}