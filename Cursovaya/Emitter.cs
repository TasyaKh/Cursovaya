using System;
using System.Collections.Generic;
using System.Drawing;
   

namespace Cursovaya
{
   public enum TypeOfMouseMove{ //Что делать при движении мыши
        INFOPARTICLE,           //Получить ифнорм о частице
        REALMPARTICLES,         //Показать область радар
    }
  class Emitter
    {
        public List<Particle> particles = new List<Particle>();                        //Хранит частицы
          
        private float widthScreen;   //Ширина области рисования  
        public float speedScroller;  //Скроллер скорости замедления
                                    
        public const float gravitationY = 0.5f; //гравитация по оси у
        public Point positionMouse;             //Позиция мыши
        private int numParticles;               //Число частиц

        public TypeOfMouseMove typeOfMouseMove; //Что делать при движеннии мыши
        public ManagerOtherObjects manager;
        public Emitter(float widthScreen, float heightScreen,ManagerOtherObjects manager)
        {
            this.manager = manager;
            typeOfMouseMove = TypeOfMouseMove.INFOPARTICLE;

            this.widthScreen = widthScreen;
            speedScroller = 1f;

            numParticles = 300;
            for (var i = 0; i < numParticles; i++) //создать частицы
            {
                var particle = new Particle();
                particle.X = widthScreen/2;
                particle.Y = heightScreen / 2;

                particles.Add(particle);
            }
        }
        public void UpdateState()      //оюновить информацию о частицах
        {

            foreach (var particle in particles) //Обновляем все частицы
            {
                particle.life--;
                if (particle.life < 0)
                {
                    particle.setColor(Color.White); //Покрасить в белый цвет

                    particle.life = 20 + Particle.rand.Next((int)particle.getMaxLife());

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

                    particle.X += particle.speedX * speedScroller; //Добавили скроллер скорости, который контролируется бегунком
                    particle.Y += particle.speedY * speedScroller;
                }
                manager.particleIntersectCollector(particle); //проверить пересечение частицы со сборщиками
                manager.particleIntersectClrCircle(particle); //проверить пересечение частицы с цветными кругами
            }
        }
        public bool mouseIntersectWithCircle(Particle particle)
        {
            bool seeInfo = false;

            //Particle partInfo = null;

            if (positionMouse.X > 0 && positionMouse.Y > 0 && typesIntersects.mouseIntersectWithCircle(particle, positionMouse))
            { //Если пересеклись с мышью,то показываем информацию о частице
                seeInfo = true;
                //partInfo = particle; //Приравниваем к частице, для которой мы хотим вывести ифнормацию

                //           g.DrawString("x mouse = " + positionMouse.X + " y = " + positionMouse.Y
                //               + "\nx particle " + particle.X + " y" + particle.Y
                //, new Font("Arial", 12), new SolidBrush(Color.Red), particle.X, particle.Y);
            }

            return seeInfo;
        }

        public void Render(Graphics g) //Here we will draw
        {
            Particle watchInfo = null;
            //radar = null;

            foreach (var particle in particles) //перебираем все частицы
            {

                if (typeOfMouseMove == TypeOfMouseMove.INFOPARTICLE && mouseIntersectWithCircle(particle))
                {
                    watchInfo = particle;
                }
                else if (typeOfMouseMove == TypeOfMouseMove.REALMPARTICLES)
                {
                    manager.particleIntersectRadar(particle);
                }

                particle.Draw(g);  //отрисовать частицу
            }

            if(watchInfo!=null)
                watchInfo.drawInfo(g); //показать информацию о частице, c которой мыщь пересеклась
        }
    }
}
