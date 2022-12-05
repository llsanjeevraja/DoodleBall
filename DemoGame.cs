﻿using System;
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
        public ball myBall = new ball();
        public 
        myGameInput Input=new myGameInput();
        public DemoGame() : base(new GameEngine.Vector2D(1000,800), "Doodle Ball")
        {
            this.myBall.UpdateLocation(CartiseanToScreen(0,0));
        }
        // class functions//methods
        //game loop functions
        public override void OnLoad()
        {
            //called only once.
            myBall.location = CartiseanToScreen(0,0);
            myBall.Bounds.Set(this.ScreenSize);
        }
        public override void OnUpdate(double deltatime)
        {
            this.deltaTime = deltatime;
            //called each time in loop after draw
            if (myBall.IsReleased)
            {
                myBall.location.X = myBall.location.X + myBall.velocity.X;
                myBall.location.Y = myBall.location.Y + myBall.velocity.Y;
                if (myBall.Environment.HaveGravity)
                {
                    myBall.velocity.X = myBall.velocity.X + myBall.acceleration.X;
                    myBall.velocity.Y = myBall.velocity.Y + myBall.acceleration.Y;
                }
                
            }
            
            if (!myBall.IsReleased)
                myBall.UpdateLocation(Input.mouseLocation);
            this.CheckBounds();
            if (!myBall.IsLockedForThrow && !myBall.IsReleased && Input.lButton)// ball is loaded for play
            {
                myBall.Arrow.UpdateNochLocation(myBall.location);
                myBall.Arrow.UpdatePoonchLocation(myBall.location);
                myBall.IsLockedForThrow = true;
            }
            else if (myBall.IsLockedForThrow && !myBall.IsReleased && Input.lButton)// ball is captured and being dragged for throw
            {
                myBall.Arrow.UpdatePoonchLocation(myBall.location);
                myBall.UpdateForceOnBall();
 
            }
            else if (myBall.IsLockedForThrow && !myBall.IsReleased && !Input.lButton)// means that ball has been throw away
            {
                myBall.IsLockedForThrow = false;
                myBall.IsReleased = true;
                myBall.IntialiseMovement();

            }
            frame++;
        }
        

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
            if (this.myBall.location.X <= myBall.Bounds.left)// impact left wall
            {
                this.myBall.location.X = myBall.Bounds.left;
                this.myBall.velocity.X = -myBall.Environment.frictionConstant.X * this.myBall.velocity.X;
                myBall.Sounds.ImpactSound.Play();
            }
            else if (this.myBall.location.X >= myBall.Bounds.right)//impact right wall
            {
                this.myBall.location.X = myBall.Bounds.right;
                this.myBall.velocity.X = -myBall.Environment.frictionConstant.X * this.myBall.velocity.X;
                myBall.Sounds.ImpactSound.Play();
            }
            if (this.myBall.location.Y <= myBall.Bounds.top)//impact top wall
            {
                this.myBall.location.Y = myBall.Bounds.top;
                this.myBall.velocity.Y = -myBall.Environment.frictionConstant.Y * this.myBall.velocity.Y;
                myBall.Sounds.ImpactSound.Play();
            } 
            else if (this.myBall.location.Y >= myBall.Bounds.bottom)// impact bottom wall
            {
                this.myBall.location.Y = myBall.Bounds.bottom;
                this.myBall.velocity.Y = -this.myBall.Environment.frictionConstant.Y*this.myBall.velocity.Y;
                
                myBall.Sounds.ImpactSound.Play();
            }
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
                myBall.Reset();
            else if (e.KeyChar == (char)Keys.G || e.KeyChar == (char)Keys.G + 32)
                myBall.Environment.HaveGravity = !myBall.Environment.HaveGravity;
            
        }
        public override void Window_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            
            
        }
        public override void Window_KeyDown(object sender, KeyEventArgs e)
        {

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
        public override void Window_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            this.ScreenSize.X = control.Width;
            this.ScreenSize.Y = control.Height;
            this.myBall.Bounds.Set(ScreenSize);
        }

        // add these function to class for conversion of point system;
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
            int infoTextLocationCounter = 0;
            //g.DrawString($" Size of Windows = {this.ScreenSize.X}x{this.ScreenSize.Y}", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            g.DrawString($" Frame Rate = {(1000 / deltaTime).ToString("0.0")}", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            //g.DrawString($" Shortcuts :", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            //g.DrawString($" 'Esc' : Quit Game", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            //g.DrawString($" 'F'   : Toggle Full Screen ", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            //g.DrawString($" 'I'   : Toggole Info ", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);
            g.DrawString($" L Mouse Down = {this.Input.lButton}", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            //g.DrawString($" R Mouse Down = {this.Input.rButton}", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            //g.DrawString($" Mouse Position = ({this.Input.mouseLocation.X}, {this.Input.mouseLocation.Y})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            //g.DrawString($" ball Position = ({this.myBall.location.X}, {this.myBall.location.Y})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            g.DrawString($" is ball locked for throw = {myBall.IsLockedForThrow}", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            g.DrawString($" is ball released = {myBall.IsReleased}", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            //g.DrawString($" Game bound (left ={myBall.Bounds.left}, right= {myBall.Bounds.right}, top={myBall.Bounds.top}, bottom={myBall.Bounds.bottom})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            g.DrawString($" poonch Location = ({(myBall.Arrow.Poonchh.X).ToString("0.00")},{(myBall.Arrow.Poonchh.Y).ToString("0.00")})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            g.DrawString($" Nock Location = ({(myBall.Arrow.Nock.X).ToString("0.00")},{(myBall.Arrow.Nock.Y).ToString("0.00")})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            g.DrawString($" ForceValue = ({(myBall.forceValue).ToString("0.00")})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            g.DrawString($" Velocity = ({(myBall.velocity.X).ToString("0.00")},{(myBall.velocity.Y).ToString("0.00")})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            g.DrawString($" velocity Value  = ({(myBall.velocity.Magnitude()).ToString("0.00")})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            //g.DrawString($" 'P'   : Play/Pause ", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * this.infoTextLocationCounter++);

        }
    }
}