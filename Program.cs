using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using ColorPicker;
using LightingModel;
using Utility;

namespace ConsoleApp1
{
    class Program
    {
        static LM_Manager lm_manager;

        public static void SetKeyboardColor(byte red, byte green, byte blue)
        {
            RGBKB_Color clr = new RGBKB_Color(true, 4U);
            for (uint ind = 0; ind < 4; ++ind)
            {
                clr.ColorBuffer[ind].R = red;
                clr.ColorBuffer[ind].G = green;
                clr.ColorBuffer[ind].B = blue;
            }

            //Console.WriteLine("!!! SET effect");
            lm_manager.LM_SetEffectALL(RGBKB_Mode.Lighting, RGBKB_Effect.Single, 4, 0, RGBKB_Direction.LeftRight, clr, RGBKB_NV_SAVE.NOT_SAVE);
            lm_manager.LM_SetEffectALL(RGBKB_Mode.Lighting, RGBKB_Effect.Single, 4, 0, RGBKB_Direction.LeftRight, clr, RGBKB_NV_SAVE.SAVE);
            lm_manager.LM_SetEffectALL(RGBKB_Mode.Welcome, RGBKB_Effect.Single, 4, 0, RGBKB_Direction.LeftRight, clr, RGBKB_NV_SAVE.NOT_SAVE);
            lm_manager.LM_SetEffectALL(RGBKB_Mode.Welcome, RGBKB_Effect.Single, 4, 0, RGBKB_Direction.LeftRight, clr, RGBKB_NV_SAVE.SAVE);
        }
        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }
        [STAThread]
        static void Main(string[] args)
        {

            Log.setLogEnabled(false);
            lm_manager = new LM_Manager();
            Console.WriteLine("!!! INIT");
            lm_manager.LM_Init(new RGBKB_Event_Handler(Event_LM));
            Console.WriteLine("!!! INIT DONE");
            /*RGBKB_Color clr = new RGBKB_Color(true, 4U);
            for (uint ind = 0; ind < 4; ++ind)
            {
                clr.ColorBuffer[ind].R = 255;
                clr.ColorBuffer[ind].G = 255;
                clr.ColorBuffer[ind].B = 255;
            }
            //lm_manager.LM_SetPower(RGBKB_PowerStatus.Lighting_on);
            //lm_manager.LM_SetEffectALL(RGBKB_Mode.Welcome, RGBKB_Effect.Single, 4, 0, 0, clr, RGBKB_NV_SAVE.NOT_SAVE);
            Console.WriteLine("project id: " + lm_manager.m_LM_ITE_RGB.m_Project_ID);
            Console.WriteLine("project id: " + lm_manager.m_LM_ITE_RGB);
            Console.WriteLine("kb type: " + lm_manager.m_LM_ITE_RGB.m_ITE_KB_Type.ToString());

            byte[] buffer1 = new byte[9];
            buffer1[1] = (byte)136;
            lm_manager.m_LM_ITE_RGB.m_HIDManager.WriteFeature(buffer1);
            Thread.Sleep(1);

            byte[] buffer2 = new byte[9];
            lm_manager.m_LM_ITE_RGB.m_HIDManager.GetFeature(buffer2);
            Thread.Sleep(1);

            for (int i = 0; i < buffer2.Length; i++)
            {
                Console.WriteLine(" val " + i + ": " + buffer2[0]);
            }
            Console.WriteLine("!!! BULLSHIT DONE");*/
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
            return;
        }

        public static void Event_LM(RGBKB_Event_Data event_data)
        {
            if (event_data.event_id != RGBKB_EventID.Brightness_update)
                return;
            uint brightness = (uint)event_data.event_data[0];

        }
    }
}
