using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Collections.Generic;
using CSLib;
using System;
using static CSLib.Config;

namespace Powerless
{
    class LevelScene : Scene
    {
        TileMap tilemap;
        Camera cam;
        Camera uiCam;
        Player player;
        public LevelScene(MainGame game) : base(game)
        {
            TextureAtlas.LoadFile("./assets/textures/atlas/atlas.atlas");
            tilemap = TileMap.FromFile("assets/tilemaps/level-01.ptm");
            Utils.SortTMLayers(tilemap);
            cam = new Camera(game.winSize, 3, 16);
            uiCam = new Camera(game.winSize, 5, 1);
            uiCam.camera.offset = new System.Numerics.Vector2(0, 0);
            player = new Player(new Vec2(0, -1));
            tilemap.UpdateVisibleTiles(cam);
        }
        public override void Update()
        {
            player.Update(tilemap.visibleTiles);
            Utils.CameraLerp(cam, player.transform.pos, .4, 4);
            tilemap.UpdateVisibleTiles(cam);
        }
        public override void Render()
        {
            BeginDrawing();
                ClearBackground(Color.BLACK);
                BeginMode2D(cam.camera);
                    tilemap.Render(cam);
                    player.Render(cam);
                EndMode2D();
                BeginMode2D(uiCam.camera);
                    TextureAtlas.DrawTexture("healthbar_" + Math.Round(Math.Clamp(8-Utils.Map(player.energy, 0, MAX_ENERGY, 0, 8), 0, 8)), new Vec2(0,0));
                EndMode2D();
            EndDrawing();
        }
        public override void Quit()
        {

        }
    }
}
