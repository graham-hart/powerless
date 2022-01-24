using System.Collections.Generic;
using CSLib;
using System;
namespace CSLib
{
    class Entity
    {
        public Transform transform;
        public Sprite sprite;
        public Vec2 velocity = Vec2.Zero;
        public Entity(Vec2 pos, Vec2 size, Sprite sprite)
        {
            transform = new Transform(pos, size);
            this.sprite = sprite;
        }
        public Rect[] CollisionList(List<Tile> collideables)
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
        public virtual void OnCollision(Dictionary<string,bool> dirs) {

        }
        public Dictionary<string, bool> Move(Vec2 amount, List<Tile> nearbyTiles)
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
                if (amount.x > 0) // Going right
                {
                    transform.pos.x = t.Left-tmp.Width;
                    hitDirs["right"] = true;
                    break;
                }
                else if (amount.x < 0) // Going left
                {
                    transform.pos.x = t.Right;
                    hitDirs["left"] = true;
                    break;
                }
            }
            transform.Move(0, amount.y);
            collisionList = CollisionList(nearbyTiles);
            tmp = transform.Rect;
            foreach (Rect t in collisionList)
            {
                if (amount.y < 0) // Going up
                {
                    transform.pos.y = t.Bottom;
                    hitDirs["top"] = true;
                    break;
                }
                else if (amount.y > 0) // Going down
                {
                    transform.pos.y = t.Top-tmp.Height;
                    hitDirs["bottom"] = true;
                    break;
                }
            }
            OnCollision(hitDirs);
            return hitDirs;
        }
        public virtual void Update(float dt) {

        }
        public virtual void Update(float dt, Dictionary<int, List<Tile>> surroundings) {

        }
    }
}