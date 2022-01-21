using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisGame
{
    public partial class Form1 : Form
    {
        figure curFigure; 
        map map = new map();

        int score;
        int interval_value;

        public Form1()
        {
            InitializeComponent();
            Invalidate();
            interval_value = 350;
            timer1.Enabled = false;
            timer1.Interval = 500;
            curFigure = new figure(map.width/2 - 1, 0);
            score = 0;
            score_label.Text = "Score: " + score;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            reset_Area();
            if (!vertical_Collision())
                curFigure.move_down();
            else
            {
                timer1.Interval = interval_value;
                score += 10;
                Merge();
                burn_Line();
                //curFigure = new figure(map.width/2 - 1, 0);
                curFigure.get_nextMatrix(map.width / 2 - 1, 0);

                if (vertical_Collision())
                {
                    for (int i = 0; i < map.height; i++)
                        for (int j = 0; j < map.width; j++)
                            map.field[i, j] = 0;
                    score = 0;
                    score_label.Text = "Score: " + score;
                }
            }
            Merge();
            Invalidate();
        }

        public void draw_Figure(Graphics e)
        {
            for (int i = 0; i < map.height; i++)
                for (int j = 0; j < map.width; j++)
                    if (map.field[i, j] == 1)
                        e.FillRectangle(Brushes.Purple, new Rectangle(20 + j * map.cellSize, 20 + i * map.cellSize, map.cellSize, map.cellSize));
        }


        public void Merge()
        {
            for (int i = curFigure.y; i < curFigure.y + curFigure.matrixSize; i++)
                for (int j = curFigure.x; j < curFigure.x + curFigure.matrixSize; j++)
                    if (curFigure.matrix[i - curFigure.y, j - curFigure.x] != 0)
                        map.field[i, j] = curFigure.matrix[i - curFigure.y, j - curFigure.x];
        }

        public void reset_Area()
        {
            for (int i = curFigure.y; i < curFigure.y + curFigure.matrixSize; i++)
                for (int j = curFigure.x; j < curFigure.x + curFigure.matrixSize; j++)
                    if (curFigure.matrix[i - curFigure.y, j - curFigure.x] != 0) 
                        map.field[i, j] = 0;
        }


        public bool vertical_Collision()
        {
            for (int i = curFigure.y + curFigure.matrixSize-1; i >= curFigure.y; i--)
                for (int j = curFigure.x; j < curFigure.x + curFigure.matrixSize; j++)
                    if (curFigure.matrix[i - curFigure.y, j - curFigure.x] != 0)
                        if (i + 1 == map.height || map.field[i + 1, j] != 0)
                            return true;

            return false;
        }

        public bool horizontal_Collision(string direction)
        {
            int temp = 0;
            if (direction == "Left")
                temp = -1;
            if (direction == "Right")
                temp = 1;

            for (int i = curFigure.y; i < curFigure.y + curFigure.matrixSize; i++)
                for (int j = curFigure.x; j < curFigure.x + curFigure.matrixSize; j++)
                    if (curFigure.matrix[i - curFigure.y, j - curFigure.x] != 0)
                    {
                        if (j + 1 * temp > map.width - 1 || j + 1 * temp < 0)
                            return true;

                        if (map.field[i, j + 1 * temp] != 0)
                        {
                            if (j - curFigure.x + 1 * temp >= curFigure.matrixSize || j - curFigure.x + 1 * temp < 0)
                                return true;
                            if (curFigure.matrix[i - curFigure.y, j - curFigure.x + 1 * temp] == 0)
                                return true;
                        }
                    }
            return false;
        }

        public bool rotation_overlap()
        {
            for (int i = curFigure.y; i < curFigure.y + curFigure.matrixSize; i++)
                for (int j = curFigure.x; j < curFigure.x + curFigure.matrixSize; j++)
                    if (j >= 0 && j <= map.width - 1)
                        if (map.field[i, j] != 0 && curFigure.matrix[i - curFigure.y, j - curFigure.x] == 0)
                            return true;
            return false;
        }

        public void burn_Line()
        {
            int count = 0;
            for (int i = 0; i < map.height; i++)
            {
                count = 0;
                for (int j = 0; j < map.width; j++)
                    if (map.field[i, j] != 0)
                        count++;
                if (count == map.width)
                {
                    score += 100;
                    for (int k = i; k >= 1; k--)
                        for (int o = 0; o < map.width; o++)
                            map.field[k, o] = map.field[k - 1, o];
                }
            }

            score_label.Text = "Score: " + score;
        }


        public void draw_nextFigureWindow(Graphics g)
        {
            for (int i = 0; i <= 4; i++)
                g.DrawLine(Pens.Gray, new Point(305 + i * map.cellSize, 105), new Point(305 + i * map.cellSize, 105 + 4 * map.cellSize));

            for (int i = 0; i <= 4; i++)
                g.DrawLine(Pens.Gray, new Point(305, 105 + i * map.cellSize), new Point(305 + 4 * map.cellSize, 105 + i * map.cellSize));
        }

        public void draw_nextFigure(Graphics g)
        {
            for (int i = 0; i < curFigure.nextMatrix.GetLength(1); i++)
                for (int j = 0; j < curFigure.nextMatrix.GetLength(1); j++)
                    if (curFigure.nextMatrix[i, j] == 1)
                        g.FillRectangle(Brushes.Purple, new Rectangle(305 + j * map.cellSize, 105 + i * map.cellSize, map.cellSize, map.cellSize));
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            draw_Figure(e.Graphics);
            map.draw_Map(e.Graphics);
            draw_nextFigure(e.Graphics);
            draw_nextFigureWindow(e.Graphics);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    if (timer1.Enabled == true)
                    {
                        timer1.Enabled = false;
                        label1.Text = "Press SPACE to start";
                    }
                    else
                    {
                        timer1.Enabled = true;
                        label1.Text = "Press SPACE to stop";
                    }
                    break;

                case Keys.Down:
                    timer1.Interval = 40;
                    break;

                case Keys.Right:
                    if (timer1.Enabled == true)
                        if (!horizontal_Collision("Right"))
                        {
                            reset_Area();
                            curFigure.move_right();
                            Merge();
                            Invalidate();
                        }
                    break;

                case Keys.Left:
                    if (timer1.Enabled == true)
                        if (!horizontal_Collision("Left"))
                        {
                            reset_Area();
                            curFigure.move_left();
                            Merge();
                            Invalidate();
                        }
                    break;

                case Keys.Up:
                    if (timer1.Enabled == true)
                        if (!rotation_overlap())
                        {
                            reset_Area();
                            curFigure.figure_Rotation();
                            Merge();
                            Invalidate();
                        }
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            timer1.Interval = interval_value;
        }
    }
}
