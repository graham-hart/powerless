using CSLib;
using Raylib_cs;
using System.Collections.Generic;
using static Powerless.Config;
namespace Powerless
{
    class Player : Entity
    {
        bool onGround = true;
        public Player(Vec2 pos) : base(pos, new Vec2(1, 1), new Sprite("testplayer"))
        {

        }
        public void Render(Camera cam)
        {
            sprite.Render(transform.pos, cam);
        }

        public override void Update(float dt, Dictionary<int, List<Tile>> surroundings)
        {
            float speed = 15f;
            Vec2 move = Vec2.Zero;
            move.y += 3;
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
            List<Tile> collide = new List<Tile>();
            foreach (int layer in COLLIDE_LAYERS)
            {
                if (surroundings.ContainsKey(layer))
                {
                    collide.AddRange(surroundings[layer]);
                }
            }
            rb.AddVel(Vec2.Down * GRAVITY);
            rb.ChangePos(move);
            onGround = false;
            rb.Update(dt, collide);
        }
        public override void OnCollision(Dictionary<string, bool> dirs)
        {
            if (dirs["bottom"])
            {
                onGround = true;
                rb.velocity.y = 0;
            }
        }
    }
}