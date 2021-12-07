using System;
using System.Drawing;


namespace Cursovaya
{
    class Particle:Circle
    {
        //public bool watch_Info;

   
        public float speedX;
        public float speedY;
       // public float speedScroller;  //Скроллер скорости замедления

        public float life; //Количество жизней

        public Particle()
        { 
            radius = 5 + rand.Next(15);
            life = 0;
          
            clr = Color.White;
            // speedScroller = 1f;
            // clr = Color.FromArgb(100+rand.Next(156), 100 + rand.Next(156), 100 + rand.Next(156));
            //watch_Info = false;
        }
        public override void Draw(Graphics g) //Рисовать
        {
            float k = Math.Min(1f, life / 100); //Найти минимальное занчение среди текущей жизни и 1
            int alpha = (int)(k * 255);         //Умножить это знаечние на 255

            var color = Color.FromArgb(alpha,clr);
            var b = new SolidBrush(color);
            g.FillEllipse(b, X - radius, Y - radius, radius * 2, radius * 2);

            var y2 = Y + speedY;
            float d = (float)Math.Sqrt(Math.Pow(-speedX,2) + Math.Pow(-speedY, 2));
            if (d > radius)
               y2 -= (d-radius);
            else
            {
                y2 = Y+radius;
            }

            g.DrawLine(new Pen(Color.Blue,2), X, Y, X + speedX, y2);

          
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
