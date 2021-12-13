﻿using System;
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
            label1.Text = Convert.ToString(trackBar1.Value);
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            emitter = new Emitter(picDisplay.Image.Width, picDisplay.Image.Height);
            pause = false;

            g = Graphics.FromImage(picDisplay.Image);
        }

        private void paint() //События по торисовке чатиц
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
            paint();
        }

        private void scrollSpeedParticles_Scroll(object sender, EventArgs e) //При перетаскивании ползунка изменения скорости
        {
            if (trackBar1.Value > 0)                  //Если ползунок дальше нуля
                emitter.speedScroller = (float)(trackBar1.Value) / 10;
            else
                emitter.speedScroller = 1f / 10 * 2; //если ползунок равен нулю 

        }

        private void Stop_Click(object sender, EventArgs e) //По нажатию клавиши "Стоп"
        {
            pause = pause ? false : true;                             //Если стояло на паузе, о включаем и наоборот

            trackBar1.Enabled = pause ? false : true; //Ести скроллер скорости вкллючен, то выкл и наоборот
        }

        private void Step_Click(object sender, EventArgs e) //По нажатию клавиши Шаг
        {
            pause = false;
            paint();
            pause = true;
        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e) //При движении мыши
        {
            if (switchOnInfo.Checked)//Если включен режим просмотра информации о частице
            {
                emitter.typeOfMouseMove = TypeOfMouseMove.INFOPARTICLE;
                //emitter.radar = null;
            }
            else if (switchOnRealm.Checked)//Если включен режим просмотра области с частицами
            {
                emitter.typeOfMouseMove = TypeOfMouseMove.REALMPARTICLES;

                int r = 40;
                if (e.X-r > 0 && e.Y - r > 0 && e.X < picDisplay.Width-r && e.Y < picDisplay.Height - r)
                {
                    emitter.radar = new Radar(e.X, e.Y, r);
                }
                else emitter.radar = null;
            }
            emitter.positionMouse = new Point(e.X, e.Y);
        }

        private void picDisplay_MouseLeave(object sender, EventArgs e)
        {
            emitter.positionMouse = new Point(-1, -1); //Передать позицию мыши
        }

        private void picDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)  //Если нажали левую клавишу мыши
            {
                CircleCollector circleCollector = new CircleCollector(e.X, e.Y); //Создать новый сборщик частиц
                if (emitter.circleCollectors.Count < 5)                             //Если количесто сборщиков не превышает
                    emitter.circleCollectors.Add(circleCollector);              //Добавить сборщик
            }
            else if (e.Button == MouseButtons.Right) //Если правую клавишу
            {
                emitter.deleteCollector(e.X, e.Y);   //Удалить сборщик
            }
        }

        private void switchOnInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (!switchOnRealm.Checked)//Если выключен режим просмотра области с частицами
            {
                emitter.radar = null;
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = Convert.ToString(trackBar1.Value);
        }
    }
}
