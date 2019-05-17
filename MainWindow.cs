using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConsoleApp1;

namespace ColorPicker
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, EventArgs e)
        {
            //openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            //pictureBox1.ImageLocation = openFileDialog1.FileName;
        }

        /*private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                Color c = bmp.GetPixel(e.X, e.Y);
                pictureBox2.BackColor = c;

                //aVal.Text = c.A.ToString();
                rVal.Value = c.R;//.ToString();
                gVal.Value = c.G;//.ToString();
                bVal.Value = c.B;//.ToString();

            }
            catch(Exception ex) {

            }
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                Bitmap bmp = new Bitmap(pictureBox1.Image);
                Color c = bmp.GetPixel(e.X, e.Y);
                pictureBox2.BackColor = c;

                //aVal.Text = c.A.ToString();
                rVal.Value = c.R;//.ToString();
                gVal.Value = c.G;//.ToString();
                bVal.Value = c.B;//.ToString();

            }
            catch (Exception ex)
            {

            }
        }*/

        private void rgbValChanged(object sender, System.EventArgs e)
        {
            Color c = Color.FromArgb(rVal.Value,gVal.Value,bVal.Value);
            pictureBox2.BackColor = c;

            this.redValue.Text = c.R.ToString();
            this.greenValue.Text = c.G.ToString();
            this.blueValue.Text = c.B.ToString();

            Program.SetKeyboardColor(c.R,c.G,c.B);
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Program.osdManager.ShowOSD(176);
        }
    }
}
