using Raylib_cs;
using System.Numerics;
using System;
namespace CSLib
{
    class Camera // Wrapper for Raylib Camera2D struct, adds more functionality
    {
        public Camera2D camera;
        public int pixelsPerUnit;
        private Vec2 screenSize;
        private Vec2 worldSize;
        public Vec2 pos;
        public Vec2 TopLeft
        {
            get { return new Vec2(UnprojectX(0), UnprojectY(0)); }
        }
        public Vec2 BottomRight
        {
            get { return new Vec2(UnprojectX(screenSize.x), UnprojectY(screenSize.y)); }
        }
        public Vec2 Position
        {
            get { return new Vec2(UnprojectX(0) + worldSize.x / 2, UnprojectY(0) + worldSize.y / 2); }
        }
        // public vec2 
        public Camera(Vec2 screenSize, float scale, int ppu)
        {
            pixelsPerUnit = ppu; // Size of one "unit" unzoomed
            camera = new Camera2D(); // Raylib camera (used for actual rendering)
            camera.target = new Vector2(0f, 0f);
            camera.offset = new Vector2((float)screenSize.x / 2, (float)screenSize.y / 2);
            pos = new Vec2();
            camera.zoom = scale;
            camera.rotation = 0f;
            this.worldSize = screenSize / (ppu * scale);
            this.screenSize = screenSize;
        }
        public double ProjectX(double x)
        {
            return x * pixelsPerUnit * camera.zoom - camera.target.X/camera.zoom + camera.offset.X;
        }
        public double ProjectXDist(double x)
        {
            return x * pixelsPerUnit * camera.zoom;
        }
        public double UnprojectX(double x)
        {
            return (x - camera.offset.X + camera.target.X*camera.zoom) / (pixelsPerUnit * camera.zoom);
        }
        public double UnprojectXDist(double x)
        {
            return x / (pixelsPerUnit * camera.zoom);
        }
        public double ProjectY(double y)
        {
            return y * pixelsPerUnit * camera.zoom - camera.target.Y/camera.zoom + camera.offset.Y;
        }
        public double ProjectYDist(double y)
        {
            return y * pixelsPerUnit * camera.zoom;
        }
        public double UnprojectY(double y)
        {
            return (y - camera.offset.Y + camera.target.Y*camera.zoom) / (pixelsPerUnit * camera.zoom);
        }
        public double UnprojectYDist(double y)
        {
            return y / (pixelsPerUnit * camera.zoom);
        }
        public Vec2 Project(Vec2 coords)
        {
            return new Vec2(ProjectX(coords.x), ProjectY(coords.y));
        }
        public Vec2 ProjectDist(Vec2 dist)
        {
            return new Vec2(ProjectXDist(dist.x), ProjectYDist(dist.y));
        }
        public Vec2 Unproject(Vec2 coords)
        {
            return new Vec2(UnprojectX(coords.x), UnprojectY(coords.y));
        }
        public Vec2 UnprojectDist(Vec2 dist)
        {
            return new Vec2(UnprojectXDist(dist.x), UnprojectYDist(dist.y));
        }
        public Rect ProjectRect(Rect r)
        {
            return new Rect(ProjectX(r.X), ProjectY(r.Y), ProjectXDist(r.Width), ProjectYDist(r.Height));
        }
        public Rect UnprojectRect(Rect r)
        {
            return new Rect(UnprojectX(r.X), UnprojectY(r.Y), UnprojectXDist(r.Width), UnprojectYDist(r.Height));
        }
        public void Zoom(float zoom)
        {
            camera.zoom += zoom;
        }
        public void Move(Vec2 change)
        {
            camera.target.X += (float)ProjectXDist(change.x);
            camera.target.Y += (float)ProjectYDist(change.y);
            this.pos.Add(change);
        }
        public void SetPos(Vec2 pos)
        {
            camera.target.X = (float)ProjectXDist(pos.x)/camera.zoom;
            camera.target.Y = (float)ProjectYDist(pos.y)/camera.zoom;
            this.pos = pos;
        }
        public void LimitDist(Vec2 pos, double maxDist) {
            Vec2 dir = this.pos - pos;
            dir.LimitLength(maxDist);
            SetPos(pos+dir);
        }
    }
}
