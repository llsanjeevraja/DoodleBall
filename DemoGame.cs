using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using GameEngine.GameEngine;


namespace GameEngine
{
    class DemoGame : GameEngine.GameEngine
    {
        private long frame = 0;
        private int infoTextSize = 12;
        private bool boolShowInfo = true;
        private int infoTextLocationCounter = 0;
        public ball myBall = new ball();
        myGameInput Input=new myGameInput();
        public DemoGame() : base(new GameEngine.Vector2D((int)Screen.PrimaryScreen.Bounds.Width, (int)Screen.PrimaryScreen.Bounds.Height), "New Game")
        {
            
        }
        // class functions//methods
        //game loop functions
        public override void OnLoad()
        {
            //called only once.
            myBall.location = CartiseanToScreen(0,0);
            myBall.Bounds.Set(this.ScreenSize);
        }
        public override void OnUpdate()
        {
            //called each time in loop after draw
            if (!myBall.IsReleased)
                myBall.UpdateLocation(Input.mouseLocation);
            this.CheckBounds();
            if (!myBall.IsLockedForThrow && !myBall.IsReleased && Input.lButton)// ball is loaded for play
            {
                myBall.Arrow.UpdateNochLocation(myBall.location);
                myBall.IsLockedForThrow = true;
            }
            else if (myBall.IsLockedForThrow && !myBall.IsReleased && Input.lButton)// ball is captured and being dragged for throw
            {
                myBall.Arrow.UpdatePoonchLocation(myBall.location);
            }
            else if (myBall.IsLockedForThrow && !myBall.IsReleased && !Input.lButton)// means that ball has been throw away
            {
                myBall.IsLockedForThrow = false;
                myBall.IsReleased = true;
                myBall.IntialiseMovement();
            }

            myBall.location.X = myBall.location.X;// + myBall.velocity.X;
            myBall.location.Y = myBall.location.Y;// + myBall.velocity.Y;
            frame++;
        }
        public override void OnDraw()// is called before the refresh call and upadate
        {
            
        }
        // input handlers 



        public override void Renderer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.Black);
            // enter your code here
            DrawBoundFrame(g);

            myBall.Draw(g);
            if (!myBall.IsReleased && myBall.IsLockedForThrow)
                myBall.Arrow.Draw(g);

            if (boolShowInfo)
                ShowScreenInfo(g);

        }
        private void DrawBoundFrame(Graphics g)
        {
            int tleft = (int)myBall.Bounds.left-(int)myBall.size/2;
            int ttop = (int)myBall.Bounds.top - (int)myBall.size / 2;
            int tright = (int)myBall.Bounds.right + (int)myBall.size / 2;
            int tbottom= (int)myBall.Bounds.bottom + (int)myBall.size / 2;

            g.DrawLine(new Pen(Brushes.Orange, 2),new Point(tleft, ttop), new Point(tright, ttop));
            g.DrawLine(new Pen(Brushes.Orange, 2), new Point(tleft, tbottom), new Point(tright, tbottom));
            g.DrawLine(new Pen(Brushes.Orange, 2), new Point(tleft, ttop), new Point(tleft, tbottom));
            g.DrawLine(new Pen(Brushes.Orange, 2), new Point(tright, ttop), new Point(tright, tbottom));
            g.DrawLine(new Pen(Brushes.Green, 1), new Point((tleft +  tright)/2, ttop), new Point((tleft + tright) / 2, tbottom));
            g.DrawLine(new Pen(Brushes.Green, 1), new Point(tleft, (ttop+tbottom)/2), new Point(tright, (ttop + tbottom) / 2));
        }
        private void CheckBounds()
        {
            if (this.myBall.location.X < myBall.Bounds.left)// impact left wall
            {
                this.myBall.location.X = myBall.Bounds.left;
                this.myBall.velocity.X = -this.myBall.velocity.X;
            }
            else if (this.myBall.location.X > myBall.Bounds.right)//impact right wall
            {
                this.myBall.location.X = myBall.Bounds.right;
                this.myBall.velocity.X = -this.myBall.velocity.X;
            }
            if (this.myBall.location.Y < myBall.Bounds.top)//impact top wall
            {
                this.myBall.location.Y = myBall.Bounds.top;
                this.myBall.velocity.Y = -this.myBall.velocity.Y;
            } 
            else if (this.myBall.location.Y > myBall.Bounds.bottom)// impact bottom wall
            {
                this.myBall.location.Y = myBall.Bounds.bottom;
                this.myBall.velocity.Y = -this.myBall.velocity.Y;
            }
                

        }
        
        // input handlers
        public override void Window_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                Application.Exit();
            else if (e.KeyChar == (char)Keys.I || e.KeyChar == (char)Keys.I + 32)
                boolShowInfo = !boolShowInfo;


        }
        public override void Window_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Input.updateMouseLocation(e.Location);
                this.Input.lButton = !this.Input.lButton;
            }
            


        }
        public override void Window_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Input.lButton = !this.Input.lButton;
                this.Input.updateMouseLocation(e.Location);
            }

        }
        public override void Window_MouseMove(object sender, MouseEventArgs e)
        {
            this.Input.updateMouseLocation(e.Location);
        }


        // add these function to class for conversion of point system
        private Vector2D CartiseanToScreen(Vector2D point)
        {
            Vector2D temp = new Vector2D();
            temp.X = this.ScreenSize.X / 2 + point.X;
            temp.Y = this.ScreenSize.Y / 2 - point.Y;
            return temp;
        }
        private Vector2D CartiseanToScreen(double x, double y)
        {
            Vector2D temp = new Vector2D();
            temp.X = this.ScreenSize.X / 2 + x;
            temp.Y = this.ScreenSize.Y / 2 - y;
            return temp;
        }

        private void ShowScreenInfo(Graphics g)
        {
            infoTextLocationCounter = 0;
            //g.DrawString($" Size of Windows = {this.ScreenSize.X}x{this.ScreenSize.Y}", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            //g.DrawString($" Shortcuts :", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            //g.DrawString($" 'Esc' : Quit Game", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            //g.DrawString($" 'F'   : Toggle Full Screen ", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            //g.DrawString($" 'I'   : Toggole Info ", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            g.DrawString($" L Mouse Down = {this.Input.lButton}", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            g.DrawString($" R Mouse Down = {this.Input.rButton}", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            g.DrawString($" Mouse Position = ({this.Input.mouseLocation.X}, {this.Input.mouseLocation.Y})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            g.DrawString($" ball Position = ({this.myBall.location.X}, {this.myBall.location.Y})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            g.DrawString($" is ball locked for throw = {myBall.IsLockedForThrow}", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            g.DrawString($" is ball released = {myBall.IsReleased}", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            g.DrawString($" Game bound (left ={myBall.Bounds.left}, right= {myBall.Bounds.right}, top={myBall.Bounds.top}, bottom={myBall.Bounds.bottom})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            g.DrawString($" poonch Location = ({myBall.Arrow.Poonchh.X},{myBall.Arrow.Poonchh.Y})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            g.DrawString($" Nock Location = ({myBall.Arrow.Nock.X},{myBall.Arrow.Nock.Y})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            g.DrawString($" Force Vector = ({myBall.forceVector.X},{myBall.forceVector.Y})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            g.DrawString($" Ball Velocity = ({myBall.velocity.X},{myBall.velocity.Y})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            //g.DrawString($" 'P'   : Play/Pause ", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);

        }
    }
}
