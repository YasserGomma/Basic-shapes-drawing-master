using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Basic_shapes_drawing_master
{
    public partial class MainForm : Form
    {
        public DataTable DDAtable = new DataTable();
        public DataTable brestable = new DataTable();
        public DataTable circletable = new DataTable();
        public DataTable ellipstable = new DataTable();
        int DDAX1 = 0, DDAX2 = 0, DDAY1 = 0, DDAY2 = 0, X, Y;
        bool draw = false, newone = false;
        int dir = 1, dir2 = -1;
        List<List<int>> DATA = new List<List<int>>();
        public MainForm()
        {
            InitializeComponent();
          
        }
       public void ddainit()
        {
            DDAtable.Columns.Add("k");
            DDAtable.Columns.Add("x old");
            DDAtable.Columns.Add("x_new =x+x_inc");
            DDAtable.Columns.Add("y old");
            DDAtable.Columns.Add("y new y+y_inc");
            DDAtable.Columns.Add("(x,y)");
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.Blue;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.Font = new Font("arial", 15);
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
        public void bresinit()
        {
            brestable.Columns.Add("k");
            brestable.Columns.Add("Pk");
            brestable.Columns.Add("x old");
            brestable.Columns.Add("x_new =x+x_inc");
            brestable.Columns.Add("y old");
            brestable.Columns.Add("y new y+y_inc");
            brestable.Columns.Add("(x,y)");
            dataGridView2.BorderStyle = BorderStyle.None;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.Blue;
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView2.Font = new Font("arial", 15);
            dataGridView2.EnableHeadersVisualStyles = false;
            dataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
        public void circleinit()
        {
            circletable.Columns.Add("k");
            circletable.Columns.Add("Pk");
            circletable.Columns.Add("x old");
            circletable.Columns.Add("y old");
            circletable.Columns.Add("x new");
            circletable.Columns.Add("y new");
            circletable.Columns.Add("(x,y)");
            dataGridView3.BorderStyle = BorderStyle.None;
            dataGridView3.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView3.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView3.DefaultCellStyle.SelectionBackColor = Color.Blue;
            dataGridView3.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView3.Font = new Font("arial", 15);
            dataGridView3.EnableHeadersVisualStyles = false;
            dataGridView3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
         public void ellipseinit()
        {
            ellipstable.Columns.Add("k");
            ellipstable.Columns.Add("P");
            ellipstable.Columns.Add("(x,y)");
            ellipstable.Columns.Add("2*ry^2*x");
            ellipstable.Columns.Add("2*rx^2*y");
            dataGridView4.BorderStyle = BorderStyle.None;
            dataGridView4.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView4.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView4.DefaultCellStyle.SelectionBackColor = Color.Blue;
            dataGridView4.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView4.Font = new Font("arial", 15);
            dataGridView4.EnableHeadersVisualStyles = false;
            dataGridView4.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView4.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dataGridView4.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
       
      

        
        //MATRIXES FUNCTION
        double[,] matrix33_X_matrix33(double[,] arr1, double[,] arr2)
        {
            double[,] res = new double[3, 3];

            for (int i = 0; i < 9; i++)
            {

                for (int col = 0; col < 3; col++)
                {
                    res[i / 3, i % 3] += arr1[i / 3, col] * arr2[col, i % 3];
                }

            }

            return res;
        }
        double[] matrix33_X_matrix31(double[,] arr1, double[] arr2)
        {
            double[] res = new double[3];

            for (int i = 0; i < 9; i++)
            {
                res[i / 3] += arr1[i / 3, i % 3] * arr2[i % 3];
            }

            return res;
        }

        double[,] tras_to_zero(double x, double y)
        {
            double[,] trans_to_zero ={
                                 {1,0,-1*x},
                                 {0,1,-1*y},
                                 {0,0,1}
                                  };
            return trans_to_zero;
        }

        // pixel drawing 
        public void draw_point(int x, int y, PictureBox ppp)
        {
            try
            {
                if (x < 0 || x > ppp.Width || y < 0 || y > ppp.Height)
                {
                    return;
                }
                ((Bitmap)ppp.Image).SetPixel(x, y, Color.Red);
            }
            catch (Exception es) { }
        }

        //draw 8
        void draw_8(int x, int y, int xc, int yc)
        {
            draw_point(xc + x, yc + y, pictureBox3);
            draw_point(xc - x, yc + y, pictureBox3);
            draw_point(xc + x, yc - y, pictureBox3);
            draw_point(xc - x, yc - y, pictureBox3);

            draw_point(xc + y, yc + x, pictureBox3);
            draw_point(xc - y, yc + x, pictureBox3);
            draw_point(xc + y, yc - x, pictureBox3);
            draw_point(xc - y, yc - x, pictureBox3);

        }
        //DDA Algorithm
        void lineDDA(float xb, float yb, float xe, float ye)
        {
            ddainit();
            DDAtable.Clear();
            float dx = xe - xb;
            float dy = ye - yb;
            float steps = 0;
            if (Math.Abs(dx) > Math.Abs(dy)) steps = Math.Abs(dx);
            else steps = Math.Abs(dy);
            float xinc = dx / steps;
            float yinc = dy / steps;
            float x = xb, y = yb;
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            for (int i = 0; i < steps; i++)
            {
                DataRow r = DDAtable.NewRow();
                r[0] = i + 1;
                r[1] = x;
                r[3] = y;
                int xx = (int)Math.Round(x);
                int yy = (int)Math.Round(y);
                draw_point(xx, yy, pictureBox1);
                x += xinc;
                y += yinc;
                r[2] = x;
                r[4] = y;
                string f = "(" + ((int)Math.Round(x)).ToString() + "," + ((int)Math.Round(y)).ToString() + ")";
                r[5] = f;
                DDAtable.Rows.Add(r);
                dataGridView1.DataSource = DDAtable;
            }
        }
        //BERSENHAM FUNCTION
        void Bresenham(int xa, int ya, int xb, int yb)
        {
            DDAX1 = xa; DDAX2 = xb; DDAY1 = ya; DDAY2 = yb;
            bresinit();
            brestable.Clear();
            pictureBox2.Image = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            int dx = Math.Abs(xa - xb);
            int dy = Math.Abs(ya - yb);
            int p = 2 * dy - dx;
            int twody = 2 * dy, twodydx = 2 * (dy - dx);
            int x, y, xend;
            if (xa > xb)
            {
                x = xb;
                y = yb;
                xend = xa;
            }
            else
            {
                x = xa;
                y = ya;
                xend = xb;
            }
            DataRow strt = brestable.NewRow();
            draw_point(x, y, pictureBox2);
            strt[0] = 1;
            strt[1] = "---";
            strt[2] = "---";
            strt[3] = x;
            strt[4] = "---";
            strt[5] = y;
            strt[6] = ("(" + x.ToString() + "," + y.ToString() + ")");
            brestable.Rows.Add(strt);
            dataGridView2.DataSource = brestable;
            int pk = p, i = 2;
            while (x < xend)
            {
                DataRow r = brestable.NewRow();
                r[0] = i;
                r[1] = pk;
                r[2] = x;
                x++;
                r[3] = x;
                r[4] = y;
                if (p < 0)
                    p += twody;
                else
                {
                    y++;
                    p += twodydx;
                }
                r[5] = y;
                r[6] = ("(" + x.ToString() + "," + y.ToString() + ")");
                draw_point(x, y, pictureBox2);
                brestable.Rows.Add(r);
                dataGridView2.DataSource = brestable;
                pk = p;
                i++;

            }
        }
        //DRAW CIRCEL
        void cyrcle(int x0, int y0, int r)
        {
            circleinit();
            circletable.Clear();
            pictureBox3.Image = new Bitmap(pictureBox3.Width, pictureBox3.Height);
            DATA.Clear();

            int p = 1 - r;
            int x = r;
            int y = 0;
            int kk = 1;

            while (x >= y)
            {
                DataRow rk = circletable.NewRow();

                rk[0] = kk;
                kk++;
                rk[1] = p;

                rk[2] = x;
                rk[3] = y;
                draw_8(x, y, x0, y0);

                rk[4] = (x0 + x).ToString();
                rk[5] = (y0 + y).ToString();
                if (p <= 0)
                {
                    p = p + 2 * (y + 1);
                }
                else
                {
                    p = p + 2 * (y + 1) - 2 * (x - 1);
                    x--;
                }
                y++;
                string g = "(" + x.ToString() + "," + y.ToString() + ")";
                rk[6] = g;

                circletable.Rows.Add(rk);

            }
            dataGridView3.DataSource = circletable;
        }
        void elepse4(int x, int y, int xcenter, int ycenter, PictureBox ppp, int d, int i)
        {
            draw_point(xcenter + x, ycenter + y, ppp);
            draw_point(xcenter - x, ycenter + y, ppp);
            draw_point(xcenter + x, ycenter - y, ppp);
            draw_point(xcenter - x, ycenter - y, ppp);
            List<int> test = new List<int>();
            test.Add(i);
            test.Add(xcenter + x);
            test.Add(ycenter + y);
            test.Add(d);
            DATA.Add(test);

            List<int> test1 = new List<int>();

            test1.Add(i);
            test1.Add(xcenter - x);
            test1.Add(ycenter + y);
            test1.Add(d);
            DATA.Add(test1);

            List<int> test2 = new List<int>();

            test2.Add(i);
            test2.Add(xcenter + x);
            test2.Add(ycenter - y);
            test2.Add(d);
            DATA.Add(test2);

            List<int> test3 = new List<int>();

            test3.Add(i);
            test3.Add(xcenter - x);
            test3.Add(ycenter - y);
            test3.Add(d);
            DATA.Add(test3);

        }
        //ELLIPs FUNCTION
        void elipse(int xc, int yc, int rx, int ry)
        {
            ellipseinit();
            ellipstable.Clear();
            int a = rx;
            int b = ry;

            int d = b * b - a * a * b + a * a / 4;
            int x = 0;
            int y = b;
            int xcenter = xc;
            int ycenter = yc;
            pictureBox4.Image = new Bitmap(pictureBox4.Width, pictureBox4.Height);
            int i = 0;
            DATA.Clear();

            while (2 * b * b * x < 2 * a * a * y)
            {
                elepse4(x, y, xcenter, ycenter, pictureBox4, d, i);
                DataRow r = ellipstable.NewRow();
                r[0] = i + 1;
                r[1] = d;
                string s = "(" + (xc + x).ToString() + "," + (yc + y).ToString() + ")";
                r[2] = s;

                r[3] = 2 * b * b * x;
                r[4] = 2 * a * a * y;
                i++;
                x++;
                if (d < 0)
                {
                    d = d + 2 * b * b * x + b * b;

                }
                else
                {
                    y--;
                    d = d + 2 * b * b * x + b * b - 2 * a * a * y;
                }
                ellipstable.Rows.Add(r);
            }

            while (y > 0)
            {
                DataRow r = ellipstable.NewRow();
                r[0] = i + 1;
                r[1] = d;
                string s = "(" + (xc + x).ToString() + "," + (yc + y).ToString() + ")";
                r[2] = s;

                r[3] = 2 * b * b * x;
                r[4] = 2 * a * a * y;
                y--;
                if (d < 0)
                {
                    d = d + 2 * b * b * x - 2 * a * a * y + a * a;
                    x++;
                }
                else
                {
                    d = d - 2 * a * a * y + a * a;
                }
                elepse4(x, y, xcenter, ycenter, pictureBox4, d, i);
                i++;
                ellipstable.Rows.Add(r);

            }
            dataGridView4.DataSource = ellipstable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DDAtable.Columns.Clear();
            DDAtable.Rows.Clear();
            try
            {
                float x1 = float.Parse(textBox1.Text);
                float x2 = float.Parse(textBox2.Text);
                float y1 = float.Parse(textBox4.Text);
                float y2 = float.Parse(textBox3.Text);
                DDAX1 = (int)x1; DDAX2 = (int)x2; DDAY1 = (int)y1; DDAY2 = (int)y2;
                lineDDA(x1, y1, x2, y2);
            }
            catch (Exception ez) { }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DDAtable.Columns.Clear();
            DDAtable.Rows.Clear();
            double ceta = double.Parse(textBox5.Text) * Math.PI / 180;
            double[,] rotate ={
                      {Math.Cos(ceta),Math.Sin(ceta)*dir,0},
                      {Math.Sin(ceta)*dir2,Math.Cos(ceta),0},
                      {0,0,1}
                      };
            double[,] p1_zer0 = tras_to_zero((DDAX1 + DDAX2) / 2, (DDAY1 + DDAY2) / 2);

            double newddx1 = DDAX1 + p1_zer0[0, 2];
            double newddx2 = DDAX2 + p1_zer0[0, 2];

            double newddy1 = DDAY1 + p1_zer0[1, 2];
            double newddy2 = DDAY2 + p1_zer0[1, 2];

            double[,] come_back = tras_to_zero((DDAX1 + DDAX2) / -2, (DDAY1 + DDAY2) / -2);

            double[,] test = matrix33_X_matrix33(come_back, rotate);
            double[,] res = matrix33_X_matrix33(test, p1_zer0);

            double[] point_1_before = { DDAX1, DDAY1, 1 };
            double[] point_2_before = { DDAX2, DDAY2, 1 };

            double[] point_1_after_rotate = matrix33_X_matrix31(res, point_1_before);
            double[] point_2_after_rotate = matrix33_X_matrix31(res, point_2_before);

            lineDDA((int)point_1_after_rotate[0], (int)point_1_after_rotate[1], (int)point_2_after_rotate[0], (int)point_2_after_rotate[1]);

            DDAX1 = (int)point_1_after_rotate[0];
            DDAY1 = (int)point_1_after_rotate[1];
            DDAX2 = (int)point_2_after_rotate[0];
            DDAY2 = (int)point_2_after_rotate[1];

        }

        private void button3_Click(object sender, EventArgs e)
        {

            int tx = int.Parse(textBox6.Text);
            int ty = int.Parse(textBox7.Text);
            DDAX1 += tx;
            DDAX2 += tx;
            DDAY1 += ty;
            DDAY2 += ty;
            DDAtable.Columns.Clear();
            DDAtable.Rows.Clear();

            lineDDA(DDAX1, DDAY1, DDAX2, DDAY2);

        }


        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackgroundImage = null;
            button1.Text = ("Draw");
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Text = null;
            button1.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.graphic_tools_icon;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            button6.BackgroundImage = null;
            button6.Text = ("Clear");
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.Text = null;
            button6.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.Magic_eraser_tool_icon;
        }

     

        private void button3_MouseHover(object sender, EventArgs e)
        {
            button3.BackgroundImage = null;
            button3.Text = ("Translate");
            
        }
        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.Text = null;
            button3.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.shape_move_back_icon;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button2.BackgroundImage = null;
            button2.Text = ("Rotate");
            
        }
        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.Text = null;
            button2.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.undo_icon;
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            button4.BackgroundImage = null;
            button4.Text = ("Scal");
        }
        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.Text = null;
            button4.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.Arrows_Sync_icon;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label10.Text = ("( " + (Cursor.Position.X - (Location.X + pictureBox1.Location.X + 8))) + " , " + ((Cursor.Position.Y - (Location.Y + pictureBox1.Location.Y + 30)) + " )");
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            label24.Text = ("( " + (Cursor.Position.X - (Location.X + pictureBox1.Location.X + 8))) + " , " + ((Cursor.Position.Y - (Location.Y + pictureBox1.Location.Y + 30)) + " )");
        }
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            timer1.Stop();
            label10.Text = "( 0 , 0 )";
        }
        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            timer2.Start();
        }
        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            timer2.Stop();
            label24.Text = "( 0 , 0 )";
        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
            dataGridView2.DataSource = null;
            dataGridView2.Refresh();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            brestable.Columns.Clear();
            brestable.Rows.Clear();
            double sx = double.Parse(textBox10.Text);
            double sy = double.Parse(textBox11.Text);
            double[,] scall ={
                      {sx,0,0},
                      {0,sy,0},
                      {0,0,1}
                      };

            double[,] p1_zer0 = tras_to_zero((DDAX1 + DDAX2) / 2, (DDAY1 + DDAY2) / 2);
            double newddx1 = DDAX1 + p1_zer0[0, 2];
            double newddx2 = DDAX2 + p1_zer0[0, 2];

            double newddy1 = DDAY1 + p1_zer0[1, 2];
            double newddy2 = DDAY2 + p1_zer0[1, 2];

            double[,] come_back = tras_to_zero((DDAX1 + DDAX2) / -2, (DDAY1 + DDAY2) / -2);

            double[,] test = matrix33_X_matrix33(come_back, scall);
            double[,] res = matrix33_X_matrix33(test, p1_zer0);

            double[] point_1_before = { DDAX1, DDAY1, 1 };
            double[] point_2_before = { DDAX2, DDAY2, 1 };

            double[] point_1_after_rotate = matrix33_X_matrix31(res, point_1_before);
            double[] point_2_after_rotate = matrix33_X_matrix31(res, point_2_before);

            Bresenham((int)point_1_after_rotate[0], (int)point_1_after_rotate[1], (int)point_2_after_rotate[0], (int)point_2_after_rotate[1]);

            DDAX1 = (int)point_1_after_rotate[0];
            DDAY1 = (int)point_1_after_rotate[1];
            DDAX2 = (int)point_2_after_rotate[0];
            DDAY2 = (int)point_2_after_rotate[1];
        }

        private void button17_Click(object sender, EventArgs e)
        {
            brestable.Columns.Clear();
            brestable.Rows.Clear();
            double sx = double.Parse(textBox11.Text);
            double sy = double.Parse(textBox10.Text);
            double[,] scall ={
                      {sx,0,0},
                      {0,sy,0},
                      {0,0,1}
                      };

            double[,] p1_zer0 = tras_to_zero((DDAX1 + DDAX2) / 2, (DDAY1 + DDAY2) / 2);
            double newddx1 = DDAX1 + p1_zer0[0, 2];
            double newddx2 = DDAX2 + p1_zer0[0, 2];

            double newddy1 = DDAY1 + p1_zer0[1, 2];
            double newddy2 = DDAY2 + p1_zer0[1, 2];

            double[,] come_back = tras_to_zero((DDAX1 + DDAX2) / -2, (DDAY1 + DDAY2) / -2);

            double[,] test = matrix33_X_matrix33(come_back, scall);
            double[,] res = matrix33_X_matrix33(test, p1_zer0);

            double[] point_1_before = { DDAX1, DDAY1, 1 };
            double[] point_2_before = { DDAX2, DDAY2, 1 };

            double[] point_1_after_rotate = matrix33_X_matrix31(res, point_1_before);
            double[] point_2_after_rotate = matrix33_X_matrix31(res, point_2_before);

            Bresenham((int)point_1_after_rotate[0], (int)point_1_after_rotate[1], (int)point_2_after_rotate[0], (int)point_2_after_rotate[1]);

            DDAX1 = (int)point_1_after_rotate[0];
            DDAY1 = (int)point_1_after_rotate[1];
            DDAX2 = (int)point_2_after_rotate[0];
            DDAY2 = (int)point_2_after_rotate[1];
        }

        private void button8_Click(object sender, EventArgs e)
        {
            brestable.Columns.Clear();
            brestable.Rows.Clear();
            int tx = int.Parse(textBox13.Text);
            int ty = int.Parse(textBox12.Text);
            DDAX1 += tx;
            DDAX2 += tx;
            DDAY1 += ty;
            DDAY2 += ty;
            Bresenham(DDAX1, DDAY1, DDAX2, DDAY2);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            brestable.Columns.Clear();
            brestable.Rows.Clear();
            double ceta = double.Parse(textBox14.Text);
            double[,] rotate ={
                      {Math.Cos(ceta),Math.Sin(ceta)*dir,0},
                      {Math.Sin(ceta)*dir2,Math.Cos(ceta),0},
                      {0,0,1}
                      };
            double[,] p1_zer0 = tras_to_zero((DDAX1 + DDAX2) / 2, (DDAY1 + DDAY2) / 2);
            double newddx1 = DDAX1 + p1_zer0[0, 2];
            double newddx2 = DDAX2 + p1_zer0[0, 2];

            double newddy1 = DDAY1 + p1_zer0[1, 2];
            double newddy2 = DDAY2 + p1_zer0[1, 2];

            double[,] come_back = tras_to_zero((DDAX1 + DDAX2) / -2, (DDAY1 + DDAY2) / -2);

            double[,] test = matrix33_X_matrix33(come_back, rotate);
            double[,] res = matrix33_X_matrix33(test, p1_zer0);

            double[] point_1_before = { DDAX1, DDAY1, 1 };
            double[] point_2_before = { DDAX2, DDAY2, 1 };

            double[] point_1_after_rotate = matrix33_X_matrix31(res, point_1_before);
            double[] point_2_after_rotate = matrix33_X_matrix31(res, point_2_before);

            Bresenham((int)point_1_after_rotate[0], (int)point_1_after_rotate[1], (int)point_2_after_rotate[0], (int)point_2_after_rotate[1]);

            DDAX1 = (int)point_1_after_rotate[0];
            DDAY1 = (int)point_1_after_rotate[1];

            DDAX2 = (int)point_2_after_rotate[0];
            DDAY2 = (int)point_2_after_rotate[1];
        }

        private void button10_Click(object sender, EventArgs e)
        {
            brestable.Columns.Clear();
            brestable.Rows.Clear();
            int x1 = int.Parse(textBox18.Text);
            int y1 = int.Parse(textBox15.Text);

            int x2 = int.Parse(textBox17.Text);
            int y2 = int.Parse(textBox16.Text);
            Bresenham(x1, y1, x2, y2);
        }
        private void button10_MouseHover(object sender, EventArgs e)
        {
            button10.BackgroundImage = null;
            button10.Text = ("Draw");
        }
        private void button10_MouseLeave(object sender, EventArgs e)
        {
            button10.Text = null;
            button10.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.graphic_tools_icon;
        }
        private void button5_MouseHover(object sender, EventArgs e)
        {
            button5.BackgroundImage = null;
            button5.Text = ("Clear");
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.Text = null;
            button5.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.Magic_eraser_tool_icon;
        }



        private void button8_MouseHover(object sender, EventArgs e)
        {
            button8.BackgroundImage = null;
            button8.Text = ("Translate");

        }
        private void button8_MouseLeave(object sender, EventArgs e)
        {
            button8.Text = null;
            button8.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.shape_move_back_icon;
        }

        private void button9_MouseHover(object sender, EventArgs e)
        {
            button9.BackgroundImage = null;
            button9.Text = ("Rotate");

        }
        private void button9_MouseLeave(object sender, EventArgs e)
        {
            button9.Text = null;
            button9.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.undo_icon;
        }

        private void button7_MouseHover(object sender, EventArgs e)
        {
            button7.BackgroundImage = null;
            button7.Text = ("Scal");
        }
        private void button7_MouseLeave(object sender, EventArgs e)
        {
            button7.Text = null;
            button7.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.Arrows_Sync_icon;
        }
       
        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DDAtable.Columns.Clear();
            DDAtable.Rows.Clear();
            double sx = double.Parse(textBox9.Text);
            double sy = double.Parse(textBox8.Text);
            double[,] scall ={
                      {sx,0,0},
                      {0,sy,0},
                      {0,0,1}
                      };
            double[,] p1_zer0 = tras_to_zero((DDAX1 + DDAX2) / 2, (DDAY1 + DDAY2) / 2);
            double newddx1 = DDAX1 + p1_zer0[0, 2];
            double newddx2 = DDAX2 + p1_zer0[0, 2];

            double newddy1 = DDAY1 + p1_zer0[1, 2];
            double newddy2 = DDAY2 + p1_zer0[1, 2];

            double[,] come_back = tras_to_zero((DDAX1 + DDAX2) / -2, (DDAY1 + DDAY2) / -2);

            double[,] test = matrix33_X_matrix33(come_back, scall);
            double[,] res = matrix33_X_matrix33(test, p1_zer0);

            double[] point_1_before = { DDAX1, DDAY1, 1 };
            double[] point_2_before = { DDAX2, DDAY2, 1 };

            double[] point_1_after_rotate = matrix33_X_matrix31(res, point_1_before);
            double[] point_2_after_rotate = matrix33_X_matrix31(res, point_2_before);

            lineDDA((int)point_1_after_rotate[0], (int)point_1_after_rotate[1], (int)point_2_after_rotate[0], (int)point_2_after_rotate[1]);

            DDAX1 = (int)point_1_after_rotate[0];
            DDAY1 = (int)point_1_after_rotate[1];
            DDAX2 = (int)point_2_after_rotate[0];
            DDAY2 = (int)point_2_after_rotate[1];
        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            circletable.Columns.Clear();
            circletable.Rows.Clear();
            int x = int.Parse(textBox25.Text);
            int y = int.Parse(textBox26.Text);
            DDAX1 = x;
            DDAY1 = y;
            int r = int.Parse(textBox24.Text);
            cyrcle(x, y, r);
        }

       
        private void button13_Click(object sender, EventArgs e)
        {
            circletable.Columns.Clear();
            circletable.Rows.Clear();
            int tx = int.Parse(textBox22.Text);
            int ty = int.Parse(textBox21.Text);
            DDAX1 += tx;
            DDAY1 += ty;
            cyrcle(DDAX1, DDAY1, int.Parse(textBox24.Text));
        }

       

        private void button12_Click(object sender, EventArgs e)
        {
            circletable.Columns.Clear();
            circletable.Rows.Clear();
            double sx = double.Parse(textBox19.Text);
            double sy = double.Parse(textBox20.Text);
            double[,] points = new double[DATA.Count, 2];
            double[,] points_in_zero = new double[DATA.Count, 2];
            for (int i = 0; i < DATA.Count; i++)
            {
                points[i, 0] = DATA[i][1];
                points[i, 1] = DATA[i][2];
            }

            for (int i = 0; i < DATA.Count; i++)
            {
                double[,] test = tras_to_zero(points[i, 0], points[i, 1]);
            }

        }

        private void button15_MouseHover(object sender, EventArgs e)
        {
            button15.BackgroundImage = null;
            button15.Text = ("Draw");
        }

        private void button15_MouseLeave(object sender, EventArgs e)
        {
            button15.Text = null;
            button15.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.graphic_tools_icon;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            dataGridView3.DataSource = null;
            dataGridView3.Refresh();
        }

        private void button11_MouseHover(object sender, EventArgs e)
        {
            button11.BackgroundImage = null;
            button11.Text = ("Clear");
        }

        private void button11_MouseLeave(object sender, EventArgs e)
        {
            button11.Text = null;
            button11.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.Magic_eraser_tool_icon;
        }

        private void button13_MouseHover(object sender, EventArgs e)
        {
            button13.BackgroundImage = null;
            button13.Text = ("Translate");
        }

        private void button13_MouseLeave(object sender, EventArgs e)
        {
            button13.Text = null;
            button13.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.shape_move_back_icon;
        }

        private void button12_MouseHover(object sender, EventArgs e)
        {
            button12.BackgroundImage = null;
            button12.Text = ("Scal");
        }

        private void button12_MouseLeave(object sender, EventArgs e)
        {
            button12.Text = null;
            button12.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.Arrows_Sync_icon;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            label38.Text = ("( " + (Cursor.Position.X - (Location.X + pictureBox1.Location.X + 8))) + " , " + ((Cursor.Position.Y - (Location.Y + pictureBox1.Location.Y + 30)) + " )");

        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            timer3.Start();
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            timer3.Stop();
            label38.Text = "( 0 , 0 )";
        }

        private void button19_Click(object sender, EventArgs e)
        {
            ellipstable.Columns.Clear();
            ellipstable.Rows.Clear();
            int x = int.Parse(textBox33.Text);
            int y = int.Parse(textBox31.Text);
            int a = int.Parse(textBox34.Text);
            int b = int.Parse(textBox32.Text);
            elipse(x, y, a, b);
            DDAY1 = y;
            DDAX1 = x;
        }

      
        private void button18_Click(object sender, EventArgs e)
        {
            ellipstable.Columns.Clear();
            ellipstable.Rows.Clear();
            int tx = int.Parse(textBox30.Text);
            int ty = int.Parse(textBox29.Text);
            DDAX1 += tx;
            DDAY1 += ty;
            elipse(DDAX1, DDAY1, int.Parse(textBox34.Text), int.Parse(textBox32.Text));
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ellipstable.Columns.Clear();
            ellipstable.Rows.Clear();
            double ceta = double.Parse(textBox23.Text) * Math.PI / 180;
            pictureBox4.Image = new Bitmap(pictureBox4.Width, pictureBox4.Height);
            for (int i = 0; i < DATA.Count; i++)
            {
                double[,] rotate ={
                      {Math.Cos(ceta),Math.Sin(ceta)*dir,0},
                      {Math.Sin(ceta)*dir2,Math.Cos(ceta),0},
                      {0,0,1}
                      };
                double[,] p1_zer0 = tras_to_zero(DDAX1, DDAY1);
                double newddx1 = DATA[i][1] + p1_zer0[0, 2];

                double newddy1 = DATA[i][2] + p1_zer0[1, 2];
                double[,] come_back = tras_to_zero(DDAX1 * (-1), DDAY1 * (-1));

                double[,] test = matrix33_X_matrix33(come_back, rotate);

                double[,] res = matrix33_X_matrix33(test, p1_zer0);

                double[] point_1_before = { DATA[i][1], DATA[i][2], 1 };

                double[] point_1_after_rotate = matrix33_X_matrix31(res, point_1_before);


                draw_point((int)point_1_after_rotate[0], (int)point_1_after_rotate[1], pictureBox4);
                DATA[i][1] = (int)point_1_after_rotate[0];
                DATA[i][2] = (int)point_1_after_rotate[1];
               
            }
           
        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            ellipstable.Columns.Clear();
            ellipstable.Rows.Clear();
            double sx = double.Parse(textBox27.Text);
            double sy = double.Parse(textBox28.Text);
            double[,] scall ={
                      {sx,0,0},
                      {0,sy,0},
                      {0,0,1}
                      };
            double[,] p1_zer0 = tras_to_zero((DDAX1 + DDAX2) / 2, (DDAY1 + DDAY2) / 2);
            double newddx1 = DDAX1 + p1_zer0[0, 2];
            double newddx2 = DDAX2 + p1_zer0[0, 2];


            double newddy1 = DDAY1 + p1_zer0[1, 2];
            double newddy2 = DDAY2 + p1_zer0[1, 2];

            double[,] come_back = tras_to_zero((DDAX1 + DDAX2) / -2, (DDAY1 + DDAY2) / -2);

            double[,] test = matrix33_X_matrix33(come_back, scall);
            double[,] res = matrix33_X_matrix33(test, p1_zer0);

            double[] point_1_before = { DDAX1, DDAY1, 1 };
            double[] point_2_before = { DDAX2, DDAY2, 1 };

            double[] point_1_after_rotate = matrix33_X_matrix31(res, point_1_before);
            double[] point_2_after_rotate = matrix33_X_matrix31(res, point_2_before);

            elipse((int)point_1_after_rotate[0], (int)point_1_after_rotate[1], (int)point_2_after_rotate[0], (int)point_2_after_rotate[1]);

            DDAX1 = (int)point_1_after_rotate[0];
            DDAY1 = (int)point_1_after_rotate[1];
            DDAX2 = (int)point_2_after_rotate[0];
            DDAY2 = (int)point_2_after_rotate[1];

         

           
        }

        private void button16_Click(object sender, EventArgs e)
        {

            pictureBox4.Image = null;
            dataGridView4.DataSource = null;
            dataGridView4.Refresh();
        }

        private void button19_MouseHover(object sender, EventArgs e)
        {
            button19.BackgroundImage = null;
            button19.Text = ("Draw");
        }

        private void button19_MouseLeave(object sender, EventArgs e)
        {
            button19.Text = null;
            button19.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.graphic_tools_icon;
        }

        private void button18_MouseHover(object sender, EventArgs e)
        {
            button18.BackgroundImage = null;
            button18.Text = ("Translate");
        }

        private void button18_MouseLeave(object sender, EventArgs e)
        {
            button18.Text = null;
            button18.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.shape_move_back_icon;
        }

        private void button14_MouseHover(object sender, EventArgs e)
        {
            button14.BackgroundImage = null;
            button14.Text = ("Rotate");
        }

        private void button14_MouseLeave(object sender, EventArgs e)
        {
            button14.Text = null;
            button14.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.undo_icon;
        }

        private void button17_MouseHover(object sender, EventArgs e)
        {
            button17.BackgroundImage = null;
            button17.Text = ("Scal");
        }

        private void button17_MouseLeave(object sender, EventArgs e)
        {
            button17.Text = null;
            button17.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.Arrows_Sync_icon;
        }

        private void button16_MouseHover(object sender, EventArgs e)
        {
            button16.BackgroundImage = null;
            button16.Text = ("Clear");
        }

        private void button16_MouseLeave(object sender, EventArgs e)
        {
            button16.Text = null;
            button16.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.Magic_eraser_tool_icon;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            label42.Text = ("( " + (Cursor.Position.X - (Location.X + pictureBox1.Location.X + 8))) + " , " + ((Cursor.Position.Y - (Location.Y + pictureBox1.Location.Y + 30)) + " )");
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            timer4.Start();
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            timer4.Stop();
            label42.Text = "( 0 , 0 )";
        }

      
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
