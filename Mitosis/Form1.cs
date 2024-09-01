using System.Diagnostics;
using System.Numerics;

namespace Mitosis
{
    public partial class Form1 : Form
    {
        Random rand = new Random();

        Graphics g;
        Graphics gF;
        Bitmap btm;
        SolidBrush brush;

        private readonly List<Cell> cells = [];
        private Vector2 initalCellPosition;
        private float initalCellRadius = 200f;

        bool isDrawing = false;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        //Draw
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            btm = new Bitmap(Width, Height);
            gF = Graphics.FromImage(btm);

            if (cells.Count == 0)
            {
                initalCellPosition = new Vector2(Width / 2, Height / 2);
                brush = new(Color.FromArgb(255, rand.Next(100, 255), 0, rand.Next(100, 255)));
                cells.Add(new Cell(initalCellPosition, initalCellRadius));
            }

            for (int i = 0; i < cells.Count; i++)
            {
                var cell = cells[i];
                cell.UpdatePosition();
                gF.FillEllipse(brush, cell.Position.X, cell.Position.Y, cell.Radius, cell.Radius);
            }

            g.DrawImage(btm, 0, 0);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //Check if mouse click is in within the radius of any cell
            Point p2 = PointToClient(Cursor.Position);
            Vector2 cursorVector2 = new Vector2(p2.X, p2.Y);
            for (int i = 0; i < cells.Count; i++)
            {
                if(Vector2.Distance(cursorVector2, cells[i].Position) < cells[i].Radius)
                {
                    CellClicked(cells[i]);
                    break;
                }
            }
        }

        private void TimerEvent(object sender, EventArgs e)
        {
            Invalidate();
        }

        //Create new cell
        private void CellClicked(Cell cell)
        {
            Cell oldCell = cell;
            Cell newCell = new Cell(oldCell.Position, oldCell.Radius / 1.8f);
            oldCell.Radius = oldCell.Radius / 1.8f;
            cells.Add(newCell);

            oldCell.CellSplitMovement(true);
            newCell.CellSplitMovement(false);
        }
    }
}
