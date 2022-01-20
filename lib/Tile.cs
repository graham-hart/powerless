using CSLib;
namespace CSLib
{
    class Tile
    {
        public Vec2 pos;
        public Vec2 size;
        public string type;
        public int layer;
        public Rect Rect
        {
            get { return new Rect(pos, size); }
        }
        public Tile(Vec2 pos, int layer, string type)
        {
            this.pos = pos;
            this.type = type;
            this.layer = layer;
            this.size = Vec2.Zero;
        }
    }
}