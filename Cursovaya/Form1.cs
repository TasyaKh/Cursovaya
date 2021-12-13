using System;
using System.Drawing;
using System.Windows.Forms;
namespace Cursovaya
{
    public partial class Form1 : Form
    {
        Emitter emitter;             //Эммиттер для частиц
        ManagerOtherObjects manager; //Менеджер для взаимодействия различных частиц друг с другом
        Graphics g;                  //Объект графики
        bool pause;                  //Остановить движение частиц

        public Form1()
        {
            InitializeComponent();

            picDisplay.MouseWheel += picDisplay_MouseWheel; //Добавить событие на колесико мышки
            label1.Text = Convert.ToString(trackBar1.Value);//Цифра возле скроллера (показывает текущую выбранную скорость)
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);
            manager = new ManagerOtherObjects();
            emitter = new Emitter(picDisplay.Image.Width, picDisplay.Image.Height, manager);
            pause = false;

            g = Graphics.FromImage(picDisplay.Image);
        }

        private void paint() //События по торисовке частиц
        {
            if (!pause)      //Если не пауза, то обновляем информацию о частицах
            {
                emitter.UpdateState(); //Обновить данные о частицах
            }
            g.Clear(Color.Black);      //Очистить холст
            emitter.Render(g);         //Отрисовать частицы               
            manager.Render(g);         //Отрисовать остальные объекты
            picDisplay.Invalidate();   //Начать рисовать
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            paint(); //Рисовать
        }

        private void scrollSpeedParticles_Scroll(object sender, EventArgs e) //При перетаскивании ползунка изменения скорости
        {
            if (trackBar1.Value > 0)                  //Если ползунок дальше нуля
                emitter.speedScroller = (float)(trackBar1.Value) / 10; //Значение скроллера скорости будет равно
            else
                emitter.speedScroller = 1f / 10 * 2; //если ползунок равен нулю 

        }

        private void Stop_Click(object sender, EventArgs e) //По нажатию клавиши "Стоп"
        {
            pause = pause ? false : true;                   //Если стояло на паузе, то включаем и наоборот

            trackBar1.Enabled = pause ? false : true;       //Ести скроллер скорости включен, то выкл и наоборот
        }

        private void Step_Click(object sender, EventArgs e) //По нажатию клавиши Шаг
        {
            pause = false; //Запускаем движение частиц
            paint();       //Рисуем теперь с движение частиц
            pause = true;  //Снова ставим на паузу
        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e) //При движении мыши
        {
           if(manager.radar != null) //Если радар существует, то
                manager.radar.positionChange(e.X, e.Y); //Изменить позицию радара

            emitter.positionMouse = new Point(e.X, e.Y); //отправить данные о положении мыши эмиттеру
        }

        private void picDisplay_MouseLeave(object sender, EventArgs e)
        {
            emitter.positionMouse = new Point(-1, -1); //Передать позицию мыши
        }

        private void picDisplay_MouseClick(object sender, MouseEventArgs e) //По нжатию мыши
        {
            if (e.Button == MouseButtons.Left)  //Если нажали левую клавишу мыши
            {
                CircleCollector circleCollector = new CircleCollector(e.X, e.Y); //Создать новый сборщик частиц
                if (manager.circleCollectors.Count < 5)                             //Если количесто сборщиков не превышает
                    manager.circleCollectors.Add(circleCollector);              //Добавить сборщик
            }
            else if (e.Button == MouseButtons.Right) //Если правую клавишу
            {
                manager.deleteCollector(e.X, e.Y);   //Удалить сборщик
            }
        }

        private void switchOnInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (!switchOnRealm.Checked)//Если выключен режим просмотра области с частицами
            {
                emitter.typeOfMouseMove = TypeOfMouseMove.INFOPARTICLE; //Будем показывать информацию о частицах при наведениина них
                manager.radar = null;
            }
            else
            {
                emitter.typeOfMouseMove = TypeOfMouseMove.REALMPARTICLES; //Будем взаимодействовать с радаром
                int r = 40;
                //if (e.X-r > 0 && e.Y - r > 0 && e.X < picDisplay.Width-r && e.Y < picDisplay.Height - r)
                //{
                Point pos = emitter.positionMouse;
                manager.radar = new Radar(pos.X, pos.Y,r);
                //}
                //else emitter.radar = null;
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e) //Если двигаем скроллер
        {
            label1.Text = Convert.ToString(trackBar1.Value); //Показываем выбранное значение скроллера
        }

        private void picDisplay_MouseWheel(object sender, MouseEventArgs e) //Для колесика мыши
        {
            if (manager.radar != null) //Если есть радар
            {
                if (e.Delta > 0)       //Если колесико двигается вперед
                {
                    manager.radar.resize(false); //Уменьшаем радар
                }
                else                            //Если колесико двигается назад
                {
                    manager.radar.resize(true); //Увеличиваем радар
                }
            }
        }
    }
}
