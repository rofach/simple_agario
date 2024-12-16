using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace dod
{
    class Player : Cell
    {
        public bool isUsedPower = false;
        public bool isNowUsingPower = false;
        public Player(Form1 form) : base(form)
        {
            color = Brushes.DarkBlue;
        }

        public override void Move()
        {
            Point pos = Cursor.Position;
            Point cursorPosition = form.PointToClient(pos);

            double dx = cursorPosition.X - x - (int)radius;
            double dy = cursorPosition.Y - y - (int)radius;

            double distance = Math.Sqrt(dx * dx + dy * dy);

            if (distance > 0)
            {
                double directionX = dx / distance; 
                double directionY = dy / distance; 

                x += directionX * speed;
                y += directionY * speed; 

                if (speed >= distance)
                {
                    x = cursorPosition.X - radius;
                    y = cursorPosition.Y - radius;
                }
            }
        }

            


        public async override void SuperPower()
        {   if (!isUsedPower)
            {
                speed = 3;
                isNowUsingPower = true;
                await Task.Delay(3000);
                isNowUsingPower = false;
                speed = 1;
                isUsedPower = true;
                
            }
            await Task.Delay(10000);
            isUsedPower = false;
        }
    }
}
