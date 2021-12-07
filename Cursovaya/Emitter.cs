using System;
using System.Collections.Generic;
using System.Drawing;
   

namespace Cursovaya
{
  class Emitter
    {
        public List<Particle> particles = new List<Particle>();
        public List<CircleCollector> circleCollectors =  new List<CircleCollector>();
        private float widthScreen;
        public float speedScroller;  //Скроллер скорости замедления
                                     //private float heightScreen;
       // public const float gravitationX = 0;
        public const float gravitationY = 0.5f;
        public Point positionMouse;
       
        public Emitter(float widthScreen, float heightScreen)
        {
            this.widthScreen = widthScreen;
            speedScroller = 1f;
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
                    var speed = (1 + Particle.rand.Next(5));

                    //richTextBox1.Text += "Cos: " + Math.Cos(directionInRadians) + "  directionInRadians: " + directionInRadians + "\n";
                    //richTextBox1.Text += "Sin: " + Math.Sin(directionInRadians) + "\n";
                   

                    particle.speedX = (float)(Math.Cos(direction / 180 * Math.PI)*speed);
                    particle.speedY = -(float)( Math.Sin(direction / 180 * Math.PI) * speed);
                }                
                else
                {
                    //particle.speedX += gravitationX;
                    particle.speedY += gravitationY;

                    particle.X += particle.speedX * speedScroller;
                    particle.Y += particle.speedY * speedScroller;
                    //Emitter.speedScroller = speedScroller;
                }
            }
        }
    
      
        
        private void collectorIntersectParticle(Particle particle)
        {
            foreach (var collector in circleCollectors)
            {
             
                if (typesIntersects.circlesOverlap(particle,collector)&& particle.life>0)
                {
                    if (collector.countHit<500)
                        collector.countHit++;
                    particle.life = 0;
                }
            }
        }

        public void deleteCollector(int mouseX, int mouseY)
        {
            foreach (var collector in circleCollectors)
            {
                if (typesIntersects.mouseIntersectWithCircle(collector, new Point(mouseX, mouseY)))
                {
                    circleCollectors.Remove(collector);
                    break;
                }
            }
        }

        public void Render(Graphics g) //Here we will draw
        {
 
                foreach (var particle in particles)
                {
                
                if (circleCollectors.Count != 0)
                {
                    collectorIntersectParticle(particle);
                }

                particle.Draw(g);

                if (positionMouse.X > 0 && positionMouse.Y > 0 && typesIntersects.mouseIntersectWithCircle(particle, positionMouse))
                { //Если пересеклись с мышью,то показываем информацию о частице
                    particle.watchInfo(g);
                    //           g.DrawString("x mouse = " + positionMouse.X + " y = " + positionMouse.Y
                    //               + "\nx particle " + particle.X + " y" + particle.Y
                    //, new Font("Arial", 12), new SolidBrush(Color.Red), particle.X, particle.Y);
                }
            }

            foreach (var collector in circleCollectors)
            {
                collector.Draw(g);
            }

            }
    }
}
