using System.Collections.Generic;
using System;
namespace CSLib
{
    class RigidBody
    {
        public Vec2 velocity;
        public Vec2 acceleration;
        public Transform transform;
        public Entity entity;
        public Vec2 change;
        public bool hasGravity = true;
        public static double GRAVITY = 0.1;
        public static double GROUND_FRICTION = 1;
        public static double AIR_RESISTANCE = 1;
        public RigidBody(Entity entity)
        {
            this.entity = entity;
            transform = entity.transform;
            velocity = Vec2.Zero;
            acceleration = Vec2.Zero;
            change = Vec2.Zero;
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
                    transform.pos.x = t.Left - tmp.Width;
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
                    transform.pos.y = t.Top - tmp.Height;
                    hitDirs["bottom"] = true;
                    break;
                }
            }
            OnCollision(hitDirs);
            entity.OnCollision(hitDirs);
            return hitDirs;
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
        public void ChangePos(Vec2 amt)
        {
            change += amt;
        }
        public void Update(List<Tile> collideables)
        {
            if (hasGravity)
                AddVel(Vec2.Down * GRAVITY);

            Move((velocity + change), collideables);
            change = Vec2.Zero;
            velocity += acceleration;
        }
        public void Accel(Vec2 amt)
        {
            acceleration += amt;
        }
        public void AddVel(Vec2 amt)
        {
            velocity += amt;
        }
        public void OnCollision(Dictionary<string, bool> dirs)
        {
            if (dirs["bottom"])
            {
                velocity.y = 0;
                velocity.x *= GROUND_FRICTION;
            }
            else if (dirs["top"])
            {
                velocity.y = 0;
            }
            if (dirs["left"])
            {
                velocity.x = 0;
            }
            else if (dirs["right"])
            {
                velocity.x = 0;
            }
        }
    }
}