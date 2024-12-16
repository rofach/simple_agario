using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dod
{
    public class Food
    {
        private double mass;
        private Brush color;
        private static Random rand = new Random();
        private static Brush[] colors = { Brushes.Blue, Brushes.Red, Brushes.Orange, Brushes.Green };
        
        public static int count = 0;
        public int x, y;
        public int radius = 5;
        public Food(Form form)
        {
            //this.form = form;
            mass = rand.Next(1, 5);
            color = colors[rand.Next(0, colors.Length)];
            x = rand.Next(0, form.ClientSize.Width);
            y = rand.Next(0, form.ClientSize.Height);
            count++;
           
        }
        public void Respawn(Form form)
        {
            x = rand.Next(0, form.ClientSize.Width);
            y = rand.Next(0, form.ClientSize.Height);
        }

        public void Draw(Graphics g)
        {
            g.FillEllipse(color, x, y, 10, 10);
        }
    }
}
