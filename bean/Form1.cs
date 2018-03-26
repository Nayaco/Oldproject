using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public class wall
        {
            public static System.IntPtr handle;
            public wall()
            {  
            }
            public void draw(int[,] map)
            {
                Graphics gr = Graphics.FromHwnd(handle);
                for (int y = 1; y <= 20; y++)
                {
                    for (int x = 1; x <= 30; x++)
                    {
                        if (map[x, y] == 1)
                        {
                            Rectangle rct = new Rectangle(30 * x - 30, 30 * y - 30, 30, 30);
                            gr.FillRectangle(new SolidBrush(Color.RoyalBlue), rct);
                        }
                    }
                }
            }
        }

        public class bean
        {
            public int x,y;
            public static Bitmap btm;
            public static SolidBrush bc;
            public static System.IntPtr handle;
            public bean()
            {
                x = 1;
                y = 1;
            }
            public void draw()
            {
                Graphics gr = Graphics.FromHwnd(handle);
                Rectangle rct = new Rectangle(30*x-30, 30*y-30,30,30);
                gr.DrawImage(btm,rct);
            }
            public void eras()
            {
                Graphics gr = Graphics.FromHwnd(handle);
                Rectangle rct = new Rectangle(30 * x-30, 30 * y-30, 30, 30);
                gr.FillRectangle(bc, rct);
            }
            public void up()
            {
                eras();
                y--;
                draw();
            }
            public void down()
            {
                eras();
                y++;
                draw();                     
            }
            public  void left()
            {
                eras();
                x--;
                draw();
            }
            public void right()
            {
                eras();
                x++;
                draw();
                
            }
            public void reset()
            {
                x = 1;
                y = 1;
            }
        }
        
        public string path;
        public bean bb;
        public string[] map;
        Boolean Isstart;

        private void drawwall()
            {
                map = new string[31];
                FileStream fl = new FileStream(path+@"\data\map.mp", FileMode.OpenOrCreate);
                StreamReader re = new StreamReader(fl);
                for (int i = 1; i < 21; i++)
                {
                    map[i] = re.ReadLine();
                }
                int k;
                Graphics gra = this.CreateGraphics();
                for (int i = 1; i < 21; i++)
                {
                    for (int j = 1; j < 31; j++)
                    {
                        k = Convert.ToInt32(map[i][j]);
                        if (k == 49)
                        {
                            Rectangle rct = new Rectangle(30 * j - 30, 30 * i - 30, 30, 30);
                            SolidBrush brash = new SolidBrush(Color.RoyalBlue);
                            gra.FillRectangle(brash, rct);
                        }
                        else
                        {
                            if (k == 50)
                            {
                                Rectangle rct = new Rectangle(30 * j - 30, 30 * i - 30, 30, 30);
                                SolidBrush brash = new SolidBrush(Color.Yellow);
                                gra.FillRectangle(brash, rct);
                            }
                        }
                    }
                }
                fl.Close();
                re.Close();
            }

        private void Form1_Load(object sender, EventArgs e)
            {
                path = Environment.CurrentDirectory;
                bean.handle = this.Handle;
                bean.bc = new SolidBrush(this.BackColor);
                string be = path+@"\data\btm.png";
                bean.btm = new Bitmap(be);
                bb=new bean();
            }
        private Boolean Check()
        {
            if (Convert.ToInt32(map[(bb.y)][bb.x]) == 50)
            {
                return true;
            }
            return false;
        }
        private void Auto()
        {
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        {
                            drawwall();
                            bb.reset();
                            bb.draw();
                            Isstart = true;
                            break;
                        }
                    case Keys.Down:
                        {
                            if (Isstart)
                            {
                                if (bb.y < 20)
                                {
                                    if (Convert.ToInt32(map[(bb.y + 1)][bb.x]) != 49)
                                    {
                                        bb.down();
                                    }
                                }
                            }
                            if (Check())
                            {
                                Isstart = false;
                            }
                            break;
                        }
                    case Keys.Up:
                        {
                            if (Isstart)
                            {
                                if (bb.y > 1)
                                {
                                    if (Convert.ToInt32(map[(bb.y - 1)][bb.x]) != 49)
                                    {
                                        bb.up();
                                    }
                                }
                            }
                            if (Check())
                            {
                                Isstart = false;
                            }
                            break;
                         }
                    case Keys.Left:
                        {
                            if (Isstart)
                            {
                                if (bb.x > 1)
                                {
                                    if (Convert.ToInt32(map[bb.y][(bb.x - 1)]) != 49)
                                    {
                                        bb.left();
                                    }
                                }
                            }
                            if (Check())
                            {
                                Isstart = false;
                            }
                            break;
                        }
                    case Keys.Right:
                        {
                            if (Isstart)
                            {
                                if (bb.x < 30)
                                {
                                    if (Convert.ToInt32(map[bb.y][(bb.x + 1)]) != 49)
                                    {
                                        bb.right();
                                    }
                                }
                            }
                            if (Check())
                            {
                                Isstart = false;
                            }
                            break;
                        }
                    case Keys.Escape:
                        {
                            Application.Exit();
                            break;
                        }
                }
            }
        
    }
}
