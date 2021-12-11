using System.Drawing;

namespace Cursovaya
{
    class ColorfulCircle : Circle
    {
        public ColorfulCircle(float mouseX, float mouseY)
        {
            radius = 20;
            this.X = mouseX;
            this.Y = mouseY;

            //clr = createNewColor();
        }
       
        public override void Draw(Graphics g)
        {
            g.DrawEllipse(new Pen(clr,2), X - radius, Y - radius, radius * 2, radius * 2);
        }
    }
}
