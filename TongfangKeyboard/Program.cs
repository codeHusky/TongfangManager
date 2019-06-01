using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Drawing;
using System.Windows.Forms;
using LightingModel;
using OSDView;
using Utility;
using JsonFx;
using JsonFx.Json;
using JsonFx.Serialization.Resolvers;
using JsonFx.Serialization;

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

        static void WriteResourceToFile(string resourceName, string fileName)
        {
            using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    resource.CopyTo(file);
                }
            }
        }
        public static void WriteDefaultConfig()
        {
            Console.WriteLine("/!\\ Writing default config");
            WriteResourceToFile("TongfangManager.default.conf", "settings.conf");
        }

        public static void SaveConfig()
        {
            //var reader = new JsonReader();
            var writer = new JsonWriter();
            var streamWriter = new StreamWriter("settings.conf");
            string json = writer.Write(config);
            streamWriter.Write(json);
            streamWriter.Close();
            Console.WriteLine("!!! Saved Config");
        }

        [STAThread]
        static void Main(string[] args)
        {

            Log.setLogEnabled(true);
            Console.WriteLine("!!! Checking Config");

            if (!File.Exists("settings.conf") )//|| (!File.ReadAllText("settings.conf").StartsWith("{") || !File.ReadAllText("settings.conf").EndsWith("}") ))
            {
                WriteDefaultConfig();
            }

                // respect DataContracts on the way in
            var reader = new JsonReader(new DataReaderSettings(new DataContractResolverStrategy()));
            // use convention over configuration on the way out
            var writer = new JsonWriter();//new DataWriterSettings(new ConventionResolverStrategy(ConventionResolverStrategy.WordCasing.Lowercase, "-")));

            config = reader.Read<Config>(File.ReadAllText("settings.conf"));
            Console.WriteLine("!!! Read config successful");
            
            
            
            //File.WriteAllText(fileName, Properties.Resources.default);

            lm_manager = new LM_Manager();
            
            osdManager = new OSDManager();
            Console.WriteLine("!!! Established");

            osdManager.Start();

            Console.WriteLine("!! OSD STARTED");

            Console.WriteLine("!!! INIT");
            lm_manager.LM_Init(new RGBKB_Event_Handler(Event_LM));
            Console.WriteLine("!!! INIT DONE");

            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
            //255 215 254
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
