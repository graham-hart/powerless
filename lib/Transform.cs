using System;
namespace CSLib
{
    class Transform
    {
        public Vec2 pos;
        public Vec2 size;
        public Vec2 Center
        {
            get { return pos + size / 2; }
        }
        public Rect Rect
        {
            get { return new Rect(pos, size); }
        }
        public Transform(Vec2 pos, Vec2 size)
        {
            this.pos = pos;
            this.size = size;
        }
        public void Move(Vec2 change)
        {
            pos.Add(change);
        }
        public void Move(double x, double y)
        {
            pos.Add(x, y);
        }
    }
}