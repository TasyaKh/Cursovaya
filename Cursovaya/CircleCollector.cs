using System;
using System.Drawing;

namespace Cursovaya
{
    class CircleCollector:Circle
    {
        public int countHit;  //Счетчик числа попаданий
        private int maxCountHit;
        public CircleCollector(int mouseX, int mouseY)
        {
            maxCountHit = 100;
            countHit = 0;

            X = mouseX;
            Y = mouseY;
            radius = 20;

            clr = createNewColor();//Color.FromArgb(0,Color.Yellow);
        }
        public int getMaxCountHit()
        {
            return maxCountHit;
        }
        private Color createNewColor()
        {
            int r = 255* rand.Next(2); 
            int g = 255 * rand.Next(2);
            int b = 255 * rand.Next(2);

            if (r == g && g == b)
                r = (r == 255)?0:255;   //По умолчанию пусть красный будет доминировать (чтобы белого и черного цвета не было)

            return Color.FromArgb(r, g, b);
        }

        public override void Draw(Graphics g)
        {
            var k = Math.Min(1f,(float)countHit / 100); //Яркость цвета (0;1)
            int alpha = (int)(k * 255);                //Умножить это знаечние на 255
            Color colorBright = Color.FromArgb(alpha, clr.R, clr.G, clr.B);
            //clr = Color.FromArgb(alpha, Color.Yellow);

            var b = new SolidBrush(colorBright);
            g.FillEllipse(b, X - radius, Y - radius, radius * 2, radius * 2);
            g.DrawEllipse(new Pen(Color.White, 2), X - radius, Y - radius, radius * 2, radius * 2);

            b = new SolidBrush(Color.White);
            g.DrawString(Convert.ToString(countHit), new Font("Times New Roman", 12), b, X-radius/2 , Y-radius / 2);
            b.Dispose();
        }
    }
}
