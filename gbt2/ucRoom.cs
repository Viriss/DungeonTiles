using System.Drawing;
using System.Windows.Forms;

namespace gbt2
{
    public partial class ucRoom : UserControl
    {
        public oRoom Room;

        public ucRoom()
        {
            InitializeComponent();
        }

        public void DrawRoom()
        {
            foreach (oTile t in Room.Tiles)
            {
                UpdateLabel(t);
            }
        }
        private void UpdateLabel(oTile t)
        {
            Label lbl = (Label)this.Controls["label" + (t.ID + 1)];

            Image img = new Bitmap(32, 32);
            Graphics g = Graphics.FromImage(img);

            //tile type
            switch (t.Type)
            {
                case TileType.grass:
                    g.FillRectangle(Brushes.ForestGreen, new Rectangle(0, 0, 32, 32));
                    break;
                case TileType.lava:
                    g.FillRectangle(Brushes.Firebrick, new Rectangle(0, 0, 32, 32));
                    break;
                case TileType.stone:
                    g.FillRectangle(Brushes.DarkSlateGray, new Rectangle(0, 0, 32, 32));
                    break;
                case TileType.water:
                    g.FillRectangle(Brushes.DeepSkyBlue, new Rectangle(0, 0, 32, 32));
                    break;
                case TileType.wood:
                    g.FillRectangle(Brushes.SaddleBrown, new Rectangle(0, 0, 32, 32));
                    break;
            }

            //thin border
            g.DrawRectangle(new Pen(Color.FromArgb(70, Color.Black)), new Rectangle(0, 0, 32, 32));


            //walls
            if ((t.Walls & Direction.North) == Direction.North)
            {
                g.FillRectangle(Brushes.Black, new Rectangle(0, 0, 32, 4));
            }
            if ((t.Walls & Direction.South) == Direction.South)
            {
                g.FillRectangle(Brushes.Black, new Rectangle(0, 28, 32, 32));
            }
            if ((t.Walls & Direction.East) == Direction.East)
            {
                g.FillRectangle(Brushes.Black, new Rectangle(28, 0, 32, 32));
            }
            if ((t.Walls & Direction.West) == Direction.West)
            {
                g.FillRectangle(Brushes.Black, new Rectangle(0, 0, 4, 32));
            }


            //corners
            if ((t.Walls & Direction.corner_NE) == Direction.corner_NE)
            {
                g.FillRectangle(Brushes.Black, new Rectangle(28, 0, 32, 4));
            }
            if ((t.Walls & Direction.corner_SE) == Direction.corner_SE)
            {
                g.FillRectangle(Brushes.Black, new Rectangle(28, 28, 32, 32));
            }
            if ((t.Walls & Direction.corner_SW) == Direction.corner_SW)
            {
                g.FillRectangle(Brushes.Black, new Rectangle(0, 28, 4, 32));
            }
            if ((t.Walls & Direction.corner_NW) == Direction.corner_NW)
            {
                g.FillRectangle(Brushes.Black, new Rectangle(0, 0, 4, 4));
            }

            lbl.Image = img;
        }
    }
}
