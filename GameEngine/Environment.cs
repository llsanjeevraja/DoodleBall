using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameEngine
{
    
    public class Environment
    {
        public double mass { get; set; }
        public double terminalVelocity { get; set; }
        public Vector2D frictionConstant { get; set; }
        public bool HaveGravity { get; set; }
        public Environment()
        {
            this.mass = 100;
            this.frictionConstant = new Vector2D(0.8, 0.5);
            this.terminalVelocity = 10;
            this.HaveGravity = true;
        }

    }
}
