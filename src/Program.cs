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
        }
    }
}