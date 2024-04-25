using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class Vector2D : ICloneable
    {
        public virtual int x { get; set; }
        public virtual int y { get; set; }

        public Vector2D() : this(0, 0) { }

        public Vector2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2D Set(Vector2D other)
        {
            x = other.x;
            y = other.y;
            return this;
        }

        public Vector2D Add(Vector2D other)
        {
            x += other.x;
            y += other.y;
            return this;
        }

        public Vector2D Subtract(Vector2D other)
        {
            x -= other.x;
            y -= other.y;
            return this;
        }

        public Vector2D Multiply(Vector2D other)
        {
            x *= other.x;
            y *= other.y;
            return this;
        }

        public Vector2D Divide(Vector2D other)
        {
            x /= other.x;
            y /= other.y;
            return this;
        }

        public static Vector2D operator +(Vector2D lhs, Vector2D rhs)
        {
            return lhs.Clone().Add(rhs);
        }

        public static Vector2D operator -(Vector2D lhs, Vector2D rhs)
        {
            return lhs.Clone().Subtract(rhs);
        }

        public static Vector2D operator *(Vector2D lhs, Vector2D rhs)
        {
            return lhs.Clone().Multiply(rhs);
        }

        public static Vector2D operator /(Vector2D lhs, Vector2D rhs)
        {
            return lhs.Clone().Divide(rhs);
        }

        public bool Equals(Vector2D? v)
        {
            if (v is null)
            {
                return false;
            }

            if (ReferenceEquals(this, v))
            {
                return true;
            }

            if (GetType() != v.GetType())
            {
                return false;
            }

            return x == v.x && y == v.y;
        }

        public static bool operator ==(Vector2D? lhs, Vector2D? rhs)
        {
            if (lhs is null)
            {
                return rhs is null;
            }

            return lhs.Equals(rhs);
        }

        public static bool operator !=(Vector2D? lhs, Vector2D? rhs) => !(lhs == rhs);

        public override bool Equals(object? obj)
        {
            return obj is Vector2D && Equals((Vector2D)obj);
        }

        public override int GetHashCode() => (x, y).GetHashCode();

        public abstract Vector2D Clone();

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
