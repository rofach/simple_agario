using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dod
{
    public partial class Form1 : Form
    {
        static public List<Food> foods = new List<Food>();
        public List<Cell> cells = new List<Cell>();
        int maxCount = 100;
        Player player;
        
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
            for (int i = 0; i < 10; i++)
            {
                cells.Add(new Cell1(this));
            }
            player = new Player(this);
            cells.Add(player);
            DoubleBuffered = true;
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            this.Invalidate();
            foreach (Cell cell in cells)
            {
                cell.Move();
                
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.Clear(this.BackColor);

            if (Food.count < maxCount)
            {
                int temp = Food.count;
                for (int i = temp; i < maxCount; i++)
                {
                    foods.Add(new Food(this));
                }

            }
            for (int i = 0; i < cells.Count; i++) {
                var cell = cells[i];
                cell.ReduceMass();
                foreach (var food in foods) // чекаєм чи попалась їжа в радіусі з'їдання
                {
                    if (cell.GetDistanceToPoint(food.x, food.y) < cell.radius)
                    {
                        food.Respawn(this);
                        cell.IncreaseMass(food.mass);
                    }
                }
                for (int j = 0; j < cells.Count; j++) // тепер чекаєм чи попалвсь клітина в радіусі з'їдання
                {
                    var other = cells[j];

                    if (cell.GetDistanceToPoint(other.x + other.radius, other.y + other.radius) < cell.radius &&
                        cell.CompareTo(other) == 1)
                    {
                        if (/*cell is Player && other is Cell1 && */!other.isUsedPower && other is Cell1) // бот може телепортуватись
                        {
                            other.SuperPower();                          
                        }
                        else
                        {
                            cells.Remove(other);
                            if(other is Player) MessageBox.Show("не молодець, запишіть собі -1 бал");
                            if (cells.Count == 1 && cells.Contains(player))
                            {
                                MessageBox.Show("молодець, запишіть собі 1 бал");
                            }
                            i--;
                            j--;
                            cell.IncreaseMass(other.mass);
                        }
                    }
                }
                //cell.Draw(g);

            }
        
          

            foreach(var cell in cells)
            {
                cell.Draw(g);
            }

            foreach (Food food in foods)
            {
                food.Draw(g);
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if(!player.isUsedPower)
                    player.SuperPower();

            }

        }
    }
}
