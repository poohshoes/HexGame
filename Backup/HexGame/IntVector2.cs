using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HexGame
{
    public struct IntVector2 {
        public int X;
        public int Y;

        public static IntVector2 Zero 
        {
            get
            {
                return new IntVector2(0, 0);
            }
        }

        public IntVector2(int x, int y) {
            X = x;
            Y = y;
        }

        public IntVector2(float x, float y)
        {
            X = Convert.ToInt32(x);
            Y = Convert.ToInt32(y);
        }

        public static IntVector2 operator +(IntVector2 a, IntVector2 b) {
            return new IntVector2(a.X + b.X, a.Y + b.Y);
        }

        public static IntVector2 operator -(IntVector2 a, IntVector2 b) {
            return new IntVector2(a.X - b.X, a.Y - b.Y);
        }

        public static IntVector2 operator -(IntVector2 a) {
            return new IntVector2(-a.X, -a.Y);
        }

        public static IntVector2 operator *(IntVector2 a, int i) {
            return new IntVector2(a.X * i, a.Y * i);
        }

        public static IntVector2 operator /(IntVector2 a, int i) {
            return new IntVector2(a.X / i, a.Y / i);
        }

        public static bool operator ==(IntVector2 a, IntVector2 b) {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(IntVector2 a, IntVector2 b) {
            return !(a == b);//a.X != b.X || a.Y != b.Y;
        }

        public override bool Equals(object obj) {
            return (obj is IntVector2 && this == (IntVector2)obj);
        }

        public override int GetHashCode() {
            return X.GetHashCode() + Y.GetHashCode();
        }

        public float Length()
        {
            return (float)Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
        }

        public Microsoft.Xna.Framework.Vector2 ToVector2() {
            return new Microsoft.Xna.Framework.Vector2(X, Y);
        }

        public Microsoft.Xna.Framework.Vector3 ToVector3() {
            return new Microsoft.Xna.Framework.Vector3(X, Y, 0);
        }
    }
}
