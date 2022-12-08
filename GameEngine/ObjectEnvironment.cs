using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameEngine
{
    public  class Environment
    {
        public Vector2D frictionConstant { get; set; } = new Vector2D(0.85f, 0.75f);
        public bool boolGravityOn { get; set; } = false;

        

    }
}
