using System;
using System.Collections.Generic;
using System.Drawing;
   

namespace Cursovaya
{
  class Emitter
    {
        public List<Particle> particles = new List<Particle>();                        //Хранит частицы
        public List<CircleCollector> circleCollectors =  new List<CircleCollector>();  //Хранит сборщики частиц
        public List<ColorfulCircle> colorfulCircles = new List<ColorfulCircle>();      //Хранит цветные круги
          
        private float widthScreen;   //Ширина области рисования  
        public float speedScroller;  //Скроллер скорости замедления
                                     //private float heightScreen;
        public const float gravitationY = 0.5f;
        public Point positionMouse;
       
        public Emitter(float widthScreen, float heightScreen)
        {

            this.widthScreen = widthScreen;
            speedScroller = 1f;

            for (var i = 0; i < 300; i++) //создать частицы
            {
                var particle = new Particle();
                particle.X = widthScreen/2;
                particle.Y = heightScreen / 2;

                particles.Add(particle);
            }
        }
        public void UpdateState()      //оюновить информацию о частицах
        {

            foreach (var particle in particles)
            {
                particle.life--;
                if (particle.life < 0)
                {
                    particle.setColor(Color.White); //Покрасить в белый цвет

                    particle.life = 20 + Particle.rand.Next(200);

                    particle.X = Particle.rand.Next((int)widthScreen);
                    particle.Y = -particle.radius;

                    var direction = (float)Particle.rand.Next(360);  //направление
                    var speed = (1 + Particle.rand.Next(5));         //скорость движения

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
    
        
        private void particleIntersectCollector(Particle particle) //Выполнить проверку на персечение со сборщиком
        {
            List<CircleCollector> fullHit = new List<CircleCollector>();

            foreach (var collector in circleCollectors)            //перебираем сборщики
            {
             
                if (typesIntersects.circlesOverlap(particle,collector)&& particle.life>0) //если произошло пересечение и частица не мертва
                {
                    if (collector.countHit<100) //если число попаданий меньше 100
                        collector.countHit++;   //увеличить число попаданий
                    else
                    {
                        createClrCircle(collector.X,collector.Y, collector.clr); //Cоздать цветной круг на этом месте
                        fullHit.Add(collector);
                    }
                    particle.life = 0;          //уничтожить частицу
                }
            }

            foreach (var full in fullHit) //Удалить автоматически заполненные сборщики
            {
                circleCollectors.Remove(full);
            }
        }

        public void deleteCollector(int mouseX, int mouseY) //удалить сборщик частиц
        {
            foreach (var collector in circleCollectors)
            {
                if (typesIntersects.mouseIntersectWithCircle(collector, new Point(mouseX, mouseY)))
                {  //если сборщик пересекся с мышью
                    circleCollectors.Remove(collector); //удалить сборщик
                    break;
                }
            }
        }
        private void particleIntersectClrCircle(Particle particle) //Если частица пересеклась с кругами цветными
        {
            foreach (var cC in colorfulCircles)
            {
                if (typesIntersects.circlesOverlap(particle, cC))
                    particle.setColor(cC.clr);
            }
            
        }
        private void createClrCircle(float X, float Y,Color clr)
        {
            ColorfulCircle colorfulCircle = new ColorfulCircle(X, Y); //Создать новый цветной круг

            if (colorfulCircles.Count >= 5)
            {
                colorfulCircles.RemoveAt(0);
            }
            colorfulCircle.clr = clr;
            colorfulCircles.Add(colorfulCircle); //Добавить этот круг
        }
        
        public void Render(Graphics g) //Here we will draw
        {
            Particle partInfo = null;

            foreach (var particle in particles) //перебираем все частицы
                {
                    particleIntersectCollector(particle); //проверить пересечение частицы со сборщиками
                    particleIntersectClrCircle(particle); //проверить пересечение частицы с цветными кругами

                if (positionMouse.X > 0 && positionMouse.Y > 0 && typesIntersects.mouseIntersectWithCircle(particle, positionMouse))
                { //Если пересеклись с мышью,то показываем информацию о частице

                    partInfo = particle; //Приравниваем к частице, для которой мы хотим вывести ифнормацию
                  
                                           //           g.DrawString("x mouse = " + positionMouse.X + " y = " + positionMouse.Y
                                           //               + "\nx particle " + particle.X + " y" + particle.Y
                                           //, new Font("Arial", 12), new SolidBrush(Color.Red), particle.X, particle.Y);
                }
                particle.Draw(g);  //отрисовать частицу
            }

            foreach(var colorf in colorfulCircles){     //Отрисовка цветных кругов
                colorf.Draw(g);
            }

            foreach (var collector in circleCollectors) //Отрисовка сборщиков
            {
                collector.Draw(g); //отрисоввать сборщики с количеством частиц, которые попали на них
            }

            if(partInfo!=null)
                partInfo.watchInfo(g); //показать информацию о частице, c которой мыщь пересеклась
        }
    }
}
