using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace TetrisGame
{
    class figure
    {
        public map map;

        public int x;
        public int y;
        public int[,] matrix;
        public int[,] nextMatrix;
        public int matrixSize;

        public figure(int _x, int _y)
        {
            map = new map();

            x = _x;
            y = _y;

            matrix = generate_Figure();
            matrixSize = matrix.GetLength(1);

            nextMatrix = generate_Figure();
        }

        public void get_nextMatrix(int _x, int _y)
        {
            map = new map();

            x = _x;
            y = _y;

            matrix = nextMatrix;
            matrixSize = matrix.GetLength(1);
            nextMatrix = generate_Figure();
        }

        public int[,] figure_1 = new int[2, 2]
        {
            { 1, 1 },
            { 1, 1 },
        };
        public int[,] figure_2 = new int[3, 3]
        {
            { 0, 1, 0 },
            { 0, 1, 1 },
            { 0, 1, 0 }
        };
        public int[,] figure_3 = new int[3, 3]
        {
            { 0, 1, 0 },
            { 1, 1, 0 },
            { 1, 0, 0 }
        };
        public int[,] figure_4 = new int[3, 3]
        {
            { 1, 0, 0 },
            { 1, 1, 0 },
            { 0, 1, 0 }
        };
        public int[,] figure_5 = new int[3, 3]
        {
            { 0, 1, 1 },
            { 0, 1, 0 },
            { 0, 1, 0 }
        };
        public int[,] figure_6 = new int[3, 3]
        {
            { 1, 1, 0 },
            { 0, 1, 0 },
            { 0, 1, 0 }
        };
        public int[,] figure_7 = new int[4, 4]
        {
            { 0, 0, 1, 0 },
            { 0, 0, 1, 0 },
            { 0, 0, 1, 0 },
            { 0, 0, 1, 0 }
        };

        public int[,] generate_Figure()
        {
            int[,] temp = figure_1;
            Random r = new Random();
            switch (r.Next(1, 8))
            {
                case 1:
                    temp = figure_1;
                    //matrixSize = 2;
                    break;

                case 2:
                    temp = figure_2;
                    //matrixSize = 3;
                    break;

                case 3:
                    temp = figure_3;
                    //matrixSize = 3;
                    break;

                case 4:
                    temp = figure_4;
                    //matrixSize = 3;
                    break;

                case 5:
                    temp = figure_5;
                    //matrixSize = 3;
                    break;

                case 6:
                    temp = figure_6;
                    //matrixSize = 3;
                    break;

                case 7:
                    temp = figure_7;
                    //matrixSize = 4;
                    break;

            }
            return temp;
        }

        public void figure_Rotation()
        {
            int[,] tempMatrix = new int[matrixSize, matrixSize];

            for (int i = 0; i < matrixSize; i++)
                for (int j = 0; j < matrixSize; j++)
                    tempMatrix[i, j] = matrix[j, (matrixSize - 1) - i];

            //

            int offset1 = (map.width - (x + this.matrixSize));
            if (offset1 < 0)
            {
                for (int i = 0; i < Math.Abs(offset1); i++)
                    move_left();
            }

            if (x < 0)
                for (int i = 0; i < Math.Abs(x) + 1; i++)
                    move_right();

            //

            matrix = tempMatrix;
        }

        public void move_down()
        {
                y++;
        }

        public void move_right()
        {
                x++;
        }

        public void move_left()
        {
                x--;
        }
    }
}
