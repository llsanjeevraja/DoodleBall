using GameEngine.GameEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace GameEngine.GameEngine
{
    public class ball
    {
        
        public double mass;
        public Vector2D frictionConstant ;
        public Vector2D velocity { get; set; }
        public double terminalVelocity { get; set; }
        public Vector2D acceleration { get; set; }
        public Vector2D forceVector { get; set; }
        public double forceValue { get; set; }
        public double velocityMultiplier { get; set; }
        public Vector2D location { get; set; }
        public float size;
        public bounds Bounds;
        public arrow Arrow=new arrow();
        public GameSounds Sounds=null;

        public ball()
        {
            this.mass = 100;
            this.size = 25;//set bulb size here
            this.frictionConstant = new Vector2D(0.90,0.85);
            this.location = new Vector2D();
            this.velocity = new Vector2D();
            this.velocityMultiplier = 0.015;
            this.acceleration = new Vector2D(0,0.5);
            this.forceVector = new Vector2D();
            this.Bounds = new bounds();
            this.Sounds = new GameSounds();
            terminalVelocity = 10;
        }
        public void Draw(Graphics g)
        {
            g.FillEllipse(Brushes.Red, (float)this.location.X-this.size/2, (float)this.location.Y-this.size/2,this.size,this.size);
        }
        
        public void UpdateLocation(Vector2D newLocation)
        {
            this.location.X = newLocation.X;
            this.location.Y = newLocation.Y;
        }
        public void UpdateLocation(double x,double y)
        {
            this.location.X = x;
            this.location.Y = y;
        }
    public void UpdateForceOnBall()
        {
            this.forceVector.X = this.Arrow.Nock.X - this.Arrow.Poonchh.X;
            this.forceVector.Y = this.Arrow.Nock.Y - this.Arrow.Poonchh.Y;
            this.forceValue = this.forceVector.Magnitude();
        }
        public void IntialiseMovement()
        {
            this.forceVector.X = (this.Arrow.Nock.X - this.Arrow.Poonchh.X) * this.velocityMultiplier;
            this.forceVector.Y = (this.Arrow.Nock.Y - this.Arrow.Poonchh.Y) * this.velocityMultiplier;

            this.velocity.X = this.forceVector.X ;
            this.velocity.Y = this.forceVector.Y ;

        }
        public void Reset()
        {
            this.velocity.X = 0;
            this.velocity.Y = 0;
        }
    }

    public class arrow
    {
        public Vector2D Nock {get; set; }
        public Vector2D Poonchh {get; set; }
        public int Width = 2;
        public Vector2D GetVector()
        {
            Vector2D temp = new Vector2D();
            temp.X = Nock.X - Poonchh.X;
            temp.Y=Nock.Y - Poonchh.Y;
            return temp;
        }
        public arrow()
        {
            this.Nock = new Vector2D();
            this.Poonchh = new Vector2D();
        }
        public void UpdatePoonchLocation(Vector2D newPoint)
        {
            this.Poonchh.X=newPoint.X;
            this.Poonchh.Y = newPoint.Y;
        }
        public void UpdatePoonchLocation(double x,double y)
        {
            this.Poonchh.X = x;
            this.Poonchh.Y = y;
        }
        public void UpdateNochLocation(Vector2D newPoint)
        {
            this.Nock.X = newPoint.X;
            this.Nock.Y = newPoint.Y;
        }
        public void UpdateNochLocation(double x, double y)
        {
            this.Nock.X = x;
            this.Nock.Y = y;
        }
            public void Draw(Graphics g)
        {
            //g.DrawLine(new Pen(Brushes.Blue, 5), (float)this.Arrow.Poonchh.X, (float)this.Arrow.Poonchh.Y, (float)0, (float)0);
            g.DrawLine(new Pen(Brushes.Blue, this.Width), (float)this.Poonchh.X, (float)this.Poonchh.Y, (float)this.Nock.X, (float)this.Nock.Y);
        }

    }
}
