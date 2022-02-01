using CSLib;
using System;
namespace Powerless
{
    class Game : MainGame
    {
        public Game(Vec2 winSize, string title) : base(winSize, title, 120)
        {
            RigidBody.GRAVITY = Config.GRAVITY;
            RigidBody.GROUND_FRICTION = Config.GROUND_FRICTION;
            RigidBody.AIR_RESISTANCE = Config.AIR_RESISTANCE;
            SetScene(new LevelScene(this));
        }
    }
}