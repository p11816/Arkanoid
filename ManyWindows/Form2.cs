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

namespace ManyWindows
{
    public partial class Form1 : Form
    {

        List<Shape> shapes = new List<Shape>();
        SolidBrush[] colors = new SolidBrush[7];

        public Form1()
        {
            // заполним цвета
            colors[0] = new SolidBrush(Color.Red);
            colors[1] = new SolidBrush(Color.Orange);
            colors[2] = new SolidBrush(Color.Yellow);
            colors[3] = new SolidBrush(Color.Green);
            colors[4] = new SolidBrush(Color.LightBlue);
            colors[5] = new SolidBrush(Color.Blue);
            colors[6] = new SolidBrush(Color.Purple);

            InitializeComponent();
            shapes.Add(new RectangleMy(500, 500, 100, 20));
            shapes.Add(new Circle(400, 400, 10));

            int nObjects = 0;
            int nString = 0;
            for (int j = 0; j < 154; j += 22)
            {
                nString++;
                for (int i = 2; i < 1042; i += 52)
                {
                   
                    shapes.Add(new RectangleMy(i, j, 50, 20));
                    shapes[nObjects].brush = colors[nString];
                    nObjects++;
                }
            }

            

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {


            // GDI+

            // DirectDraw, OpenGL, Vulcan

            // game engines: ioquake, unity, SDL...

            // создаём холст - canvas
            Graphics g = this.CreateGraphics();
            g.Clear(SystemColors.Control);

            // рисуем все фигуры
            foreach (Shape s in this.shapes)
            {
                s.Paint(g);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Form1_Paint(sender, null);
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
    }
}
