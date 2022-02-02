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
        TextureAtlas tileAtlas;
        TextureAtlas spriteAtlas;
        TextureAtlas uiAtlas;
        Camera cam;
        Camera uiCam;
        Player player;
        Dictionary<int, List<Tile>> visibleTiles;
        public LevelScene(MainGame game) : base(game)
        {
            tileAtlas = TextureAtlas.FromFile("assets/images/tiles/atlas/atlas.atlas", "tile");
            spriteAtlas = TextureAtlas.FromFile("./assets/images/sprites/atlas/atlas.atlas", "sprite");
            uiAtlas = TextureAtlas.FromFile("./assets/images/ui/atlas/atlas.atlas", "ui");
            tilemap = TileMap.FromFile("assets/tilemaps/level01.ptm");
            Utils.SortTMLayers(tilemap);
            cam = new Camera(game.winSize, 3, 16);
            uiCam = new Camera(game.winSize, 5, 1);
            uiCam.camera.offset = new System.Numerics.Vector2(0, 0);
            player = new Player(new Vec2(0, -1));
            visibleTiles = tilemap.GetVisible(cam);
        }
        public override void Update()
        {
            player.Update(visibleTiles);
            Utils.CameraLerp(cam, player.transform.pos, .4, 4);
            visibleTiles = tilemap.GetVisible(cam);
        }
        public override void Render()
        {
            BeginDrawing();
            ClearBackground(Color.BLACK);
            BeginMode2D(cam.camera);
            foreach (int z in tilemap.layers)
            {
                if (visibleTiles.ContainsKey(z))
                {
                    List<Tile> layer = visibleTiles[z];
                    foreach (Tile t in layer)
                    {
                        tileAtlas.DrawTexture(t.type, t.pos * cam.pixelsPerUnit);
                    }
                }
            }
            player.Render(cam);
            EndMode2D();
            BeginMode2D(uiCam.camera);
            uiAtlas.DrawTexture("hud", new Vec2(0, 0));
            uiAtlas.DrawTexture("energybar_" + Math.Round(Math.Clamp(player.energy, 0, 8)), new Vec2(2, 2));
            EndMode2D();
            EndDrawing();
        }
        public override void Quit()
        {

        }
    }
}
