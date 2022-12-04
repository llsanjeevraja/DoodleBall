using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameEngine
{
    public class myGameInput
    {
        public Vector2D mouseLocation;
        public bool lButton { get; set; }
        public bool rButton { get; set; }

        public myGameInput()
        {
            mouseLocation = new Vector2D(0,0);
            rButton = false;
            lButton = false;
        }
        public void updateMouseLocation(Point newLocation)
        {
            this.mouseLocation.X = newLocation.X;
            this.mouseLocation.Y = newLocation.Y;   
        }
    }
}
