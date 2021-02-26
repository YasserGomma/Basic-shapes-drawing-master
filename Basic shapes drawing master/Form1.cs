using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Basic_shapes_drawing_master
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackgroundImage = null;
            button1.Text = "DDA ";
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Text = null;
            button1.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.Line_icon;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button2.BackgroundImage = null;
            button2.Text = "Bresenham ";
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.Text = null;
            button2.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.bresenham;
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            button3.BackgroundImage = null;
            button3.Text = "Circle ";
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.Text = null;
            button3.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.circle_icon;
        }
        private void button4_MouseHover(object sender, EventArgs e)
        {
            button4.BackgroundImage = null;
            button4.Text = "Ellipse ";
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.Text = null;
            button4.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.Ellipse_tool_icon;
        }

        private void mainbutton_Click(object sender, EventArgs e)
        {
            MainForm mainform = new MainForm();
            mainform.Show();
        }

        private void mainbutton_MouseHover(object sender, EventArgs e)
        {
            label2.Text = null;
            mainbutton.BackgroundImage = null;
            mainbutton.Text = "Click to start";
        }

        private void mainbutton_MouseLeave(object sender, EventArgs e)
        {
            mainbutton.Text = null;
            label2.Text = ("Com-");
            mainbutton.BackgroundImage = Basic_shapes_drawing_master.Properties.Resources.unnamed;
        }
    }
}
