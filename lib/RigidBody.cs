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
        public Dictionary<string, List<Tile>> Move(Vec2 amount, List<Tile> nearbyTiles)
        {
            Dictionary<string, List<Tile>> hitDirs = new Dictionary<string, List<Tile>>();
            transform.Move(amount.x, 0);
            Tile[] collisionList = CollisionList(nearbyTiles);
            Rect tmp = transform.Rect;
            foreach (Tile t in collisionList)
            {
                Rect r = t.Rect;
                if (amount.x > 0) // Going right
                {
                    transform.pos.x = r.Left - tmp.Width;
                    if(!hitDirs.ContainsKey("right")) {
                        hitDirs.Add("right", new List<Tile>());
                    }
                    hitDirs["right"].Add(t);
                }
                else if (amount.x < 0) // Going left
                {
                    transform.pos.x = r.Right;
                    if (!hitDirs.ContainsKey("left"))
                    {
                        hitDirs.Add("left", new List<Tile>());
                    }
                    hitDirs["left"].Add(t);
                }
            }
            transform.Move(0, amount.y);
            collisionList = CollisionList(nearbyTiles);
            tmp = transform.Rect;
            foreach (Tile t in collisionList)
            {
                Rect r = t.Rect;

                if (amount.y < 0) // Going up
                {
                    transform.pos.y = r.Bottom;
                    if (!hitDirs.ContainsKey("top"))
                    {
                        hitDirs.Add("top", new List<Tile>());
                    }
                    hitDirs["top"].Add(t);
                    break;
                }
                else if (amount.y > 0) // Going down
                {
                    transform.pos.y = r.Top - tmp.Height;
                    if (!hitDirs.ContainsKey("bottom"))
                    {
                        hitDirs.Add("bottom", new List<Tile>());
                    }
                    hitDirs["bottom"].Add(t);
                }
            }
            OnCollision(hitDirs);
            entity.OnCollision(hitDirs);
            return hitDirs;
        }
        public Tile[] CollisionList(List<Tile> collideables)
        {
            List<Tile> hits = new List<Tile>();
            foreach (Tile t in collideables)
            {
                if (transform.Rect.CollideRect(t.Rect))
                {
                    hits.Add(t);
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
        public void OnCollision(Dictionary<string, List<Tile>> dirs)
        {
            if (dirs.ContainsKey("bottom"))
            {
                velocity.y = 0;
                velocity.x *= GROUND_FRICTION;
            }
            else if (dirs.ContainsKey("top"))
            {
                velocity.y = 0;
            }
            if (dirs.ContainsKey("right"))
            {
                velocity.x = 0;
            }
            else if (dirs.ContainsKey("left"))
            {
                velocity.x = 0;
            }
        }
    }
}