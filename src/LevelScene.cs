using Raylib_cs;
using static Raylib_cs.Raylib;
using System;
using System.Collections.Generic;
using CSLib;
namespace Powerless
{
    class LevelScene : Scene
    {
        TileMap tilemap;
        TextureAtlas tileAtlas;
        TextureAtlas spriteAtlas;
        Camera cam;
        Entity character;
        public LevelScene(MainGame game) : base(game)
        {
            tileAtlas = TextureAtlas.FromFile("assets/images/tiles/test.atlas");
            tilemap = TileMap.FromFile("assets/tilemaps/test.ptm");
            Utils.SortTMLayers(tilemap);
            cam = new Camera(game.winSize, 2, 16);
            // character = new Entity(new Vec2(0,0), new Vec2(1,1), )
        }

        public override void Update(float dt)
        {
            float speed = 15f * dt;
            Vec2 move = Vec2.Zero;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
            {
                move.y -= speed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
            {
                move.y += speed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
            {
                move.x -= speed;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
            {
                move.x += speed;
            }
            cam.Move(move);
        }
        public override void Render()
        {
            BeginDrawing();
            ClearBackground(Color.BLACK);
            BeginMode2D(cam.camera);
            Dictionary<int, Dictionary<Vec2, Tile>> tiles = tilemap.GetVisible(cam);
            foreach (int z in tilemap.layers)
            {
                Dictionary<Vec2, Tile> layer = tiles[z];
                foreach (Tile t in layer.Values)
                {
                    tileAtlas.DrawTexture(t.type, t.pos * cam.pixelsPerUnit);
                }
            }
            EndMode2D();
            DrawFPS(0, 0);
            EndDrawing();
        }
        public override void Quit()
        {

        }
    }

}
