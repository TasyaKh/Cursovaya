using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursovaya
{
    abstract class Circle
    {
        public int radius;
        public float X;
        public float Y;

        public static Random rand = new Random();
        public Color clr;

        public Circle()
        {
            radius = 0;
            X = Y = 0;
            clr = Color.White;
        }

        public abstract void Draw(Graphics g);
    }
}
