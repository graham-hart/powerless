using Raylib_cs;
using static Raylib_cs.Raylib;
using System;
using System.Collections.Generic;
using CSLib;
using System.Linq;
using static Powerless.Config;

namespace Powerless
{
    class LevelScene : Scene
    {
        TileMap tilemap;
        TextureAtlas tileAtlas;
        TextureAtlas spriteAtlas;
        Camera cam;
        Player player;
        Dictionary<int, List<Tile>> visibleTiles;
        public LevelScene(MainGame game) : base(game)
        {
            tileAtlas = TextureAtlas.FromFile("assets/images/tiles/atlas/atlas.txt", "tile");
            spriteAtlas = TextureAtlas.FromFile("./assets/images/sprites/atlas/atlas.txt", "sprite");
            tilemap = TileMap.FromFile("assets/tilemaps/test.ptm");
            Utils.SortTMLayers(tilemap);
            cam = new Camera(game.winSize, 3, 16);
            player = new Player(new Vec2(0, -1));
        }

        public override void Update(float dt)
        {
            visibleTiles = tilemap.GetVisible(cam);
            player.Update(dt, visibleTiles);
            cam.SetPos(player.transform.pos); 
        }
        public override void Render()
        {
            BeginDrawing();
            ClearBackground(Color.BLACK);
            BeginMode2D(cam.camera);
            foreach (int z in tilemap.layers)
            {
                List<Tile> layer = visibleTiles[z];
                foreach (Tile t in layer)
                {
                    tileAtlas.DrawTexture(t.type, t.pos * cam.pixelsPerUnit);
                }
            }
            player.Render(cam);
            EndMode2D();
            DrawFPS(0, 0);
            EndDrawing();
        }
        public override void Quit()
        {

        }
    }

}
