using System.Collections.Generic;
using CSLib;
using System;
namespace CSLib
{
    class Entity
    {
        public Transform transform;
        public Sprite sprite;
        public RigidBody rb;
        public Entity(Vec2 pos, Vec2 size, Sprite sprite)
        {
            transform = new Transform(pos, size);
            rb = new RigidBody(this);
            this.sprite = sprite;
        }
        public Dictionary<string, bool> Move(Vec2 amount, List<Tile> nearbyTiles) {
            return rb.Move(amount, nearbyTiles);
        }
        public virtual void OnCollision(Dictionary<string,bool> dirs) {

        }
        public virtual void Update() {

        }
        public virtual void Update(Dictionary<int, List<Tile>> surroundings) {

        }
    }
}