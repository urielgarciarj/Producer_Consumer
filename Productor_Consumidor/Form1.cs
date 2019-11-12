using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Productor_Consumidor
{
    public partial class Restaurant : Form
    {
        static PictureBox[] characters = new PictureBox[2];
        static PictureBox[] Tables = new PictureBox[8];

        static Queue<PictureBox> myq = new Queue<PictureBox>();

        static Queue<PictureBox> myqcliente = new Queue<PictureBox>();
        public Restaurant()
        {

            InitializeComponent();
            pboxChef.Hide();
            pboxCliente.Hide();
            pboxTable1.Hide();
            pboxTable2.Hide();
            pboxTable3.Hide();
            pboxTable4.Hide();
            pboxTable5.Hide();
            pboxTable6.Hide();
            pboxTable7.Hide();
            pboxTable8.Hide();

            /*characters[0] = pboxChef;
            characters[1] = pboxCliente;*/

            /*Tables[0] = pboxTable1;
            Tables[1] = pboxTable2;
            Tables[2] = pboxTable3;
            Tables[3] = pboxTable4;
            Tables[4] = pboxTable5;
            Tables[5] = pboxTable6;
            Tables[6] = pboxTable7;
            Tables[7] = pboxTable8;*/

            myq.Enqueue(pboxTable1);
            myq.Enqueue(pboxTable2);
            myq.Enqueue(pboxTable3);
            myq.Enqueue(pboxTable4);
            myq.Enqueue(pboxTable5);
            myq.Enqueue(pboxTable6);
            myq.Enqueue(pboxTable7);
            myq.Enqueue(pboxTable8);


            myqcliente.Enqueue(pboxTable1);
            myqcliente.Enqueue(pboxTable2);
            myqcliente.Enqueue(pboxTable3);
            myqcliente.Enqueue(pboxTable4);
            myqcliente.Enqueue(pboxTable5);
            myqcliente.Enqueue(pboxTable6);
            myqcliente.Enqueue(pboxTable7);
            myqcliente.Enqueue(pboxTable8);

        }

        public static class GlobalData
        {
            public static int decision = 0;
            public static int numberOfActionsChef = 0;
            public static int numberOfActionsClient = 1;
            public static int ultimaImagen = 0;
        }

        public async Task action()
        {
            Random rdn = new Random();
            GlobalData.numberOfActionsChef = rdn.Next(1, 4);
        }

        public async void productor()
        {
            await action();
            MessageBox.Show(GlobalData.numberOfActionsChef.ToString());
            pboxCliente.Invoke(new MethodInvoker(delegate
            {
                pboxCliente.Hide();
            }));
            pboxChef.Invoke(new MethodInvoker(delegate
            {
                pboxChef.Show();
            }));

            for(int i =0; i < GlobalData.numberOfActionsChef; i++)
            {
                if(myq.Count == 0)
                {
                    llenarCola();
                }
                PictureBox ver = myq.Dequeue();
                ver.Invoke(new MethodInvoker(delegate
                {
                    ver.Show();
                }));
                await Task.Delay(1000);
            }

            /*List<PictureBox> ImagenesInvisibles = new List<PictureBox>();
            for (int i = GlobalData.ultimaImagen; i < Tables.Length - GlobalData.ultimaImagen; i++)
            {
                if (Tables[i].Visible == false)
                {
                    ImagenesInvisibles.Add(Tables[i]);
                }
            }
            MessageBox.Show(GlobalData.numberOfActionsChef.ToString());
            for (int i = GlobalData.ultimaImagen; i < GlobalData.numberOfActionsChef; i++)
            {
                ImagenesInvisibles[i].Invoke(new MethodInvoker(delegate
                {
                    ImagenesInvisibles[i].Show();
                }));
                GlobalData.ultimaImagen++; 
                await Task.Delay(1000);
            }

            MessageBox.Show(GlobalData.ultimaImagen.ToString());
            ImagenesInvisibles.Clear();*/
            cliente();
    }

        public async void cliente()
        {
            pboxChef.Invoke(new MethodInvoker(delegate
            {
                pboxChef.Hide();
            }));
            pboxCliente.Invoke(new MethodInvoker(delegate
            {
                pboxCliente.Show();
            }));
            Random rdn = new Random();
            GlobalData.numberOfActionsClient = rdn.Next(1, GlobalData.numberOfActionsChef);
            MessageBox.Show(GlobalData.numberOfActionsClient.ToString());

            for (int i = 0; i < GlobalData.numberOfActionsClient; i++)
            {
                if (myqcliente.Count == 0)
                {
                    llenarCola2();
                }
                PictureBox ver = myqcliente.Dequeue();
                ver.Invoke(new MethodInvoker(delegate
                {
                    ver.Hide();
                }));
                await Task.Delay(1000);
            }

            /*
            List<PictureBox> mesasHechas = new List<PictureBox>();
            for (int i = 0; i < Tables.Length; i++)
            {
                if (Tables[i].Visible == true)
                {
                    mesasHechas.Add(Tables[i]);
                }
            }
            for (int i = 0; i < GlobalData.numberOfActionsClient; i++)
            {
                mesasHechas[i].Invoke(new MethodInvoker(delegate
                {
                    mesasHechas[i].Hide();
                }));
                await Task.Delay(1000);
            }
            mesasHechas.Clear();*/
            productor();
        }


        public async void llenarCola()
        {
            myq.Enqueue(pboxTable1);
            myq.Enqueue(pboxTable2);
            myq.Enqueue(pboxTable3);
            myq.Enqueue(pboxTable4);
            myq.Enqueue(pboxTable5);
            myq.Enqueue(pboxTable6);
            myq.Enqueue(pboxTable7);
            myq.Enqueue(pboxTable8);
        }

        public async void llenarCola2()
        {
            myqcliente.Enqueue(pboxTable1);
            myqcliente.Enqueue(pboxTable2);
            myqcliente.Enqueue(pboxTable3);
            myqcliente.Enqueue(pboxTable4);
            myqcliente.Enqueue(pboxTable5);
            myqcliente.Enqueue(pboxTable6);
            myqcliente.Enqueue(pboxTable7);
            myqcliente.Enqueue(pboxTable8);
        }

        private void button_Click(object sender, EventArgs e)
        {
            Thread productorHilo = new Thread(productor);
            productorHilo.Start();
        }
    }
}
