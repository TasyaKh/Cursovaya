using System;
using System.Drawing;


namespace Cursovaya
{
    class Particle:Circle
    {
        //public bool watch_Info;

   
        public float speedX;
        public float speedY;

        public float life; //Количество жизней

        public Particle()
        { 
            radius = 5 + rand.Next(15);
            life = 0;
          
            clr = Color.White;
        }

        public void setColor(Color color)
        {
            clr = color;
        }
        private float vectorWithoutGravitation()
        {
            var y2 = Y + speedY;               //Послудующая точка(где перерисуется частица)
            float d = (float)Math.Sqrt(Math.Pow(-speedX, 2) + Math.Pow(-speedY, 2)); //Найти расстояние до этой точки от текущего положения частицы

            if (d > radius)      //Если расстояние больше радиуса,то
                y2 -= (d - radius); //вектор движения частиц будет по размеру равен радиусу
            else
            {
                y2 = Y + radius;
            }
            return y2;
        }
        public override void Draw(Graphics g)   //Рисовать
        {
            float k = Math.Min(1f, life / 200); //Найти минимальное занчение среди текущей жизни и 1
            int alpha = (int)(k * 255);         //Умножить это знаечние на 255

            var color = Color.FromArgb(alpha,clr);
            var b = new SolidBrush(color);
            g.FillEllipse(b, X - radius, Y - radius, radius * 2, radius * 2);

            var y2 = vectorWithoutGravitation();

            g.DrawLine(new Pen(Color.Blue,2), X, Y, X + speedX, y2); //Рисовать вектор направления движения
          
            b.Dispose();
        }
        public void watchInfo(Graphics g) //Показать информацию о частице
        {
            var b = new SolidBrush(Color.FromArgb(180,Color.White));

            g.FillRectangle(b, X + radius, Y ,120, 60);

            b = new SolidBrush(Color.Black);
            g.DrawString("x = " + X + "\ny = " + Y
          + "\nLife= " + life, new Font("Times New Roman", 12), b,X + radius, Y);

            g.DrawEllipse(new Pen(Color.FromArgb(255,0,0), 5), X - radius,
                Y - radius, radius * 2, radius * 2);

            b.Dispose();
        }

    }
}
