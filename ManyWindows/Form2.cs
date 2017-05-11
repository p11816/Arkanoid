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
        Image images = Image.FromFile("../../ImageStage1.png");
        int xStep = 0;
        int yStep = -8;

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
        }

 
        private void Move()
        {

            //////////////////
                shapes[3].point.Y += yStep;
                shapes[3].point.X += xStep;

            this.Invalidate();
        }

        // попадаем ли в биту?
        private void ShootByte()
        {
       
            // так же нужно определить в какое место биты мы попали
            //////////////////////
            // здесь попадаем в центр
            if (shapes[3].point.Y + ((Circle)shapes[3]).radius + 8 > shapes[0].point.Y  // по Y
                    && shapes[3].point.X + ((Circle)shapes[3]).radius > (shapes[0].point.X + 40)
                    && shapes[3].point.X + ((Circle)shapes[3]).radius < shapes[0].point.X + ((RectangleMy)shapes[0]).width - 40)
            {
                // тоесть движемся прямо вверх(на 8 пикселей)
                xStep = 0;
                yStep = -8;
            }
            else if (shapes[3].point.Y + ((Circle)shapes[3]).radius + 8 > shapes[0].point.Y  // по Y
                   && shapes[3].point.X + ((Circle)shapes[3]).radius > (shapes[0].point.X + 60)
                   && shapes[3].point.X + ((Circle)shapes[3]).radius < shapes[0].point.X + ((RectangleMy)shapes[0]).width - 20)
            {
                // тоесть движемся вверх вправо по 60 градусов
                xStep = 4;
                yStep = -7;
            }
            else if (shapes[3].point.Y + ((Circle)shapes[3]).radius + 8 > shapes[0].point.Y  // по Y
                   && shapes[3].point.X + ((Circle)shapes[3]).radius > (shapes[0].point.X + 80)
                   && shapes[3].point.X + ((Circle)shapes[3]).radius < shapes[0].point.X + ((RectangleMy)shapes[0]).width)
            {
                // тоесть движемся прямо вправо под 45 градусов
                xStep = 6;
                yStep = -6;
            }
            else if (shapes[3].point.Y + ((Circle)shapes[3]).radius + 8 > shapes[0].point.Y  // по Y
                   && shapes[3].point.X + ((Circle)shapes[3]).radius > (shapes[0].point.X + 20)
                   && shapes[3].point.X + ((Circle)shapes[3]).radius < shapes[0].point.X + ((RectangleMy)shapes[0]).width - 60)
            {
                // тоесть движемся вверх влево под 60 градусов
                xStep = -4;
                yStep = -7;
            }
            else if (shapes[3].point.Y + ((Circle)shapes[3]).radius + 8 > shapes[0].point.Y  // по Y
                   && shapes[3].point.X + ((Circle)shapes[3]).radius > (shapes[0].point.X)
                   && shapes[3].point.X + ((Circle)shapes[3]).radius < shapes[0].point.X + ((RectangleMy)shapes[0]).width - 80)
            {
                // тоесть движемся вверх влево под 45 градусов
                xStep = -6;
                yStep = -6;
            }
        }

        // функция для отскока от стенок
        private void RebounWalls()
        {
            // у нас три стены 
            // левая
            if (shapes[3].point.X < 0 && xStep == -4 && yStep == -7)    // тоесть летит снизу под углом 60
            {
                // меняем траекторию
                xStep = 4;
                yStep = -7;
            }
            else if (shapes[3].point.X < 0 && xStep == -6 && yStep == -6)    // тоесть летит снизу под углом 45
            {
                // меняем траекторию
                xStep = 6;
                yStep = -6;
            }
            else if (shapes[3].point.X < 0 && xStep == -4 && yStep == 7)    // тоесть летит сверху под углом 30
            {
                // меняем траекторию
                xStep = 4;
                yStep = 7;
            }
            else if (shapes[3].point.X < 0 && xStep == -6 && yStep == 6)    // тоесть летит сверху под углом 45
            {
                // меняем траекторию
                xStep = 6;
                yStep = 6;
            }
            ////////////////////////////////////////////////////////////////////////////////
            // правая стена
            else if (shapes[3].point.X + ((Circle)shapes[3]).radius * 2 > this.ClientSize.Width  && xStep == 6 && yStep == -6)    // тоесть летит снизу под углом 45
            {
                // меняем траекторию
                xStep = -6;
                yStep = -6;
            }
            else if (shapes[3].point.X + ((Circle)shapes[3]).radius * 2 > this.ClientSize.Width && xStep == 4 && yStep == -7)    // тоесть летит снизу под углом 60
            {
                // меняем траекторию
                xStep = -4;
                yStep = -7;
            }
            else if (shapes[3].point.X + ((Circle)shapes[3]).radius * 2 > this.ClientSize.Width && xStep == 4 && yStep == 7)    // тоесть летит сверху под углом 30
            {
                // меняем траекторию
                xStep = -4;
                yStep = 7;
            }
            else if (shapes[3].point.X + ((Circle)shapes[3]).radius * 2 > this.ClientSize.Width && xStep == 6 && yStep == 6)    // тоесть летит сверху под углом 45
            {
                // меняем траекторию
                xStep = -6;
                yStep = 6;
            }
            ////////////////////////////////////////////////////////////////////////////////
            // теперь отскок от верхней стены
            else if (shapes[3].point.Y < 0 && xStep == 0 && yStep == -8)    // тоесть прямой отскок
            {
                // меняем траекторию
                xStep = 0;
                yStep = 8;
            }
            else if (shapes[3].point.Y < 0 && xStep == 4 && yStep == -7)    // тоесть отскок в 60 градусов в право
            {
                // меняем траекторию
                xStep = 4;
                yStep = 7;
            }
            else if (shapes[3].point.Y < 0 && xStep == -4 && yStep == -7)    // тоесть отскок в 60 градусов в влево
            {
                // меняем траекторию
                xStep = -4;
                yStep = 7;
            }
            else if (shapes[3].point.Y < 0 && xStep == 6 && yStep == -6)    // тоесть отскок в 45 градусов в вправо
            {
                // меняем траекторию
                xStep = 6;
                yStep = 6;
            }
            else if (shapes[3].point.Y < 0 && xStep == -6 && yStep == -6)    // тоесть отскок в 45 градусов в лево
            {
                // меняем траекторию
                xStep = -6;
                yStep = 6;
            }
        }


        // передвижение шарика
        private void NewMoveShare()
        {

            // 2 что нужно определить это то что мы попали в ракетку, а дальше в какое место ракетки мы попали
            // от этого будет зависить угол передвижения мячика
            if (shapes[3].point.Y > this.ClientSize.Height)
            {
                timer1.Enabled = false;
                MessageBox.Show("Game Over");
                if (MessageBox.Show("Continue the game?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    shapes[3].point = new PointF(500, 400);
                    xStep = 0;
                    yStep = -8;
                }
                else
                {
                    this.Close();
                }
                

            }
            ShootByte();        // функция отскока от ракетки
            RebounWalls();      // отскоко от стен
            Move();             // передвижени по назначенной траектории
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
            timer1.Enabled = true;
            foreach (Shape s in this.shapes)
            {
                if (s.isInside(e.X, e.Y))
                {
                    this.Text = "Выбран элемент №" + s.Id;
                }

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // движение шарика
            NewMoveShare();

            // движение биты
            if (MousePosition.X - this.Location.X - 7 < 60)
            {
                shapes[0].point.X = 10;
                shapes[1].point.X = 0;
                shapes[2].point.X = 100;
            }
            else if (MousePosition.X - this.Location.X - 7 > this.ClientSize.Width - 60)
            {
                shapes[0].point.X = this.ClientSize.Width - 110;
                shapes[1].point.X = this.ClientSize.Width - 120;
                shapes[2].point.X = this.ClientSize.Width - 20;
            }
            else
            {
                //Region r = new Region(new Rectangle(0, 0, 1000, 500));
                shapes[0].point.X = MousePosition.X - this.Location.X - 7 - 50;
                shapes[1].point.X = MousePosition.X - this.Location.X - 7 - 60;
                shapes[2].point.X = MousePosition.X - this.Location.X - 7 + 40;
                // Graphics g = this.CreateGraphics();
                // shapes[0].Paint(g);
                // this.Update();
            }
            this.Invalidate();
        }
    }
}
