using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

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
        public Vector2D ScreenSize = new Vector2D(800,600);
        private string Title="My Game";
        private Canvas Window = null;
        private Thread GameLoopthread = null;

        public GameEngine(Vector2D screenSize, string title)
        {
            ScreenSize = screenSize;
            Title = title;
            Window = new Canvas();
            Window.FormBorderStyle = FormBorderStyle.None;
            Window.WindowState = FormWindowState.Maximized;
            Window.Size = new Size((int)screenSize.X, (int)screenSize.Y);
            Window.Text = this.Title;
            Window.Location = new Point(0, 0);
            Window.Paint += Renderer;
            Window.KeyPress += Window_KeyPress;
            Window.MouseUp += Window_MouseUp;
            Window.MouseMove += Window_MouseMove;
            Window.MouseDown += Window_MouseDown;
            GameLoopthread=new Thread(GameLoop);
            GameLoopthread.Start();
            Application.Run(Window);
            
        }

       

        void GameLoop()
        {
            OnLoad();
            try
            {
                while (GameLoopthread.IsAlive)
                {
                    OnDraw();
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    OnUpdate();
                    Thread.Sleep(5);
                }
            }
            catch
            {
                Console.WriteLine("Game is Loading......");
            }
        }
        int frame = 0;
        

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void Renderer(object sender, PaintEventArgs e);
        public abstract void OnDraw();
        public abstract void Window_KeyPress(object sender, KeyPressEventArgs e);
        public abstract void Window_MouseUp(object sender, MouseEventArgs e);
        public abstract void Window_MouseMove(object sender, MouseEventArgs e);
        public abstract void Window_MouseDown(object sender, MouseEventArgs e);




    }
}
