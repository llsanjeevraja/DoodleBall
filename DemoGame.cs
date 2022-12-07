using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using GameEngine.GameEngine;
using System.Windows.Forms.VisualStyles;
using System.Media;

namespace GameEngine
{
    class DemoGame : GameEngine.GameEngine
    {
        private long frame = 0;
        private double deltaTime = 100;
        private int infoTextSize = 12;
        private bool boolShowInfo = true;
        public Stage Stage1 = new Stage();
        
        public DemoGame() : base("Doodle Ball")
        {
           
        }
        // class functions//methods
        //game loop functions
        public override void OnLoad()
        {
            //called only once.
            Stage1.OnLoad(this.ScreenSize);
            
        }
        public override void OnUpdate(double deltatime)
        {
            Stage1.OnUpdate();
            this.deltaTime = deltatime;
            //called each time in loop after draw
            
            frame++;
        }
        public override void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            Stage1.Draw(g);
            
            if (boolShowInfo)
                ShowScreenInfo(g);

        }

        // input handlers
        public override void Window_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                Application.Exit();
            else if (e.KeyChar == (char)Keys.I || e.KeyChar == (char)Keys.I + 32)
                boolShowInfo = !boolShowInfo;
            else if (e.KeyChar == (char)Keys.F || e.KeyChar == (char)Keys.F + 32)
                ToggleFullScreen();
            else if (e.KeyChar == (char)Keys.R || e.KeyChar == (char)Keys.R + 32)
                Stage1.Reset();

        }
        public override void Window_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Stage1.Input.updateMouseLocation(e.Location);
                this.Stage1.Input.lButton = !this.Stage1.Input.lButton;
            }
        }
        public override void Window_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Stage1.Input.lButton = !this.Stage1.Input.lButton;
                this.Stage1.Input.updateMouseLocation(e.Location);
            }

        }
        public override void Window_MouseMove(object sender, MouseEventArgs e)
        {
            this.Stage1.Input.updateMouseLocation(e.Location);
        }
        public override void Window_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            this.ScreenSize.X = control.Width;
            this.ScreenSize.Y = control.Height;
            this.Stage1.Bounds.Set(ScreenSize);
        }

        // add these function to class for conversion of point system;
        

        private void ShowScreenInfo(Graphics g)
        {
            int infoTextLocationCounter = 0;
            //g.DrawString($" Size of Windows = {this.ScreenSize.X}x{this.ScreenSize.Y}", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            g.DrawString($" Frame Rate = {1000 / deltaTime}", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            //g.DrawString($" Shortcuts :", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            //g.DrawString($" 'Esc' : Quit Game", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            //g.DrawString($" 'F'   : Toggle Full Screen ", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            //g.DrawString($" 'I'   : Toggole Info ", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            //g.DrawString($" 'P'   : Play/Pause ", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            Stage1.ShowInfo(g, infoTextLocationCounter);
        }
    }
}