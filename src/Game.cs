using CSLib;
using System;
namespace Powerless
{
    class Game : MainGame
    {
        public Game(Vec2 winSize, string title) : base(winSize, title, 120)
        {
            SetScene(new LevelScene(this));
        }
    }
}