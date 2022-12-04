using GameEngine.GameEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bulb
{
    public class Bulb
    {
        public Vector2D location { get; set; }
        public bool Islocked { get; set; }
        public float size;
        public playbound Bounds;
        public arrow Arrow=new arrow();
        public bool mouseDragging { get; set; }
        

        public Bulb()
        {
            this.location = new Vector2D();
            this.size = 25;//set bulb size here
            this.Bounds = new playbound();
            Islocked = false;
        }
        public void Show(Graphics g)
        {
            g.FillEllipse(Brushes.Red, (float)this.location.X-this.size/2, (float)this.location.Y-this.size/2,this.size,this.size);
            
        }
        public void DrawArrow(Graphics g)
        {
            //g.DrawLine(new Pen(Brushes.Blue, 5), (float)this.Arrow.Poonchh.X, (float)this.Arrow.Poonchh.Y, (float)0, (float)0);
            g.DrawLine(new Pen(Brushes.Blue, this.Arrow.Width), (float)this.Arrow.Poonchh.X, (float)this.Arrow.Poonchh.Y, (float)this.Arrow.Nock.X, (float)this.Arrow.Nock.Y);
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
        public void UpdatePoonchLocation(Point newPoint)
        {
            this.Poonchh.X=newPoint.X;
            this.Poonchh.Y = newPoint.Y;
        }
        public void UpdateNochLocation(Point newPoint)
        {
            this.Nock.X = newPoint.X;
            this.Nock.Y = newPoint.Y;
        }


    }
}
