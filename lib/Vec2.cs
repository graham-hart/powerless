using System;
namespace CSLib
{
    public class Vec2
    {
        public double x, y;
        public System.Numerics.Vector2 SystemV2
        {
            get { return new System.Numerics.Vector2((float)x, (float)y); }
        }
        public Vec2(System.Numerics.Vector2 v)
        {
            this.x = (double)v.X;
            this.y = (double)v.Y;
        }
        public Vec2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public Vec2(Vec2 v)
        {
            x = v.x;
            y = v.y;
        }
        public Vec2()
        {
            x = 0;
            y = 0;
        }
        public static Vec2 Zero
        {
            get
            {
                return new Vec2(0, 0);
            }
        }
        public static Vec2 One
        {
            get
            {
                return new Vec2(1, 1);
            }
        }
        public static Vec2 Left
        {
            get
            {
                return new Vec2(-1, 0);
            }
        }
        public static Vec2 Right
        {
            get
            {
                return new Vec2(1, 0);
            }
        }
        public static Vec2 Up
        {
            get
            {
                return new Vec2(0, -1);
            }
        }
        public static Vec2 Down
        {
            get
            {
                return new Vec2(0, 1);
            }
        }
        public static Vec2 operator +(Vec2 a, Vec2 b)
        {
            return new Vec2(b.x + a.x, b.y + a.y);
        }
        public static Vec2 operator -(Vec2 a, Vec2 b)
        {
            return new Vec2(a.x - b.x, a.y - b.y);
        }
        public static Vec2 operator *(Vec2 a, double scalar)
        {
            return new Vec2(a.x * scalar, a.y * scalar);
        }
        public static Vec2 operator *(Vec2 a, float scalar)
        {
            return new Vec2(a.x * scalar, a.y * scalar);
        }
        public static Vec2 operator *(Vec2 a, Vec2 b)
        {
            return new Vec2(a.x * b.x, a.y * b.y);
        }
        public static Vec2 operator /(Vec2 a, double x)
        {
            if (x == 0)
            {
                throw new DivideByZeroException();
            }
            return new Vec2(a.x / x, a.y / x);
        }
        public static Vec2 operator /(Vec2 a, float x)
        {
            if (x == 0)
            {
                throw new DivideByZeroException();
            }
            return new Vec2(a.x / x, a.y / x);
        }
        public static Vec2 operator /(Vec2 a, Vec2 b)
        {
            return new Vec2(a.x / b.x, a.y / b.y);
        }
        public static bool operator ==(Vec2 a, Vec2 b)
        {
            return a.x == b.x && a.y == b.y;
        }
        public static bool operator !=(Vec2 a, Vec2 b)
        {
            return a.x != b.x || a.y != b.y;
        }
        public override bool Equals(object o)
        {
            return Equals(o as Vec2);
        }
        public bool Equals(Vec2 o)
        {
            return o.x == x && o.y == y;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }
        public void Div(double a)
        {
            x /= a;
            y /= a;
        }
        public void Mult(double scalar)
        {
            x *= scalar;
            y *= scalar;
        }
        public void Add(Vec2 o)
        {
            x += o.x;
            y += o.y;
        }
        public void Sub(Vec2 o)
        {
            x -= o.x;
            y -= o.y;
        }
        public void Add(double x, double y)
        {
            this.x += x;
            this.y += y;
        }
        public void Sub(double x, double y)
        {
            this.x -= x;
            this.y -= y;
        }
        public Vec2 Copy()
        {
            return new Vec2(x, y);
        }
        public double Angle()
        {
            double a = System.Math.Atan2(y, x);
            if (a < 0)
            {
                a += System.Math.PI * 2;
            }
            return a;
        }
        public static Vec2 FromAngle(double angle, double size)
        {
            return new Vec2(System.Math.Cos(angle) * size, System.Math.Sin(angle) * size);
        }
        public static Vec2 FromAngle(double angle)
        {
            return new Vec2(System.Math.Cos(angle), System.Math.Sin(angle));
        }
        public double Dot(Vec2 o)
        {
            return x * o.x + y * o.y;
        }
        public double SqMagnitude()
        {
            return (x * x) + (y * y);
        }
        public double Magnitude()
        {
            return System.Math.Sqrt((x * x) + (y * y));
        }
        public double Length()
        {
            return System.Math.Sqrt((x * x) + (y * y));
        }
        public double SqLength()
        {
            return (x * x) + (y * y);
        }
        public void Normalize()
        {
            Mult(1 / Magnitude());
        }
        public void SetLength(double len)
        {
            Mult(len / Magnitude());
        }
        public double DistanceTo(Vec2 o)
        {
            return (this - o).Length();
        }
        public double SqDistanceTo(Vec2 o)
        {
            return (this - o).SqLength();
        }
        public void Set(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public void Set(Vec2 v)
        {
            x = v.x;
            y = v.y;
        }
        public Vec2 Floored()
        {
            return new Vec2(Math.Floor(x), Math.Floor(y));
        }
        public Vec2 Rounded()
        {
            return new Vec2(Math.Round(x), Math.Round(y));
        }
        public Vec2 Ceilinged()
        {
            return new Vec2(Math.Ceiling(x), Math.Ceiling(y));
        }
        public override string ToString()
        {
            return string.Format("[{0}, {1}]", x, y);
        }
    }
}
