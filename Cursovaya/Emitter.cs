using System;
using System.Collections.Generic;
using System.Drawing;

namespace Cursovaya
{
  class Emitter
    {
        public List<Particle> particles = new List<Particle>();
        private float widthScreen;
        //private float heightScreen;

        public Point positionMouse;
       
        public float gravitationX=0;
        public float gravitationY=0.5f;
        public Emitter(float widthScreen, float heightScreen)
        {
            this.widthScreen = widthScreen;
            //this.heightScreen = heightScreen;

            for (var i = 0; i < 300; i++)
            {
                var particle = new Particle();
                particle.X = widthScreen/2;
                particle.Y = heightScreen / 2;

                particles.Add(particle);
            }
        }
        public void UpdateState()      //Update condition of the System
        {

            foreach (var particle in particles)
            {
                particle.life--;
                if (particle.life < 0)
                {
                    particle.life = 20 + Particle.rand.Next(100);

                    particle.X = Particle.rand.Next((int)widthScreen);
                    particle.Y = -particle.radius;

                    var direction = (float)Particle.rand.Next(360);
                    var speed = 1 + Particle.rand.Next(5);

                    //richTextBox1.Text += "Cos: " + Math.Cos(directionInRadians) + "  directionInRadians: " + directionInRadians + "\n";
                    //richTextBox1.Text += "Sin: " + Math.Sin(directionInRadians) + "\n";
                   

                    particle.speedX = (float)(Math.Cos(direction / 180 * Math.PI)*speed);
                    particle.speedY = -(float)( Math.Sin(direction / 180 * Math.PI) * speed);
                }                
                else
                {
                    particle.speedX += gravitationX;
                    particle.speedY += gravitationY;

                    particle.X += particle.speedX;
                    particle.Y += particle.speedY;
                }
            }
        }
    
       private bool mouseIntersectWithParticle(Particle particle) //Метод для процерки пересеклась ли мышь с частицей или нет
        {
            bool intersect = false;

            //Предположим, что наша окружность находится в точке (0;0)
            var startCoordsX = positionMouse.X - particle.X; //Запихиваем точку в начало координат, т.е на то же отнятое число по оси х, что и у окружности
            var startCoordsY = positionMouse.Y - particle.Y;

            var length = Math.Sqrt(Math.Pow(startCoordsX, 2) + Math.Pow(startCoordsY, 2)); //Расстояние от точки мыши до начала координат

            if (length <= particle.radius)
            {
                intersect = true;
            }
            return intersect;
        }
        //public void Overlap(Object particle,Object mouse)
        //{

        //}
        public void Render(Graphics g) //Here we will draw
        {
            foreach (var particle in particles)
            {
                particle.Draw(g);

                if (positionMouse.X>0 && positionMouse.Y > 0 && mouseIntersectWithParticle(particle))
                { //Если пересеклись с мышью,то показываем информацию о частице

                   particle.watchInfo(g);
                  //           g.DrawString("x mouse = " + positionMouse.X + " y = " + positionMouse.Y
                  //               + "\nx particle " + particle.X + " y" + particle.Y
                  //, new Font("Arial", 12), new SolidBrush(Color.Red), particle.X, particle.Y);

                }
            }
        }
    }
}
