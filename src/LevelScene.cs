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
        int level;
        List<Button> btns = new List<Button>();
        Button restartButton = null;
        public LevelScene(MainGame game, int level) : base(game)
        {

            TextureAtlas.LoadFile("./assets/textures/atlas/atlas.atlas");
            tilemap = TileMap.FromFile("assets/tilemaps/level-" + level + ".ptm");
            this.level = level;
            Utils.SortTMLayers(tilemap);
            cam = new Camera(game.winSize, 3, 16);
            uiCam = new Camera(game.winSize, 5, 1);
            uiCam.camera.offset = new System.Numerics.Vector2(0, 0);
            player = new Player(new Vec2(0, -1));
            tilemap.UpdateVisibleTiles(cam);
        }
        public override void Update()
        {
            if (player.alive)
            {
                player.Update(tilemap.visibleTiles);
                Utils.CameraLerp(cam, player.transform.pos, .4, 4);
                tilemap.UpdateVisibleTiles(cam);
            }
            else
            {
                if (btns.Count == 0)
                {
                    btns.Add(Button.FitToText("Restart", game.winSize / 2, 24, () => { game.SetScene(new LevelScene(game, level)); }, 12));
                    btns.Add(Button.FitToText("Quit", new Vec2(game.winSize.x/2, game.winSize.y - 36), 24, game.Quit, 12));
                }
                Vec2 mouse = Input.GetMousePos();
                if (Input.IsMouseButtonDown(MouseButton.MOUSE_LEFT_BUTTON))
                {
                    foreach (var btn in btns)
                    {
                        if (btn.rect.CollidePoint(mouse))
                        {
                            btn.OnClick();
                        }
                    }

                }
            }
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
            TextureAtlas.DrawTexture("healthbar_" + Math.Round(Math.Clamp(8 - Utils.Map(player.energy, 0, MAX_ENERGY, 0, 8), 0, 8)), new Vec2(0, 0));
            EndMode2D();
            if (!player.alive)
            {
                DrawRectangle(0, 0, (int)game.winSize.x, (int)game.winSize.y, new Color(0, 0, 0, 128));
                foreach(var btn in btns) {
                    btn.Render();
                }
            }
            EndDrawing();
        }
        public override void Quit()
        {

        }
    }
}
