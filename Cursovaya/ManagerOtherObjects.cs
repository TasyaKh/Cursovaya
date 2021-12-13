using System.Collections.Generic;
using System.Drawing;

namespace Cursovaya
{
    class ManagerOtherObjects
    {
        public List<CircleCollector> circleCollectors = new List<CircleCollector>();  //Хранит сборщики частиц
        public List<ColorfulCircle> colorfulCircles = new List<ColorfulCircle>();      //Хранит цветные круги
        public Radar radar; //Радар, который крепится к мыши (круг)

        private int maxNumTapCircles;           //Число сборщиков макс

        public ManagerOtherObjects()
        {
            maxNumTapCircles = 5;
        }
        public void particleIntersectCollector(Particle particle) //Выполнить проверку на персечение со сборщиком
        {
            List<CircleCollector> fullHit = new List<CircleCollector>();

            foreach (var collector in circleCollectors)            //перебираем сборщики
            {

                if (typesIntersects.circlesOverlap(particle, collector) && particle.life > 0) //если произошло пересечение и частица не мертва
                {
                    if (collector.countHit < collector.getMaxCountHit()) //если число попаданий меньше х
                        collector.countHit++;                          //увеличить число попаданий
                    else                                              //Если сборщик заполнен
                    {
                        createClrCircle(collector.X, collector.Y, collector.clr); //Cоздать цветной круг на этом месте
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
        public void particleIntersectClrCircle(Particle particle) //Если частица пересеклась с кругами цветными
        {
            foreach (var cC in colorfulCircles)
            {
                if (typesIntersects.circlesOverlap(particle, cC))
                    particle.setColor(cC.clr);                    //Задать цвет частице
            }

        }
        public void createClrCircle(float X, float Y, Color clr)      //Создать цветной круг (цвет меняет)
        {
            ColorfulCircle colorfulCircle = new ColorfulCircle(X, Y); //Создать новый цветной круг

            if (colorfulCircles.Count >= maxNumTapCircles)            //Если число цветных кругов превысило допустимое
            {  //Удалить цветной круг 1-й в списке
                colorfulCircles.RemoveAt(0);
            }
            colorfulCircle.clr = clr; //создать новый
            colorfulCircles.Add(colorfulCircle); //Добавить этот круг
        }

        public void particleIntersectRadar(Particle particle)
        {
            if (radar == null)
                return;
            //Создадим подрадар радара размерностью = радар - радиус частицы*2
            //Т.е. частица подсветится, если она полностью окажется в круге
            Radar psevdoradar = new Radar(radar.X, radar.Y, radar.radius - particle.radius * 2);

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
        public void Render(Graphics g)
        {
            foreach (var colorf in colorfulCircles)
            {     //Отрисовка цветных кругов
                colorf.Draw(g);
            }

            foreach (var collector in circleCollectors) //Отрисовка сборщиков
            {
                collector.Draw(g); //отрисоввать сборщики с количеством частиц, которые попали на них
            }
            if (radar != null) 
                radar.Draw(g);
        }
    }
}
