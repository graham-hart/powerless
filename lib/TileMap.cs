using System.Collections.Generic;
using System.IO;
using System;
using CSLib;
using System.Linq;
using System.Threading.Tasks;
namespace CSLib
{
    class TileMap
    {
        Dictionary<int, Dictionary<Vec2, Tile>> tiles;
        public Dictionary<int, List<Tile>> visibleTiles;
        public List<int> layers;
        public TileMap()
        {
            tiles = new Dictionary<int, Dictionary<Vec2, Tile>>();
            layers = new List<int>();
        }
        public TileMap(Dictionary<int, Dictionary<Vec2, Tile>> tiles)
        {
            this.tiles = tiles;
            layers = new List<int>();
        }
        public static TileMap FromFile(string fn)
        {
            string[] allTiles = String.Concat(File.ReadAllText(fn).Where(c => !Char.IsWhiteSpace(c))).Split(';');
            Dictionary<int, Dictionary<Vec2, Tile>> tiles = new Dictionary<int, Dictionary<Vec2, Tile>>();
            List<int> layers = new List<int>();
            foreach (string s in allTiles)
            {
                if (s == "") continue;
                string[] parts = s.Split(":");
                string type = parts[1];
                string[] coords = parts[0].Split(",");
                Vec2 xy = new Vec2(int.Parse(coords[0]), int.Parse(coords[1]));
                int z = int.Parse(coords[2]);
                if (!tiles.ContainsKey(z))
                {
                    tiles.Add(z, new Dictionary<Vec2, Tile>());
                    layers.Add(z);
                }
                tiles[z].Add(xy, new Tile(xy, z, type));
            }
            TileMap tm = new TileMap(tiles);
            tm.layers = layers;
            return tm;
        }
        public void ToFile(string fn)
        {
            string output = "";
            foreach (int z in layers)
            {
                Dictionary<Vec2, Tile> layer = tiles[z];
                for (int j = 0; j < layer.Count; j++)
                {
                    Vec2 xy = layer.Keys.ElementAt(j);
                    Tile t = layer[xy];
                    output += String.Format("{0},{1},{2}:{3};", (int)xy.x, (int)xy.y, z, t.type);
                }
            }
            File.WriteAllLines(fn, new string[] { output });
        }
        public bool HasTile(Vec2 xy, int layer) {
            return (tiles.ContainsKey(layer) && tiles[layer].ContainsKey(xy));
        }
        public Tile GetTile(Vec2 xy, int layer)
        {
            return tiles[layer][xy];
        }
        public void SetTile(Vec2 xy, int layer, string type)
        {
            if (!tiles.ContainsKey(layer)) {
                tiles.Add(layer, new Dictionary<Vec2, Tile>()); 
                layers.Add(layer);
            }
            tiles[layer][xy] = new Tile(xy, layer, type);
        }
        public void DelTile(Vec2 xy, int layer)
        {
            tiles[layer].Remove(xy);
            if (tiles[layer].Count == 0)
            {
                tiles.Remove(layer);
                layers.Remove(layer);
            }
        }
        public void UpdateVisibleTiles(Camera cam)
        {
            Vec2 bottomRight = cam.BottomRight;
            Vec2 topLeft = cam.TopLeft;
            Dictionary<int, List<Tile>> vis = new Dictionary<int, List<Tile>>();
            for (int x = (int)Math.Floor(topLeft.x); x < (int)Math.Ceiling(bottomRight.x); x++)
            {
                for (int y = (int)Math.Floor(topLeft.y); y < (int)Math.Ceiling(bottomRight.y); y++)
                {
                    Vec2 xy = new Vec2(x, y);
                    foreach(int z in layers) {
                        if(!vis.ContainsKey(z)) {
                            vis.Add(z, new List<Tile>());
                        }
                        if(tiles[z].ContainsKey(xy)) 
                            vis[z].Add(tiles[z][xy]);
                    }
                }
            }
            visibleTiles = vis;
        }
        public static TileMap RandomTM(int width, int height, string[] typeList)
        {
            Random rnd = new Random();
            TileMap tm = new TileMap();
            for (int i = -width / 2; i < width / 2; i++)
            {
                for (int j = -height / 2; j < height / 2; j++)
                {
                    tm.SetTile(new Vec2(i, j), 0, typeList[rnd.Next(0, typeList.Length)]);
                }
            }
            return tm;
        }

        public void Render(Camera cam) {
            foreach (int z in layers)
            {
                if (visibleTiles.ContainsKey(z))
                {
                    List<Tile> layer = visibleTiles[z];
                    foreach (Tile t in layer)
                    {
                        TextureAtlas.DrawTexture(t.type, t.pos * cam.pixelsPerUnit);
                    }
                }
            }
        }
    }
}