using CSLib;
using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Collections.Generic;
using static Powerless.Config;
namespace Powerless
{
    class Player : Entity {
        bool onGround = true;
        public Player(Vec2 pos) : base(pos, new Vec2(1,1), new Sprite("testplayer")) {

        }
        public void Render(Camera cam) {
            sprite.Render(transform.pos, cam);
        }

        public override void Update(float dt, Dictionary<int, List<Tile>> surroundings) {
            float speed = 5f;
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
            if(Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) && onGround) {
                move.y-=50f;
                onGround = false;
            }
            List<Tile> collide = new List<Tile>();
            foreach (int layer in COLLIDE_LAYERS)
            {
                if (surroundings.ContainsKey(layer))
                {
                    collide.AddRange(surroundings[layer]);
                }
            }

            Move(move*dt, collide);
        }
        public override void OnCollision(Dictionary<string, bool> dirs)
        {
            if(dirs["bottom"]) {
                onGround = true;
            }
        }
    }
}