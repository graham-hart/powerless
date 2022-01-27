using CSLib;
using System;
using Raylib_cs;
using System.Collections.Generic;
using static Powerless.Config;
namespace Powerless
{
    class Player : Entity
    {
        bool onGround = true;
        const double MAX_VEL = .7;
        public Player(Vec2 pos) : base(pos, new Vec2(1, 1), new Sprite("testplayer"))
        {

        }
        public void Render(Camera cam)
        {
            sprite.Render(transform.pos, cam);
        }

        public override void Update(Dictionary<int, List<Tile>> surroundings)
        {
            float speed = .5f;
            Vec2 move = Vec2.Zero;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
            {
                move.x -= speed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
            {
                move.x += speed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) && onGround)
            {
                rb.velocity.y = 0;
                rb.AddVel(Vec2.Up * JUMP_FORCE);
            }
            rb.AddVel(move);
            onGround = false;
            List<Tile> collide = new List<Tile>();
            foreach (int layer in COLLIDE_LAYERS)
            {
                if (surroundings.ContainsKey(layer))
                {
                    collide.AddRange(surroundings[layer]);
                }
            }
            rb.velocity.x = Math.Clamp(rb.velocity.x, -MAX_VEL, MAX_VEL);
            rb.Update(collide);
        }
        public override void OnCollision(Dictionary<string, bool> dirs)
        {
            if (dirs["bottom"])
            {
                onGround = true;
                rb.velocity.y = 0;
                rb.velocity.x *= GROUND_FRICTION;
            }
            else if (dirs["top"])
            {
                rb.velocity.y = 0;
            }
            rb.velocity.x *= AIR_RESISTANCE;
        }
    }
}