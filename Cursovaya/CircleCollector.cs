using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursovaya
{
    class CircleCollector:Circle
    {
        public int countHit;
        public CircleCollector(int mouseX, int mouseY)
        {
            countHit = 0;

            X = mouseX;
            Y = mouseY;
            radius = 20; 

            clr = Color.FromArgb(0,Color.Yellow);
        }

        public override void Draw(Graphics g)
        {
            var k = Math.Min(1f,(float)countHit / 500);
            int alpha = (int)(k * 255);         //Умножить это знаечние на 255

            clr = Color.FromArgb(alpha, Color.Yellow);

            var b = new SolidBrush(clr);
            g.FillEllipse(b, X - radius, Y - radius, radius * 2, radius * 2);
            g.DrawEllipse(new Pen(Color.FromArgb(255, 0, 0), 4), X - radius, Y - radius, radius * 2, radius * 2);

            b = new SolidBrush(Color.White);
            g.DrawString(Convert.ToString(countHit), new Font("Times New Roman", 12), b, X-radius/2 , Y-radius / 2);
            b.Dispose();
        }
    }
}
