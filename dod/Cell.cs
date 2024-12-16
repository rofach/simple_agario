using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dod
{
    abstract public class Cell : IComparable<Cell>
    {
        
        static int minMass = 1000;
        public double mass;
        public double radius;
        public double x, y;
        public Brush color;
        private static Brush[] colors = { Brushes.Blue, Brushes.Red, Brushes.Orange, Brushes.Green };
        protected static Random rand = new Random();
        protected Point targetPoint;
        protected Form1 form;
        protected double speed;
        public bool isUsedPower = false;

        protected bool getPoint = false;
        protected bool foundEnemy = false;
        public Cell(Form1 form)
        {
            mass = minMass;
            radius = CalculateRadius();
            this.form = form;
            x = rand.Next(0, form.ClientSize.Width);
            y = rand.Next(0, form.ClientSize.Height);
            color = colors[rand.Next(0, colors.Length)];
            speed = 1.3;
        }
        
        double CalculateRadius()
        {
            return Math.Sqrt(mass);   
        }
        double CalculateSpeed()
        {
            return 1.3 - (mass - minMass) / 30000.0; 
        }
        bool CheckAccelerate()
        {
            bool isUse = false;
            if (this is Player obj)
            {               
                isUse = obj.isNowUsingPower;
            }
            return isUse;
        }

        public void ReduceMass()
        {
            if (mass > minMass)
                mass -= mass * 0.0002;
            if (!CheckAccelerate())
                speed = CalculateSpeed();
            
                        
        }

        public void IncreaseMass(double mass)
        {
            this.mass += mass;
            if (!CheckAccelerate())
                speed = CalculateSpeed();
        }

       

        
        abstract public void Move();
        

        public void Draw(Graphics g)
        {
            radius = CalculateRadius();
            g.FillEllipse(color, (int)x, (int)y, (int)radius*2, (int)radius*2);
        }

        public double GetDistanceToPoint(double x, double y)
        {
            return Math.Sqrt(Math.Pow((x - this.x - radius), 2) + Math.Pow((y - this.y - radius), 2));
        }
        public abstract void SuperPower();

        public int CompareTo(Cell other)
        {
            if (mass > other.mass + 100) 
                return 1;
            if (mass < other.mass - 100) 
                return -1;
            else
                return 0;
            
        }
    }
}
