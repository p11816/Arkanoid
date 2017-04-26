using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManyWindows.Shapes;
using System.Drawing;

namespace ManyWindows
{
    class Maps
    {
        List<Shape> shapesColl = new List<Shape>();
        SolidBrush[] colors = new SolidBrush[7];
        
        public Maps()
        {
            colors[0] = new SolidBrush(Color.Red);
            colors[1] = new SolidBrush(Color.Orange);
            colors[2] = new SolidBrush(Color.Yellow);
            colors[3] = new SolidBrush(Color.Green);
            colors[4] = new SolidBrush(Color.LightBlue);
            colors[5] = new SolidBrush(Color.Blue);
            colors[6] = new SolidBrush(Color.Purple);
        }

        // первая карта
       public List<Shape> firstMap()
        {
            
            shapesColl.Clear();
            int nObjects = 0;
            int nString = 0;
            for (int j = 80; j < 234; j += 22)
            {
                for (int i = 2; i < 1042; i += 52)
                {

                    shapesColl.Add(new RectangleMy(i, j, 50, 20));
                    shapesColl[nObjects].brush = colors[nString];
                    nObjects++;
                }
                nString++;
            }
            return shapesColl;
        }
    }
}
