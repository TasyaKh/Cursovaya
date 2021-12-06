using System;
using System.Drawing;


namespace Cursovaya
{
    class Particle
    {
        //public bool watch_Info;

        public int radius;
        public float X;
        public float Y;

        public float speedX;
        public float speedY;

        public float life; //Количество жизней

        public static Random rand = new Random();
        public Color clr;

        public Particle()
        {
            radius = 2 + rand.Next(10);
            life = -1;
            // clr = Color.FromArgb(100+rand.Next(156), 100 + rand.Next(156), 100 + rand.Next(156));
            clr = Color.White;

            //watch_Info = false;
        }
        public void Draw(Graphics g) //Рисовать
        {
            float k = Math.Min(1f, life / 100); //Найти минимальное занчение среди текущей жизни и 1
            int alpha = (int)(k * 255);         //Умножить это знаечние на 255

            var color = Color.FromArgb(alpha,clr);
            var b = new SolidBrush(color);
            g.FillEllipse(b, X - radius, Y - radius, radius * 2, radius * 2);
            g.DrawLine(new Pen(Color.Blue,2), X, Y, X + speedX, Y + speedY);

          
            b.Dispose();
        }
        public void watchInfo(Graphics g) //Показать информацию о частице
        {
            var b = new SolidBrush(Color.FromArgb(124, 252, 0));

            g.DrawString("x = " + X + "\ny = " + Y
          + "\nLife= " + life, new Font("Times New Roman", 12), b,X + 10, Y);

            g.DrawEllipse(new Pen(Color.FromArgb(255,0,0), 5), X - radius,
                Y - radius, radius * 2, radius * 2);

            b.Dispose();
        }
    }
}
