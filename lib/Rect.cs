using Raylib_cs;
using System;
namespace CSLib
{
    public class Rect
    {
        public Vec2 pos;
        public Vec2 size;
        public double X
        {
            get { return pos.x; }
            set { pos.x = value; }
        }
        public double Y
        {
            get { return pos.y; }
            set { pos.y = value; }
        }
        public double Width
        {
            get { return size.x; }
            set { size.x = value; }
        }
        public double Height
        {
            get { return size.y; }
            set { size.y = value; }
        }
        public double Left
        {
            get { return pos.x; }
            set { pos.x = value; }
        }
        public double Right
        {
            get { return pos.x + size.x; }
            set { pos.x = value - size.x; }
        }
        public double Top
        {
            get { return pos.y; }
            set { pos.y = value; }
        }
        public double Bottom
        {
            get { return pos.y + size.y; }
            set { pos.y = value - size.y; }
        }
        public Vec2 center
        {
            get { return pos + size / 2; }
            set { pos.Set(value.x - Width / 2, value.y - Height / 2); }
        }
        public Rect(double x, double y, double width, double height)
        {
            pos = new Vec2(x, y);
            size = new Vec2(width, height);
        }
        public Rect(Vec2 p, Vec2 s)
        {
            pos = p.Copy();
            size = s.Copy();
        }
        public Rect(Rectangle r) {
            pos = new Vec2(r.x, r.y);
            size = new Vec2(r.width, r.height);
        }
        public Rect Copy()
        {
            return new Rect(pos, size);
        }
        public void SetPos(Vec2 pos)
        {
            this.pos.Set(pos);
        }
        public void Move(Vec2 change)
        {
            pos.Add(change);
        }
        public Vec2 BottomRight()
        {
            return pos + size;
        }
        public bool CollideRect(Rect o)
        {
            Vec2 br = BottomRight();
            Vec2 obr = o.BottomRight();
            return br.x > o.X && X < obr.x && br.y > o.Y && Y < obr.y;
        }
        public bool CollidePoint(Vec2 p)
        {
            Vec2 br = BottomRight();
            return X <= p.x && p.x <= br.x && Y <= p.y && p.y <= br.y;
        }
        public bool CollideListAny(Rect[] lst)
        {
            foreach (Rect r in lst)
            {
                if (CollideRect(r))
                {
                    return true;
                }
            }
            return false;
        }
        public bool CollideListAll(Rect[] lst)
        {
            foreach (Rect r in lst)
            {
                if (!CollideRect(r))
                {
                    return false;
                }
            }
            return true;
        }
        public Rectangle RaylibRect()
        {
            return new Rectangle((float)X, (float)Y, (float)Width, (float)Height);
        }
    }
}