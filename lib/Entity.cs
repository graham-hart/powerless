using System.Collections.Generic;
using CSLib;
namespace CSLib
{
    class Entity
    {
        Transform transform;
        public Entity(Vec2 pos, Vec2 size, Sprite sprite)
        {
            transform = new Transform(pos, size);
        }
        public Rect[] CollisionList(Tile[] collideables)
        {
            List<Rect> hits = new List<Rect>();
            foreach (Tile t in collideables)
            {
                Rect r = t.Rect;
                if (transform.Rect.CollideRect(r))
                {
                    hits.Add(r);
                }
            }
            return hits.ToArray();
        }
        public Dictionary<string, bool> Move(Vec2 amount, Tile[] nearbyTiles)
        {
            Dictionary<string, bool> hitDirs = new Dictionary<string, bool>();
            foreach (string dir in new string[] { "left", "right", "top", "bottom" })
            {
                hitDirs.Add(dir, false);
            }
            transform.Move(amount.x, 0);
            Rect[] collisionList = CollisionList(nearbyTiles);
            Rect tmp = transform.Rect;
            foreach (Rect t in collisionList)
            {
                if (amount.x > 0)
                {
                    tmp.Right = t.Left;
                    transform.pos.x = tmp.X;
                    hitDirs["right"] = true;
                }
                else if (amount.x < 0)
                {
                    tmp.Left = t.Right;
                    transform.pos.x = tmp.X;
                    hitDirs["left"] = true;
                }
            }
            transform.Move(0, amount.y);
            collisionList = CollisionList(nearbyTiles);
            tmp = transform.Rect;
            foreach (Rect t in collisionList)
            {
                if (amount.y > 0)
                {
                    tmp.Top = t.Bottom;
                    transform.pos.y = tmp.Y;
                    hitDirs["top"] = true;
                }
                else if (amount.x < 0)
                {
                    tmp.Bottom = t.Top;
                    transform.pos.y = tmp.Y;
                    hitDirs["bottom"] = true;
                }
            }
            return hitDirs;
        }
    }
}