using Raylib_cs;
using System;
namespace CSLib
{
    abstract class Scene
    {
        public MainGame game;
        public Scene(MainGame game)
        {
            this.game = game;
        }
        public abstract void Update(float dt);
        public abstract void Render();
        public abstract void Quit();
    }
}