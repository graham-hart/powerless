using CSLib;
using System;
using Raylib_cs;
using System.Collections.Generic;
using static CSLib.Config;
namespace Powerless
{
    class Player : Entity
    {
        bool onGround = true;
        const double MAX_VEL = .5;
        public double energy = 2;
        public Player(Vec2 pos) : base(pos, new Vec2(1, 1), new Sprite("testplayer"))
        {

        }

        public void Render(Camera cam)
        {
            sprite.Render(transform.pos, cam);
        }

        public override void Update(Dictionary<int, List<Tile>> surroundings)
        {
            float speed = .17f;
            Vec2 move = Vec2.Zero;
            if (Input.IsKeyDown(KeyboardKey.KEY_A))
            {
                move.x -= speed * (onGround ? 1 : 0.3);
            }
            if (Input.IsKeyDown(KeyboardKey.KEY_D))
            {
                move.x += speed * (onGround ? 1 : 0.3);
            }
            if(move != Vec2.Zero) {
                energy -= 0.005;
            }
            rb.AddVel(move * (energy <= 0 ? .5 : 1));
            if (Input.IsKeyDown(KeyboardKey.KEY_SPACE) && onGround)
            {
                energy -= 0.01;
                rb.velocity.y = 0;
                rb.AddVel(Vec2.Up * JUMP_FORCE * (energy <= 0 ? .6 : 1));
            }
            energy = Math.Clamp(energy, 0, MAX_ENERGY);
            if (Input.IsKeyPressed(KeyboardKey.KEY_E))
            {
                energy = MAX_ENERGY;
            }
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
        public override void OnCollision(Dictionary<string, List<Tile>> dirs)
        {
            if (dirs.ContainsKey("bottom"))
            {
                if(Utils.Any(dirs["bottom"], (val) => {return val.type == "tileset_battery";})) {
                    energy = MAX_ENERGY;
                }
                onGround = true;
            }
        }
    }
}