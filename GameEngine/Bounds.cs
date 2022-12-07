using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameEngine
{
    public class bounds
    {
        public float left { get; set; }
        public float right { get; set; }
        public float top { get; set; }
        public float bottom { get; set; } 

        public void Set(Vector2D screensize)
        {
            this.left=screensize.X*0.1f;
            this.right=screensize.X*0.9f;
            this.top = screensize.Y * 0.1f;
            this.bottom = screensize.Y * 0.9f;
        }
        
    }
}
