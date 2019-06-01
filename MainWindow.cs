using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.CSharp.RuntimeBinder;


namespace TongfangManager
{
    public partial class MainWindow : Form
    {
        private Boolean ignoreUpdate = false;
        
        public MainWindow()
        {
            InitializeComponent();


            int red = Program.config.keyboard.red;//Config.User.keyboard.red;
            int green = Program.config.keyboard.green;//Config.User.keyboard.green;
            int blue = Program.config.keyboard.blue;//Config.User.keyboard.blue;
            int brightness = Program.config.keyboard.brightness;//Config.User.keyboard.brightness;

            Console.WriteLine(red+","+green+","+blue+" @ " + brightness);

            setRGB(red, green, blue);
        }


        private void setRGB(int red, int green, int blue)
        {
            ignoreUpdate = true;
            rVal.Value = red;
            gVal.Value = green;
            bVal.Value = blue;
            ignoreUpdate = false;
            rgbValChanged(null, null);
            /*Color c = Color.FromArgb(rVal.Value, gVal.Value, bVal.Value);
            pictureBox2.BackColor = c;

            this.redValue.Text = c.R.ToString();
            this.greenValue.Text = c.G.ToString();
            this.blueValue.Text = c.B.ToString();

            Program.SetKeyboardColor(c.R, c.G, c.B);*/
        }

        private void rgbValChanged(object sender, System.EventArgs e)
        {
            if (ignoreUpdate) return;
            Color c = Color.FromArgb(rVal.Value,gVal.Value,bVal.Value);
            pictureBox2.BackColor = c;

            this.redValue.Text = c.R.ToString();
            this.greenValue.Text = c.G.ToString();
            this.blueValue.Text = c.B.ToString();

            Program.SetKeyboardColor(c.R,c.G,c.B);
            Program.config.keyboard.red = rVal.Value;
            Program.config.keyboard.green = gVal.Value;
            Program.config.keyboard.blue = bVal.Value;
            //Program.Red = rVal.Value;
        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            //Program.osdManager.ScanCode_Handler(176);
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            Program.SaveConfig();
        }
    }
}
