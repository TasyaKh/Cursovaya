using System;
using System.Drawing;
using System.Linq;

namespace Cursovaya
{
    class typesIntersects
    {
      public static bool circlesOverlap(Circle one,Circle two) //Для проверки пересечения двух кругов
        {
            bool intersects = false;                           //Пересечклись или нет

            var d = Math.Sqrt(Math.Pow(one.X- two.X,2) + Math.Pow(one.Y - two.Y, 2));//расстояниме между двумя окружностями

            if (d <= one.radius + two.radius)                                        //Если это расстояние меньше чем сумма радиусов окружностей
            {
                intersects = true;
            }

            return intersects;
        }

        public static bool mouseIntersectWithCircle(Circle circle, Point positionMouse) //Метод для процерки пересеклась ли мышь с частицей или нет
        {
            bool intersect = false;

            //Предположим, что наша окружность находится в точке (0;0)
            var startCoordsX = positionMouse.X - circle.X; //Запихиваем точку в начало координат, т.е на то же отнятое число по оси х, что и у окружности
            var startCoordsY = positionMouse.Y - circle.Y;

            var length = Math.Sqrt(Math.Pow(startCoordsX, 2) + Math.Pow(startCoordsY, 2)); //Расстояние от точки мыши до начала координат

            if (length <= circle.radius) //если расстояние от положения мыши до круга меньше или равно радиусу круга
            {
                intersect = true;       //тогда говорим, сто мы пересеклись мышью с кругом
            }
            return intersect;
        }
    }
}
