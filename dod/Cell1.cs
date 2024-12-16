using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Windows.Forms;
using System.Diagnostics;

namespace dod
{
    class Cell1 : Cell
    {
        bool foundTarget = false;
        Cell enemy;
        public Cell1(Form1 form) : base(form)
        {
            targetPoint = GeneratePoint();
        }

        private Point GeneratePoint()
        {
            int x2 = rand.Next(0, form.ClientSize.Width);
            int y2 = rand.Next(0, form.ClientSize.Height);
            return new Point(x2, y2);
        }
        async public override void SuperPower()
        {
            bool flag = rand.Next(-1, 1) != 0;
            if (!isUsedPower && flag)
            {
                x = rand.Next(0, form.ClientSize.Width);
                y = rand.Next(0, form.ClientSize.Height);
                
            }
            isUsedPower = true;
            await Task.Delay(20000);
            isUsedPower = false;
        }

        private bool ReachedPoint()
        {
            return x > targetPoint.X - 5 && x < targetPoint.X + 5 && y > targetPoint.Y - 5 && y < targetPoint.Y + 5;
        }
        private void FindEnemy()
        {
            foreach(var enemy in form.cells)
            {
                if(GetDistanceToPoint(enemy.x + enemy.radius, enemy.y + enemy.radius) < 120)
                {
                    if (enemy.mass < mass - 100 && !foundTarget)
                        targetPoint = new Point((int)(enemy.x), (int)(enemy.y));
                    
                }
            }
        }
        public override void Move()
        {
            FindEnemy();
            if (ReachedPoint())
            {
                getPoint = true;
                //foundTarget = false;
            }
            if (!getPoint)
            {
                double dx = targetPoint.X - x;
                double dy = targetPoint.Y - y;
                double distance = Math.Sqrt(dx * dx + dy * dy);

   
                if (distance > 0)
                {
                    double directionX = dx / distance;
                    double directionY = dy / distance;

                    x += directionX * speed;
                    y += directionY * speed; ;

                    if (speed >= distance)
                    {
                        x = targetPoint.X - radius;
                        y = targetPoint.Y - radius;
                    }
                }

            }
            else
            {
                targetPoint = GeneratePoint();
                getPoint = false;
            }

        }
    }
}
