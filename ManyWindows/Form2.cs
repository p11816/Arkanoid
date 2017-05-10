using ManyWindows.Shapes;
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
using System.Timers;

namespace ManyWindows
{
    public partial class Form1 : Form
    {

        List<Shape> shapes = new List<Shape>();
        Image images = Image.FromFile("ImageStage1.png");
        bool path = true;

        public Form1()
        {
            // включили какое то свойство буффиризации
            this.DoubleBuffered = true;
            InitializeComponent();
            // бита
            shapes.Add(new RectangleMy(500, 650, 100, 20));
            // создадим два круга для биты
            shapes.Add(new Circle(490, 650, 10));
            shapes.Add(new Circle(590, 650, 10));
            // шарик
            shapes.Add(new Circle(500, 400, 10));
            // бита, мячик, цвета
            shapes[0].brush = new SolidBrush(Color.Blue);
            shapes[1].brush = new SolidBrush(Color.Red);
            shapes[2].brush = new SolidBrush(Color.Red);
            shapes[3].brush = new SolidBrush(Color.Black);
            Maps m = new Maps();
            shapes.AddRange(m.firstMap());
            // вставили фон на форме
            this.BackgroundImage = images;
            moveBall();
            
        }

        
        // перемещение шарика
        public void moveBall()
        {
            // устанавливаем метод обратного вызова
            TimerCallback tm = new TimerCallback(Move);
            // создаем таймер
            System.Threading.Timer timer = new System.Threading.Timer(tm, 0, 0, 15);
           
        }

    
        private void Move(object obj)
        {
            if (path)
            {
                shapes[3].point.Y += 8;
                if (shapes[3].point.Y >= this.ClientSize.Height)
                {
                    path = false;
                }
            }
            else
            {
                shapes[3].point.Y -= 8;
                if (shapes[3].point.Y <= 0)
                {
                    path = true;
                }

            }
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {


            // GDI+

            // DirectDraw, OpenGL, Vulcan

            // game engines: ioquake, unity, SDL...

            // создаём холст - canvas
            Graphics g = e.Graphics;            // сдесь получаем обьект через союытие(по другому не работает)
            //this.Invalidate();
           
            // рисуем все фигуры
            foreach (Shape s in this.shapes)
            {
                s.Paint(g);
            }
            
        }
        
        private void Form1_Resize(object sender, EventArgs e)
        {
                //this.Form1_Paint(sender, null);               // если делать перерисовка в ресайз - сваливается
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

            foreach (Shape s in this.shapes)
            {
                if (s.isInside(e.X, e.Y))
                {
                    this.Text = "Выбран элемент №" + s.Id;
                }

            }
        }

           // движение биты
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (e.X < 60)
            {
                shapes[0].point.X = 10;
                shapes[1].point.X = 0;
                shapes[2].point.X = 100;
            }
            else if (e.X > this.ClientSize.Width - 60)
            {
                shapes[0].point.X = this.ClientSize.Width - 110;
                shapes[1].point.X = this.ClientSize.Width - 120;
                shapes[2].point.X = this.ClientSize.Width - 20;
            }
            else
            {
                //Region r = new Region(new Rectangle(0, 0, 1000, 500));
                shapes[0].point.X = e.X - 50;
                shapes[1].point.X = e.X - 60;
                shapes[2].point.X = e.X + 40;
               // Graphics g = this.CreateGraphics();
               // shapes[0].Paint(g);
               // this.Update();
            }
            this.Invalidate();
        }
    }
}
