using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameEngine
{
        public class Vector2D
        {
            double X { get; set; }
            double Y { get; set; }

            public Vector2D()
            {
                this.X = Zero().X;
                this.Y = Zero().Y;
            }
            public Vector2D(double x, double y)
            {
                this.X = x;
                this.Y = y;
            }
            static Vector2D Zero()
            {
                return new Vector2D(0, 0);
            }
        }

}
