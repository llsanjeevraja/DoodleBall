using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameEngine
{
    public class bounds
    {
        public double left { get; set; }
        public double right { get; set; }
        public double top { get; set; }
        public double bottom { get; set; } 

        public void Set(Vector2D screensize)
        {
            this.left=screensize.X*0.1;
            this.right=screensize.X*0.9;
            this.top = screensize.Y * 0.1;
            this.bottom = screensize.Y * 0.9;
        }
        
    }
}
