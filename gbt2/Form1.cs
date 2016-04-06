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

            oFloor floor = new oFloor();
            floor.AddRoom(0, 0, pb.Patterns[0]);
            floor.AddRoom(1, 0, pb.Patterns[1]);
            floor.AddRoom(2, 0, pb.Patterns[2]);
            floor.AddRoom(0, 1, pb.Patterns[3]);

            ucRoom ucr = new ucRoom();
            ucr.Room = floor.Rooms[0];
            pnlRooms.Controls.Add(ucr);
            ucr.DrawRoom();

            ucRoom ucr2 = new ucRoom();
            ucr2.Room = floor.Rooms[1];
            ucr2.Left = 160;
            pnlRooms.Controls.Add(ucr2);
            ucr2.DrawRoom();

            ucRoom ucr3 = new ucRoom();
            ucr3.Room = floor.Rooms[2];
            ucr3.Left = 160 * 2;
            pnlRooms.Controls.Add(ucr3);
            ucr3.DrawRoom();

            ucRoom ucr4 = new ucRoom();
            ucr4.Room = floor.Rooms[3];
            ucr4.Top = 160;
            pnlRooms.Controls.Add(ucr4);
            ucr4.DrawRoom();

        }
    }
}
