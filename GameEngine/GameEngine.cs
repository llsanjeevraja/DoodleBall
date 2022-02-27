using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace GameEngine.GameEngine
{
    class Canvas : Form
    {
        public Canvas()
        {
            this.DoubleBuffered = true;

        }
    }
    public abstract class GameEngine
    {
        private Vector2D ScreenSize = new Vector2D(Screen.PrimaryScreen.WorkingArea.X,Screen.PrimaryScreen.WorkingArea.Y);
        private string Title="New Game";
        Canvas Window = null;
        protected GameEngine(Vector2D screenSize, string title)
        {
            ScreenSize = screenSize;
            Title = title;
            Window = new Canvas();
            Window.Size = new Size((int)screenSize.X, (int)screenSize.Y);
            Window.Text = this.Title;

            Application.Run(Window);
        }

    }
}
