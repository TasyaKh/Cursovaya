using System;
using System.Drawing;
using System.Windows.Forms;
namespace Cursovaya
{
    public partial class Form1 : Form
    {
        Emitter emitter;
        Graphics g;
        bool pause;  //Остановить движение частиц
      
        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            emitter = new Emitter(picDisplay.Image.Width, picDisplay.Image.Height);
            pause = false;

            g = Graphics.FromImage(picDisplay.Image);
           // posScroll = trackBar1.Value;

            //timer1.Interval = trackBar1.Value != 0 ? minSpeedScroll * posScroll : minSpeedScroll; //Ести число на скролле не равно 0, 
        }                                                                                 //то умножаем это число на минимальную скорость

        private void events()
        {
            if (!pause)
            {
                emitter.UpdateState();                      //Обновить данные о частицах
            }
            //using (var g = Graphics.FromImage(picDisplay.Image))
            //{
           // richTextBox1.Text += "x cursor " + Cursor.Position.X + "   y " + Cursor.Position.Y;
            g.Clear(Color.Black);                           //Очистить холст
            emitter.Render(g);    
            //}
            picDisplay.Invalidate();
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
             events();
        }

        private void scrollSpeedParticles_Scroll(object sender, EventArgs e)
        {
            if(trackBar1.Value>0)
                emitter.speedScroller = (float)(trackBar1.Value) / 10;
            else
                emitter.speedScroller = 1f / 10*2;
            //int maxIntervalTimer = 400;                                   //Максимальная скорость замедлени(таймер)
            //int minIntervalTimer = maxIntervalTimer / trackBar1.Maximum;  //Минимальная скорость таймера

            //if (trackBar1.Value < trackBar1.Maximum)
            //{
            //    timer1.Interval = maxIntervalTimer - minIntervalTimer * trackBar1.Value;
            //}
            //else if (trackBar1.Value == trackBar1.Maximum) //Interval у таймера не может бытть равен 0, поэтому устанавливаем скорость вручную
            //    timer1.Interval = 40;
        }

        private void Stop_Click(object sender, EventArgs e) //По нажатию клавиши "Стоп"
        {
            pause = pause?false:true;                             //Если стояло на паузе, о включаем и наоборот

            //trackBar1.Value = trackBar1.Maximum;
            //timer1.Interval = 40;
            trackBar1.Enabled = pause ? false : true; //Ести скроллер скорости вкллючен, то выкл и наоборот
        }

        private void Step_Click(object sender, EventArgs e) //По нажатию клавиши Шаг
        {
            pause = false;
            events();
            pause = true;
        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            emitter.positionMouse = new Point(e.X,e.Y);
        }

        private void picDisplay_MouseLeave(object sender, EventArgs e)
        {
            emitter.positionMouse = new Point(-1, -1);
        }

        private void picDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                CircleCollector circleCollector = new CircleCollector(e.X, e.Y);
                if(emitter.circleCollectors.Count<5)
                     emitter.circleCollectors.Add(circleCollector);
            }
            else if (e.Button == MouseButtons.Right)
            {
                emitter.deleteCollector(e.X,e.Y);
            }
        }
    }
}
