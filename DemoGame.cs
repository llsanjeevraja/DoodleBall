using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GameEngine
{
    class DemoGame : GameEngine.GameEngine
    {
        public DemoGame() : base(new GameEngine.Vector2D((int)Screen.PrimaryScreen.WorkingArea.Width, (int)Screen.PrimaryScreen.WorkingArea.Height), "New Game")
        {
        }
        public override void OnLoad()
        {
            Console.WriteLine(" On Loading .....");
        }
        public override void OnUpdate()
        {
            //Console.WriteLine("Updating .....");
        }
        public override void OnDraw()
        {
            //Console.WriteLine("On Draw .....");
        }

    }
}
