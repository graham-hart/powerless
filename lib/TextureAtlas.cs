using Raylib_cs;
using static Raylib_cs.Raylib;
using CSLib;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;

namespace CSLib
{
    class TextureAtlas
    {
        static Dictionary<string, TextureAtlas> atlases = new Dictionary<string, TextureAtlas>();
        Texture2D text;
        Dictionary<string, Rectangle> images = new Dictionary<string, Rectangle>();
        private TextureAtlas(string name)
        {
            atlases.Add(name, this);
        }
        public string[] AllTextureNames()
        {
            return images.Keys.ToArray();
        }
        public static TextureAtlas FromFile(string fn, string name)
        {
            TextureAtlas tx = new TextureAtlas(name);
            Dictionary<string, Rectangle> imgs = tx.images;
            string[] lines = File.ReadLines(fn).ToArray();
            string imagefn = lines[0];
            string size = lines[1].Replace("size: ", "");
            string format = lines[2].Replace("format: ", "");
            string filter = lines[3].Replace("filter: ", "");
            string repeat = lines[4].Replace("repeat: ", "");
            string currName = null;
            Rectangle currRect = new Rectangle();
            for(int i = 5; i < lines.Length; i++) 
            {
                string line = lines[i];
                if(!line.StartsWith("  ")) {
                    if(currName != null) {
                        imgs.Add(currName,currRect);
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
            imgs.Add(currName, currRect);
            tx.text = LoadTexture(fn.Substring(0, fn.LastIndexOf("/")) + "/" + imagefn);
            return tx;
        }
        public void DrawTexture(string id, Vec2 pos)
        {
            DrawTextureRec(text, images[id], new System.Numerics.Vector2((float)pos.x, (float)pos.y), Color.WHITE);
            // Raylib.DrawRectangle((int)pos.x,(int)pos.y, (int)images[id].width, (int)images[id].height, Color.WHITE);
        }
        public static TextureAtlas GetAtlas(string name) {
            return atlases[name];
        }
    }
}