using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameEngine
{
    public class Stage
    {
        public StageSounds Sounds = new StageSounds();
        Vector2D ScreenSize=new Vector2D(0,0);
        public StageBounds Bounds = new StageBounds();
        
        private int infoTextSize=12;
        public myGameInput Input = new myGameInput();
        public gameObject myBall = new gameObject();
        public Stage()
        {

        }
        public void OnLoad(Vector2D ScreenSize)
        {
            myBall.location = CartiseanToScreen(0, 0);
            this.ScreenSize.X = ScreenSize.X;
            this.ScreenSize.Y = ScreenSize.Y;
        }
        public void OnUpdate()
        {
            myBall.location.X = myBall.location.X + myBall.velocity.X;
            myBall.location.Y = myBall.location.Y + myBall.velocity.Y;
            myBall.velocity.X = myBall.velocity.X + myBall.acceleration.X;
            myBall.velocity.Y = myBall.velocity.Y + myBall.acceleration.Y;
            if (!myBall.IsReleased)
                myBall.UpdateLocation(Input.mouseLocation);

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
            CheckBounds(myBall);
        }
        public void Reset()
        {
            myBall.ResetMovement();
        }
        private Vector2D CartiseanToScreen(Vector2D point)
        {
            Vector2D temp = new Vector2D();
            temp.X = ScreenSize.X / 2 + point.X;
            temp.Y = ScreenSize.Y / 2 - point.Y;
            return temp;
        }

        private Vector2D CartiseanToScreen(double x, double y)
        {
            Vector2D temp = new Vector2D();
            temp.X = (float)(ScreenSize.X / 2 + x);
            temp.Y = (float)(ScreenSize.Y / 2 - y);
            return temp;
        }
        public void UpdateScreenSize(Vector2D ScreenSize)
        {
            this.ScreenSize.X = ScreenSize.X;
            this.ScreenSize.Y = ScreenSize.Y;
        }
        public void Draw(Graphics g)
        {
            g.Clear(Color.Black);
            myBall.Draw(g);
            DrawBoundFrame(g);

        }

        private void CheckBounds(gameObject CurrentBall)
        {
            if (CurrentBall.location.X <= this.Bounds.left)// impact left wall
            {
                CurrentBall.location.X = this.Bounds.left;
                CurrentBall.velocity.X = -CurrentBall.frictionConstant.X * CurrentBall.velocity.X;
                this.Sounds.GlassTing.Play();
            }
            else if (CurrentBall.location.X >= this.Bounds.right)//impact right wall
            {
                CurrentBall.location.X = this.Bounds.right;
                CurrentBall.velocity.X = -CurrentBall.frictionConstant.X * CurrentBall.velocity.X;
                this.Sounds.GlassTing.Play();
            }
            if (CurrentBall.location.Y <= this.Bounds.top)//impact top wall
            {
                CurrentBall.location.Y = this.Bounds.top;
                CurrentBall.velocity.Y = -CurrentBall.frictionConstant.Y * CurrentBall.velocity.Y;
                this.Sounds.GlassTing.Play();
            }
            else if (CurrentBall.location.Y >= this.Bounds.bottom)// impact bottom wall
            {
                CurrentBall.location.Y = this.Bounds.bottom;
                CurrentBall.velocity.Y = -CurrentBall.frictionConstant.Y * CurrentBall.velocity.Y;

                this.Sounds.GlassTing.Play();
            }
        }
        private void DrawBoundFrame(Graphics g)
        {
            int tleft = (int)Bounds.left - (int)myBall.size / 2;
            int ttop = (int)Bounds.top - (int)myBall.size / 2;
            int tright = (int)Bounds.right + (int)myBall.size / 2;
            int tbottom = (int)Bounds.bottom + (int)myBall.size / 2;

            g.DrawLine(new Pen(Brushes.Orange, 2), new Point(tleft, ttop), new Point(tright, ttop));
            g.DrawLine(new Pen(Brushes.Orange, 2), new Point(tleft, tbottom), new Point(tright, tbottom));
            g.DrawLine(new Pen(Brushes.Orange, 2), new Point(tleft, ttop), new Point(tleft, tbottom));
            g.DrawLine(new Pen(Brushes.Orange, 2), new Point(tright, ttop), new Point(tright, tbottom));
            g.DrawLine(new Pen(Brushes.Green, 1), new Point((tleft + tright) / 2, ttop), new Point((tleft + tright) / 2, tbottom));
            g.DrawLine(new Pen(Brushes.Green, 1), new Point(tleft, (ttop + tbottom) / 2), new Point(tright, (ttop + tbottom) / 2));
        }

        public void ShowInfo(Graphics g, int infoTextLocationCounter)
        {
            g.DrawString($" L Mouse Down = {this.Input.lButton}", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            g.DrawString($" R Mouse Down = {this.Input.rButton}", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            g.DrawString($" Mouse Position = ({this.Input.mouseLocation.X}, {this.Input.mouseLocation.Y})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);

            g.DrawString($" ball Position = ({this.myBall.location.X}, {this.myBall.location.Y})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            g.DrawString($" is ball locked for throw = {myBall.IsLockedForThrow}", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            g.DrawString($" is ball released = {myBall.IsReleased}", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            g.DrawString($" Game bound (left ={Bounds.left}, right= {Bounds.right}, top={Bounds.top}, bottom={Bounds.bottom})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            g.DrawString($" poonch Location = ({myBall.Arrow.Poonchh.X},{myBall.Arrow.Poonchh.Y})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            g.DrawString($" Nock Location = ({myBall.Arrow.Nock.X},{myBall.Arrow.Nock.Y})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            g.DrawString($" ForceValue = ({myBall.forceValue})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            g.DrawString($" Velocity = ({myBall.velocity.X},{myBall.velocity.Y})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);
            g.DrawString($" velocity Value  = ({myBall.velocity.Magnitude()})", new Font(FontFamily.GenericSerif, this.infoTextSize), Brushes.White, 50.0f, 100.0f + 2.0f * this.infoTextSize * infoTextLocationCounter++);

        }
    }
}
