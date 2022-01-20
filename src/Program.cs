using Raylib_cs;
using CSLib;
using System;
namespace Powerless
{
    static class Program
    {
        public static void Main()
        {
            Game game = new Game(new Vec2(1600, 900), "Powerless");
            game.Run();
            // Camera cam = new Camera(new Vec2(500, 500), 8);
            // Console.WriteLine(cam.ProjectX(1));
        }
    }
}