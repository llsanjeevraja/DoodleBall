using GameEngine.GameEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEngine.GameEngine
{
    public class ball
    {
        
        public double mass;
        public Vector2D velocity { get; set; }
        public Vector2D acceleration { get; set; }
        public Vector2D forceVector { get; set; }
        public Vector2D location { get; set; }
        public bool IsLockedForThrow { get; set; }
        public bool IsReleased { get; set; }
        public float size;
        public bounds Bounds;
        public arrow Arrow=new arrow();
        public bool mouseDragging { get; set; }
        

        public ball()
        {
            this.mass = 100;
            this.location = new Vector2D();
            this.velocity = new Vector2D();
            this.acceleration = new Vector2D();
            this.forceVector = new Vector2D();
            this.size = 25;//set bulb size here
            this.Bounds = new bounds();
            IsLockedForThrow = false;
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
        public void IntialiseMovement()
        {

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
        public void UpdateNochLocation(Vector2D newPoint)
        {
            this.Nock.X = newPoint.X;
            this.Nock.Y = newPoint.Y;
        }
        public void Draw(Graphics g)
        {
            //g.DrawLine(new Pen(Brushes.Blue, 5), (float)this.Arrow.Poonchh.X, (float)this.Arrow.Poonchh.Y, (float)0, (float)0);
            g.DrawLine(new Pen(Brushes.Blue, this.Width), (float)this.Poonchh.X, (float)this.Poonchh.Y, (float)this.Nock.X, (float)this.Nock.Y);
        }

    }
}
