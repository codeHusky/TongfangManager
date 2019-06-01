using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace TongfangManager
{
    [DataContract]
    class Config
    {
        [DataContract]
        public class Keyboard {
            [DataMember(Name = "red")] public int red;
            [DataMember(Name = "green")] public int green;
            [DataMember(Name = "blue")] public int blue;
            [DataMember(Name = "brightness")] public int brightness;

        }
        [DataMember(Name = "keyboard")] public Keyboard keyboard;
    }
}
