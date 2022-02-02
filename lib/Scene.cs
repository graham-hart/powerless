using Raylib_cs;
using System;
using System.Collections.Generic;
namespace CSLib
{
    abstract class Scene
    {
        public MainGame game;
        public Scene(MainGame game)
        {
            this.game = game;
        }
        public abstract void Update();
        public abstract void Render();
        public abstract void Quit();
    }
}