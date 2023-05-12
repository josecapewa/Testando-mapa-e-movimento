using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mapa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeMap();
            ClientSize = new Size(mapsizex * BlockSize, mapsizey * BlockSize);
            Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            playerX = 1;
            playerY = 6;
        }
        private const int BlockSize = 30;
        private const int lx = 9;
        private const int ly = 15;
        private const int cx = 15;
        private const int cy = 9;
        private const int mapsizex = 15;
        private const int mapsizey = 15;
        private bool[,] mapA;
        private bool[,] mapB;
        private int playerX;
        private int playerY;
        private void InitializeMap()
        {
            mapA = new bool[lx, ly]
            {
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
                { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false},
                { false, false, false, false, false, false, false, false, false, false, false, false, false, true, false},
                { false, true, true, true, true, true, false, false, false, true, true, true, true, true, false},
                { false, true, false, false, false, false, false, false, false, false, false, false, false, false, false}
            };
            mapB = new bool[cx,cy]
            {
                { false, false, false, false, false, false, false, false, false},
                { false, false, false, false, false, false, true, true, false},
                { false, false, false, false, false, false, false, true, false},
                { false, false, false, false, false, false, false, true, false},
                { false, false, false, false, false, false, false, true, false},
                { false, false, false, false, false, false, false, true, false},
                { false, false, false, false, false, false, false, false, false},
                { false, false, false, false, false, false, false, false, false},
                { false, false, false, false, false, false, false, false, false},
                { false, false, false, false, false, false, false, true, false},
                { false, false, false, false, false, false, false, true, false},
                { false, false, false, false, false, false, false, true, false},
                { false, false, false, false, false, false, false, true, false},
                { false, false, false, false, false, false, false, true, true},
                { false, false, false, false, false, false, false, false, false}
            };
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;

            for (int x = 6; x < lx; x++)
            {
                for (int y = 0; y < ly; y++)
                {
                    bool isPath = mapA[x, y];
                    Brush brush = isPath ? Brushes.Green : Brushes.Gray;
                    Pen pen = isPath ? Pens.DarkGreen : Pens.DarkGray;

                    int xPos = x * BlockSize;
                    int yPos = y * BlockSize;

                    g.FillRectangle(brush, xPos, yPos, BlockSize, BlockSize);
                    g.DrawRectangle(pen, xPos, yPos, BlockSize, BlockSize);
                }
            }
            
            for (int x = 0; x < cx; x++)
            {
                for (int y = 6; y < cy; y++)
                {
                    bool isPath = mapB[x, y];
                    Brush brush = isPath ? Brushes.Green : Brushes.Gray;
                    Pen pen = isPath ? Pens.DarkGreen : Pens.DarkGray;

                    int xPos = x * BlockSize;
                    int yPos = y * BlockSize;

                    g.FillRectangle(brush, xPos, yPos, BlockSize, BlockSize);
                    g.DrawRectangle(pen, xPos, yPos, BlockSize, BlockSize);
                }
            }
            Brush playerBrush = Brushes.Red;
            Pen playerPen = Pens.DarkRed;
            int playerSize = BlockSize / 2;
            int playerOffset = (BlockSize - playerSize) / 2;
            int playerPosX = playerX * BlockSize + playerOffset;
            int playerPosY = playerY * BlockSize + playerOffset;

            g.FillEllipse(playerBrush, playerPosX, playerPosY, playerSize, playerSize);
            g.DrawEllipse(playerPen, playerPosX, playerPosY, playerSize, playerSize);
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            int newPlayerX = playerX;
            int newPlayerY = playerY;

            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (playerX > 0)
                    {
                        newPlayerX = --playerX;
                    }
                    break;
                case Keys.Right:
                    if (playerX < cx - 1)
                    {
                        newPlayerX = ++playerX;
                    }
                    break;
                case Keys.Up:
                    if (playerY > 6 || playerX>5)
                    {
                        newPlayerY = --playerY;
                    }
                    break;
                case Keys.Down:
                    if (playerY < cy - 1)
                    {
                        newPlayerY = ++playerY;
                    }
                    break;
            }

            playerX = newPlayerX; 
            playerY = newPlayerY; 

            Invalidate();
        }
    }
}