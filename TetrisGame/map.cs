using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisGame
{
    class map
    {
        Graphics g;

        public int[,] field;
        public static int height;
        public static int width;
        public int cellSize;

        public map()
        {
            height = 20;
            width = 10;
            cellSize = 20;
            field = new int[height, width];
        }

        public void draw_Map(Graphics g)
        {
            for (int i = 0; i <= map.width; i++)
                g.DrawLine(Pens.Gray, new Point(20 + i * this.cellSize, 20), new Point(20 + i * this.cellSize, 20 + height * this.cellSize));

            for (int i = 0; i <= map.height; i++)
                g.DrawLine(Pens.Gray, new Point(20, 20 + i * this.cellSize), new Point(20 + width * this.cellSize, 20 + i * this.cellSize));
        }

    }
}
