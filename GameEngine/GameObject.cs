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
    public class gameObject
    {

        public float mass { get; set; }
        public float size { get; set; }
        public Color color { get; set; }
        public Environment environmet = new Environment();
        public Vector2D velocity { get; set; }
        public double terminalVelocity { get; set; }
        public Vector2D acceleration { get; set; }
        public Vector2D forceVector { get; set; }
        public double forceValue { get; set; }
        public double velocityMultiplier { get; set; }
        public Vector2D location { get; set; }
        public bool IsLockedForThrow { get; set; }
        public bool IsReleased { get; set; }
        public arrow Arrow = new arrow();

        public gameObject()
        {
            this.mass = 100;
            this.size = 25;//set bulb size here
            this.color = Color.Aquamarine;
            this.environmet.frictionConstant = new Vector2D(0.97f,0.95f);
            this.location = new Vector2D();
            this.velocity = new Vector2D();
            this.velocityMultiplier = 0.02;
            this.acceleration = new Vector2D(0,0.3f);
            this.forceVector = new Vector2D();
            
            IsLockedForThrow = false;
            terminalVelocity = 10;
        }
        public void SetProperties(float mass,float size,Color color,bool IsReleased,Vector2D location,Vector2D velocity)
        {
            this.mass = mass;
            this.size = size;
            this.color = color;
            this.IsReleased = IsReleased;
            this.location.X = location.X;
            this.location.Y = location.Y;
            this.velocity.X = velocity.X;
            this.velocity.Y = velocity.Y;
        }
        public void Draw(Graphics g)
        {
            g.FillEllipse(new SolidBrush(this.color),(float)(this.location.X-this.size/2), (float)(this.location.Y-this.size/2),(float)this.size, (float)this.size);
            if(IsLockedForThrow)
            {
                this.Arrow.Draw(g);
            }
        }
        public void Update()
        {
            this.location.X = this.location.X + this.velocity.X;
            this.location.Y = this.location.Y + this.velocity.Y;
            if (this.environmet.boolGravityOn)
            {
                this.velocity.X = this.velocity.X + this.acceleration.X;
                this.velocity.Y = this.velocity.Y + this.acceleration.Y;
            }
        }
        
        public void UpdateLocation(Vector2D newLocation)
        {
            this.location.X = newLocation.X;
            this.location.Y = newLocation.Y;
        }
        public void UpdateForceOnBall()
        {
            this.forceVector.X = this.Arrow.Nock.X - this.Arrow.Poonchh.X;
            this.forceVector.Y = this.Arrow.Nock.Y - this.Arrow.Poonchh.Y;
            this.forceValue = this.forceVector.Magnitude();
        }
        public void IntialiseMovement()
        {
            this.forceVector.X = (float)((this.Arrow.Nock.X - this.Arrow.Poonchh.X) * this.velocityMultiplier);
            this.forceVector.Y = (float)((this.Arrow.Nock.Y - this.Arrow.Poonchh.Y) * this.velocityMultiplier);

            this.velocity.X = this.forceVector.X ;
            this.velocity.Y = this.forceVector.Y ;

        }
        public void ResetMovement()
        {
            this.velocity.X = 0;
            this.velocity.Y = 0;
            this.acceleration.X = 0;
            this.acceleration.Y = 0;
            this.IsReleased = false;
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
            this.Poonchh.X = newPoint.X;
            this.Poonchh.Y = newPoint.Y;
        }
        public void UpdatePoonchLocation(float x, float y)
        {
            this.Poonchh.X = x;
            this.Poonchh.Y = y;
        }
        public void UpdateNochLocation(Vector2D newPoint)
        {
            this.Nock.X = newPoint.X;
            this.Nock.Y = newPoint.Y;
        }
        public void UpdateNochLocation(float x, float y)
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
