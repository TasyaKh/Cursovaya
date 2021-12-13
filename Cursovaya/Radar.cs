using System;
using System.Drawing;

namespace Cursovaya
{
    class Radar : Circle
    {
        public int countHit;
        public Radar(float mouseX, float mouseY,int radius)
        {
            this.radius = radius;
            X = mouseX;
            Y = mouseY;

            countHit = 0;
            clr = Color.White;
        }
        public override void Draw(Graphics g)
        {
            var b = new SolidBrush(Color.FromArgb(180, Color.Red));
            //Обводка
            g.DrawEllipse(new Pen(clr, 2), X - radius, Y - radius, radius * 2, radius * 2);
            g.DrawString(Convert.ToString(countHit), new Font("Times New Roman", 14,FontStyle.Bold), b, X, Y - 20);

            countHit = 0;
            b.Dispose();
        }
    }
}
