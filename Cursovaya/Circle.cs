using System;
using System.Drawing;

namespace Cursovaya
{
    abstract class Circle
    {
        public int radius; //Радиус круга
        public float X;    //Координата по оси х
        public float Y;    //Координата по оси у

        public static Random rand = new Random();
        public Color clr;  //Цвет круга

        public Circle()
        {
            radius = 0;
            X = Y = 0;
            clr = Color.White;
        }

        public abstract void Draw(Graphics g); //Для создания фигуры
    }
}
