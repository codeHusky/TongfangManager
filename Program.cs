using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using LightingModel;
using OSDView;
using Utility;
using JsonFx.Json;
using JsonFx.Serialization;
using JsonFx.Serialization.Resolvers;
using System.IO;

namespace TongfangManager
{
    class Program
    {
        public static LM_Manager lm_manager;

        public static OSDManager osdManager;

        public static Config config;

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

        public static void SaveDefaultConfig()
        {
            config = new Config();
            config.keyboard = new Config.Keyboard();
            config.keyboard.red = 255;
            config.keyboard.green = 215;
            config.keyboard.blue = 245;
            config.keyboard.brightness = 4;
            // = conf;
            Console.WriteLine("!!! Loading Default Config...");
            SaveConfig();
        }

        public static void LoadConfig()
        {
            if (!File.Exists("settings.conf"))
            {
                SaveDefaultConfig();
            }
            else
            {
                var reader = new JsonReader(new DataReaderSettings(new DataContractResolverStrategy()));
                // use convention over configuration on the way out
                var writer = new JsonWriter();//new DataWriterSettings(new ConventionResolverStrategy(ConventionResolverStrategy.WordCasing.Lowercase, "-")));


                config = reader.Read<Config>(File.ReadAllText("settings.conf"));
                Console.WriteLine("!!! Config Loaded");
            }
        }

        public static void SaveConfig()
        {
            var reader = new JsonReader(new DataReaderSettings(new DataContractResolverStrategy()));
            // use convention over configuration on the way out
            var writer = new JsonWriter();//new DataWriterSettings(new ConventionResolverStrategy(ConventionResolverStrategy.WordCasing.Lowercase, "-")));
            int red = Program.config.keyboard.red;//Config.User.keyboard.red;
            int green = Program.config.keyboard.green;//Config.User.keyboard.green;
            int blue = Program.config.keyboard.blue;//Config.User.keyboard.blue;
            int brightness = Program.config.keyboard.brightness;//Config.User.keyboard.brightness;

            Console.WriteLine(red + "," + green + "," + blue + " @ " + brightness);
            string json = writer.Write(config);
            File.WriteAllText("settings.conf", json);
            Console.WriteLine("!!! Config Saved");
        }

        [STAThread]
        static void Main(string[] args)
        {

            Log.setLogEnabled(true);
            Console.WriteLine("!!! Loading Config...");
            LoadConfig();

            lm_manager = new LM_Manager();
            
            osdManager = new OSDManager();
            Console.WriteLine("!!! Established");

            osdManager.Start();

            Console.WriteLine("!! OSD STARTED");

            Console.WriteLine("!!! INIT");
            lm_manager.LM_Init(new RGBKB_Event_Handler(Event_LM));
            Console.WriteLine("!!! INIT DONE");

            if (args.Contains("onlyset"))
            {
                SetKeyboardColor((byte)config.keyboard.red, (byte)config.keyboard.green, (byte)config.keyboard.blue);
                return;
            }

            

            

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
