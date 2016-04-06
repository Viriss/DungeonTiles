using System;
using System.IO;
using System.Windows.Forms;

using Newtonsoft.Json;

namespace gbt2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RoomPattern rp = new RoomPattern();
            rp.Name = "Cellar";
            rp.Exits = Direction.North;
            rp.Pattern = @"-----
----
----
  --
  --";

            string json = "";
            json = JsonConvert.SerializeObject(rp);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = "";
            PatternBook pb;
            StreamReader sr = new StreamReader("App_Data\\RoomPatterns.txt");

            while (!sr.EndOfStream)
            {
                text += sr.ReadLine();
            }
            sr.Close();
            sr.Dispose();

            pb = JsonConvert.DeserializeObject<PatternBook>(text);

            //oFloor floor = new oFloor();
            Engine.floor.AddRoom(0, 0, pb.Patterns[0]);
            Engine.floor.AddRoom(1, 0, pb.Patterns[1]);
            Engine.floor.AddRoom(2, 0, pb.Patterns[2]);
            Engine.floor.AddRoom(0, 1, pb.Patterns[4]);
            Engine.floor.AddRoom(1, 1, pb.Patterns[3]);

            ucRoom ucr = new ucRoom();
            ucr.SetRoom(0);
            pnlRooms.Controls.Add(ucr);
            ucr.DrawRoom();

            ucRoom ucr2 = new ucRoom();
            ucr2.SetRoom(1);
            ucr2.Left = 160;
            pnlRooms.Controls.Add(ucr2);
            ucr2.DrawRoom();

            ucRoom ucr3 = new ucRoom();
            ucr3.SetRoom(2);
            ucr3.Left = 160 * 2;
            pnlRooms.Controls.Add(ucr3);
            ucr3.DrawRoom();

            ucRoom ucr4 = new ucRoom();
            ucr4.SetRoom(3);
            ucr4.Top = 160;
            pnlRooms.Controls.Add(ucr4);
            ucr4.DrawRoom();


            ucRoom ucr5 = new ucRoom();
            ucr5.SetRoom(4);
            ucr5.Left = 160 * 1;
            ucr5.Top = 160 * 1;
            pnlRooms.Controls.Add(ucr5);
            ucr5.DrawRoom();

            ucr.SetupRoom();
            ucr2.SetupRoom();
            ucr3.SetupRoom();
            ucr4.SetupRoom();
            ucr5.SetupRoom();

        }
    }
}
