using Raylib_cs;
using System;
using CSLib;
namespace CSLib
{
    class MainGame
    {
        Scene currentScene = null;
        string title;
        public Vec2 winSize;
        bool quitFlag = false;
        public MainGame(Vec2 winSize, string title, int fps)
        {
            Raylib.SetTargetFPS(fps);
            this.winSize = winSize;
            this.title = title;
            Raylib.InitWindow((int)winSize.x, (int)winSize.y, title);
        }
        // public void Run()
        // {
        //     while (!Raylib.WindowShouldClose() && !quitFlag)
        //     {
        //         float dt = Raylib.GetFrameTime();
        //         if (currentScene != null)
        //         {
        //             currentScene.Update(dt);
        //             currentScene.Render();
        //         }
        //         else
        //         {
        //             Raylib.ClearBackground(Color.BLACK);
        //             Raylib.BeginDrawing();
        //             Raylib.DrawText("No Scene Selected", 50, 50, 10, Color.WHITE);
        //             Raylib.EndDrawing();
        //         }
        //     }
        //     Raylib.CloseWindow();
        // }
        public void Run() {
            const int ups = 30;
            const double dt = (double)ups/1000;
            double currentTime = Raylib.GetTime();
            double accumulator = 0;
            while(!Raylib.WindowShouldClose() && !quitFlag) {
                double currentTIme = Raylib.GetTime();
                accumulator += Raylib.GetFrameTime();
                while(accumulator >=dt) {
                    currentScene.Update();
                    accumulator -= dt;
                }
                currentScene.Render();
            }
            Raylib.CloseWindow();
        }
        public Scene SetScene(Scene s)
        {
            currentScene = s;
            return s;
        }
        public void Quit()
        {
            quitFlag = true;
        }
    }
}