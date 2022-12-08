using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameEngine
{
    public class Vector2D
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2D()
        {
            this.X = Zero().X;
            this.Y = Zero().Y;
        }
        public Vector2D(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
        static Vector2D Zero()
        {
            return new Vector2D(0, 0);
        }
        public float Magnitude()
        {
            return (float)(Math.Sqrt(this.X * this.X + this.Y * this.Y));
        }
        public static Vector2D operator+(Vector2D A,Vector2D B)
        {
            Vector2D temp = new Vector2D();
            temp.X = A.X + B.X;
            temp.Y = A.Y + B.Y;
            return temp;
        }
        public static Vector2D operator-(Vector2D A, Vector2D B)
        {
            Vector2D temp = new Vector2D();
            temp.X = A.X - B.X;
            temp.Y = A.Y - B.Y;
            return temp;
        }

    }
}
