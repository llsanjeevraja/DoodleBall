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
        private Vector2D ScreenSize = new Vector2D(800,600);
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
            GameLoopthread=new Thread(GameLoop);
            GameLoopthread.Start();
            Application.Run(Window);
            
        }

        private void Window_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar== (char)Keys.Escape)
                Application.Exit();
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
        private void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            frame++;
            g.Clear(Color.Black);
            
            g.DrawString($" This is rendering Window {frame}",new Font(FontFamily.GenericSerif,12),Brushes.White,100.0f,100.0f);
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();  
        public abstract void OnDraw();

        

    }
}
