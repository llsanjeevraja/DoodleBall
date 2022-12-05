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
        private bool boolFullScreen = false;
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
            Window.KeyDown += Window_KeyDown;
            Window.KeyUp += Window_KeyUp;
            Window.MouseUp += Window_MouseUp;
            Window.MouseMove += Window_MouseMove;
            Window.MouseDown += Window_MouseDown;
            Window.Resize += Window_Resize;
            GameLoopthread=new Thread(GameLoop);
            GameLoopthread.Start();
            Application.Run(Window);
            
        }



        void GameLoop()
        {
            OnLoad();
            try
            {
                DateTime frameStart;
                DateTime frameEnd;
                double FixedFrameRate = 0.02;// formaintaining frame rate of 50
                double deltaTime = 1;
                while (GameLoopthread.IsAlive)
                {
                    frameStart = DateTime.Now;
                    OnUpdate(deltaTime);
                    Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                    Thread.Sleep(5);
                    frameEnd = DateTime.Now;
                    deltaTime = (frameEnd - frameStart).Milliseconds;
                    
                }
            }
            catch
            {
                Console.WriteLine("Game is Loading......");
            }
        }
        public void ToggleFullScreen()
        {
            if (boolFullScreen)
            {
                this.Window.WindowState = FormWindowState.Normal;
                this.Window.FormBorderStyle = FormBorderStyle.FixedDialog;
            }
            else
            {
                this.Window.FormBorderStyle = FormBorderStyle.None;
                this.Window.WindowState = FormWindowState.Maximized;
            }
            boolFullScreen = !boolFullScreen;
        }
        

        public abstract void OnLoad();
        public abstract void OnUpdate(double deltatime);
        public abstract void Renderer(object sender, PaintEventArgs e);
        public abstract void Window_KeyPress(object sender, KeyPressEventArgs e);
        public abstract void Window_KeyDown(object sender, KeyEventArgs e);
        public abstract void Window_KeyUp(object sender, KeyEventArgs e);
        public abstract void Window_MouseUp(object sender, MouseEventArgs e);
        public abstract void Window_MouseMove(object sender, MouseEventArgs e);
        public abstract void Window_MouseDown(object sender, MouseEventArgs e);
        public abstract void Window_Resize(object sender, EventArgs e);



    }
}
