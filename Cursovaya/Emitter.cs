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
        public List<CircleCollector> circleCollectors =  new List<CircleCollector>();  //Хранит сборщики частиц
        public List<ColorfulCircle> colorfulCircles = new List<ColorfulCircle>();      //Хранит цветные круги
        public Radar radar;
          
        private float widthScreen;   //Ширина области рисования  
        public float speedScroller;  //Скроллер скорости замедления
                                    
        public const float gravitationY = 0.5f; //гравитация по оси у
        public Point positionMouse;             //Позиция мыши
        private int numParticles;               //Число частиц
        private int maxNumTapCircles;           //Число сборщиков макс

        public TypeOfMouseMove typeOfMouseMove; //Что делать при движеннии мыши

        public Emitter(float widthScreen, float heightScreen)
        {
            typeOfMouseMove = TypeOfMouseMove.INFOPARTICLE;

            maxNumTapCircles = 5;
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
                particleIntersectCollector(particle); //проверить пересечение частицы со сборщиками
                particleIntersectClrCircle(particle); //проверить пересечение частицы с цветными кругами
            }
        }
            private void particleIntersectCollector(Particle particle) //Выполнить проверку на персечение со сборщиком
        {
            List<CircleCollector> fullHit = new List<CircleCollector>();

            foreach (var collector in circleCollectors)            //перебираем сборщики
            {
             
                if (typesIntersects.circlesOverlap(particle,collector)&& particle.life>0) //если произошло пересечение и частица не мертва
                {
                    if (collector.countHit<collector.getMaxCountHit()) //если число попаданий меньше х
                        collector.countHit++;                          //увеличить число попаданий
                    else                                              //Если сборщик заполнен
                    {
                        createClrCircle(collector.X,collector.Y, collector.clr); //Cоздать цветной круг на этом месте
                        fullHit.Add(collector);//Добавить коллектор в "мусор", удалить надо
                    }
                    particle.life = 0;          //уничтожить частицу
                }
            }

            foreach (var full in fullHit) //Удалить заполненные сборщики
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
                    particle.setColor(cC.clr);                    //Задать цвет частице
            }
            
        }
        private void createClrCircle(float X, float Y,Color clr)      //Создать цветной круг (цвет меняет)
        {
            ColorfulCircle colorfulCircle = new ColorfulCircle(X, Y); //Создать новый цветной круг

            if (colorfulCircles.Count >= maxNumTapCircles)            //Если число цветных кругов превысило допустимое
            {  //Удалить цветной круг 1-й в списке
                colorfulCircles.RemoveAt(0);
            }
            colorfulCircle.clr = clr; //создать новый
            colorfulCircles.Add(colorfulCircle); //Добавить этот круг
        }

        private bool mouseIntersectWithCircle(Particle particle)
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
        private void particleIntersectRadar(Particle particle)
        { 
            if (radar == null)
                return;
            //Создадим подрадар радара размерностью = радар - радиус частицы*2
            //Т.е. частица подсветится, если она полностью окажется в круге
            Radar psevdoradar = new Radar(radar.X,radar.Y,radar.radius - particle.radius*2);

            if (typesIntersects.circlesOverlap(psevdoradar, particle))
            {
                radar.countHit++;
                particle.switchUnderColor = true;
            }
            else
            {
                particle.switchUnderColor = false;
            }
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
                    particleIntersectRadar(particle);
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

            if(watchInfo!=null)
                watchInfo.drawInfo(g); //показать информацию о частице, c которой мыщь пересеклась

            if(radar!=null)radar.Draw(g);
        }
    }
}
