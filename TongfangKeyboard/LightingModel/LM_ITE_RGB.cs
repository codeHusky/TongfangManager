// Decompiled with JetBrains decompiler
// Type: LightingModel.LM_ITE_RGB
// Assembly: GamingCenter, Version=1.0.0.35, Culture=neutral, PublicKeyToken=null
// MVID: CF6E1F4E-70FD-4705-A8F7-86A4F7835797
// Assembly location: C:\Program Files\OEM\GamingCenter\GamingCenter.exe

using Microsoft.Win32.SafeHandles;
using OemServiceModel;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UsbHidModel;
using Utility;

namespace LightingModel
{
    public class LM_ITE_RGB : ITE_SPEC, ILM_RGBKB
  {
    public HIDManager m_HIDManager;
    public FileStream m_HIDDevice;
    public RGBKB_Type m_ITE_KB_Type;
    public byte m_Project_ID;
    public byte m_effect_type;
    public SAVE_LIGHTING_EFFECT_DATA m_save_lighting_data;
    public bool m_enableOnkeyPressed;
    public bool m_light_lock;
    public Task m_ap_effect_task;
    public bool m_ap_effect_task_stop;
    public SAVE_LIGHTING_EFFECT_DATA m_ap_effect_data;

    private static event RGBKB_Event_Handler m_Layout_Event_handler;

    public void ScanCode_Hnadler(int scancode)
    {
      if (scancode != 240)
        return;
      this.m_light_lock = true;
      if (this.m_effect_type == (byte) 3)
        this.DLL_SetMusicMode(false, (byte) 0);
      RGBKB_Event_Data event_data = new RGBKB_Event_Data();
      byte light = 0;
      this.Get_ITE_Light_Value(ref light);
      event_data.event_id = RGBKB_EventID.Brightness_update;
      event_data.envet_data_len = 1U;
      event_data.event_data = new byte[1];
      event_data.event_data[0] = (byte) this.Translate_Layout_LightValue(light);
      LM_ITE_RGB.m_Layout_Event_handler(event_data);
      if (this.m_effect_type == (byte) 3)
        this.DLL_SetMusicMode(true, light);
      else if (this.m_effect_type == (byte) 4)
        this.m_ap_effect_data.save_light = light;
      this.m_light_lock = false;
    }

    public void Light_Lock()
    {
      if (!this.m_light_lock)
        return;
      for (uint index = 10; this.m_light_lock && index > 0U; --index)
        Thread.Sleep(5);
    }

    public void AP_Effect_Task()
    {
      RGB_S[] colorBuffer1 = new RGB_S[3];
      RGB_S[] colorBuffer2 = new RGB_S[126];
      byte saveLight = this.m_ap_effect_data.save_light;
      bool bRefresh = true;
      uint num1 = 0;
      while (!this.m_ap_effect_task_stop)
      {
        if ((int) saveLight != (int) this.m_ap_effect_data.save_light)
        {
          saveLight = this.m_ap_effect_data.save_light;
          bRefresh = true;
        }
        if (this.m_ap_effect_data.save_effect == (byte) 15)
        {
          if (num1 == 7U)
            num1 = 0U;
          colorBuffer1[0].R = this.m_ap_effect_data.save_layout_color.ColorBuffer[(int) num1].R;
          colorBuffer1[0].G = this.m_ap_effect_data.save_layout_color.ColorBuffer[(int) num1].G;
          colorBuffer1[0].B = this.m_ap_effect_data.save_layout_color.ColorBuffer[(int) num1].B;
          this.Set_ITE_Effect_Type_StaticMode(this.m_ap_effect_data.save_light, (byte) 0, colorBuffer1, bRefresh);
          Thread.Sleep(50 * (int) this.m_ap_effect_data.save_speed);
          ++num1;
          if (this.m_ap_effect_data.save_effect != (byte) 15)
            num1 = 0U;
        }
        else if (this.m_ap_effect_data.save_effect == (byte) 13)
        {
          colorBuffer1[0].R = this.m_ap_effect_data.save_layout_color.ColorBuffer[0].R;
          colorBuffer1[0].G = this.m_ap_effect_data.save_layout_color.ColorBuffer[0].G;
          colorBuffer1[0].B = this.m_ap_effect_data.save_layout_color.ColorBuffer[0].B;
          colorBuffer1[1].R = this.m_ap_effect_data.save_layout_color.ColorBuffer[1].R;
          colorBuffer1[1].G = this.m_ap_effect_data.save_layout_color.ColorBuffer[1].G;
          colorBuffer1[1].B = this.m_ap_effect_data.save_layout_color.ColorBuffer[1].B;
          colorBuffer1[2].R = this.m_ap_effect_data.save_layout_color.ColorBuffer[2].R;
          colorBuffer1[2].G = this.m_ap_effect_data.save_layout_color.ColorBuffer[2].G;
          colorBuffer1[2].B = this.m_ap_effect_data.save_layout_color.ColorBuffer[2].B;
          if (num1 == 20U)
            num1 = 0U;
          for (uint index = 0; index < 126U; ++index)
          {
            colorBuffer2[(int) index].ID = index;
            colorBuffer2[(int) index].R = (byte) 0;
            colorBuffer2[(int) index].G = (byte) 0;
            colorBuffer2[(int) index].B = (byte) 0;
          }
          if (num1 < 10U)
          {
            colorBuffer2[(int) num1].R = colorBuffer1[0].R;
            colorBuffer2[(int) num1].G = colorBuffer1[0].G;
            colorBuffer2[(int) num1].B = colorBuffer1[0].B;
            colorBuffer2[1 + (int) num1].R = colorBuffer1[0].R;
            colorBuffer2[1 + (int) num1].G = colorBuffer1[0].G;
            colorBuffer2[1 + (int) num1].B = colorBuffer1[0].B;
            colorBuffer2[21 + (int) num1].R = colorBuffer1[0].R;
            colorBuffer2[21 + (int) num1].G = colorBuffer1[0].G;
            colorBuffer2[21 + (int) num1].B = colorBuffer1[0].B;
            colorBuffer2[22 + (int) num1].R = colorBuffer1[0].R;
            colorBuffer2[22 + (int) num1].G = colorBuffer1[0].G;
            colorBuffer2[22 + (int) num1].B = colorBuffer1[0].B;
            colorBuffer2[42 + (int) num1].R = colorBuffer1[0].R;
            colorBuffer2[42 + (int) num1].G = colorBuffer1[0].G;
            colorBuffer2[42 + (int) num1].B = colorBuffer1[0].B;
            colorBuffer2[43 + (int) num1].R = colorBuffer1[0].R;
            colorBuffer2[43 + (int) num1].G = colorBuffer1[0].G;
            colorBuffer2[43 + (int) num1].B = colorBuffer1[0].B;
            colorBuffer2[63 + (int) num1].R = colorBuffer1[0].R;
            colorBuffer2[63 + (int) num1].G = colorBuffer1[0].G;
            colorBuffer2[63 + (int) num1].B = colorBuffer1[0].B;
            colorBuffer2[64 + (int) num1].R = colorBuffer1[0].R;
            colorBuffer2[64 + (int) num1].G = colorBuffer1[0].G;
            colorBuffer2[64 + (int) num1].B = colorBuffer1[0].B;
            colorBuffer2[84 + (int) num1].R = colorBuffer1[0].R;
            colorBuffer2[84 + (int) num1].G = colorBuffer1[0].G;
            colorBuffer2[84 + (int) num1].B = colorBuffer1[0].B;
            colorBuffer2[85 + (int) num1].R = colorBuffer1[0].R;
            colorBuffer2[85 + (int) num1].G = colorBuffer1[0].G;
            colorBuffer2[85 + (int) num1].B = colorBuffer1[0].B;
            colorBuffer2[105 + (int) num1].R = colorBuffer1[0].R;
            colorBuffer2[105 + (int) num1].G = colorBuffer1[0].G;
            colorBuffer2[105 + (int) num1].B = colorBuffer1[0].B;
            colorBuffer2[106 + (int) num1].R = colorBuffer1[0].R;
            colorBuffer2[106 + (int) num1].G = colorBuffer1[0].G;
            colorBuffer2[106 + (int) num1].B = colorBuffer1[0].B;
            colorBuffer2[20 - (int) num1].R = colorBuffer1[1].R;
            colorBuffer2[20 - (int) num1].G = colorBuffer1[1].G;
            colorBuffer2[20 - (int) num1].B = colorBuffer1[1].B;
            colorBuffer2[19 - (int) num1].R = colorBuffer1[1].R;
            colorBuffer2[19 - (int) num1].G = colorBuffer1[1].G;
            colorBuffer2[19 - (int) num1].B = colorBuffer1[1].B;
            colorBuffer2[41 - (int) num1].R = colorBuffer1[1].R;
            colorBuffer2[41 - (int) num1].G = colorBuffer1[1].G;
            colorBuffer2[41 - (int) num1].B = colorBuffer1[1].B;
            colorBuffer2[40 - (int) num1].R = colorBuffer1[1].R;
            colorBuffer2[40 - (int) num1].G = colorBuffer1[1].G;
            colorBuffer2[40 - (int) num1].B = colorBuffer1[1].B;
            colorBuffer2[62 - (int) num1].R = colorBuffer1[1].R;
            colorBuffer2[62 - (int) num1].G = colorBuffer1[1].G;
            colorBuffer2[62 - (int) num1].B = colorBuffer1[1].B;
            colorBuffer2[61 - (int) num1].R = colorBuffer1[1].R;
            colorBuffer2[61 - (int) num1].G = colorBuffer1[1].G;
            colorBuffer2[61 - (int) num1].B = colorBuffer1[1].B;
            colorBuffer2[83 - (int) num1].R = colorBuffer1[1].R;
            colorBuffer2[83 - (int) num1].G = colorBuffer1[1].G;
            colorBuffer2[83 - (int) num1].B = colorBuffer1[1].B;
            colorBuffer2[82 - (int) num1].R = colorBuffer1[1].R;
            colorBuffer2[82 - (int) num1].G = colorBuffer1[1].G;
            colorBuffer2[82 - (int) num1].B = colorBuffer1[1].B;
            colorBuffer2[104 - (int) num1].R = colorBuffer1[1].R;
            colorBuffer2[104 - (int) num1].G = colorBuffer1[1].G;
            colorBuffer2[104 - (int) num1].B = colorBuffer1[1].B;
            colorBuffer2[103 - (int) num1].R = colorBuffer1[1].R;
            colorBuffer2[103 - (int) num1].G = colorBuffer1[1].G;
            colorBuffer2[103 - (int) num1].B = colorBuffer1[1].B;
            colorBuffer2[125 - (int) num1].R = colorBuffer1[1].R;
            colorBuffer2[125 - (int) num1].G = colorBuffer1[1].G;
            colorBuffer2[125 - (int) num1].B = colorBuffer1[1].B;
            colorBuffer2[124 - (int) num1].R = colorBuffer1[1].R;
            colorBuffer2[124 - (int) num1].G = colorBuffer1[1].G;
            colorBuffer2[124 - (int) num1].B = colorBuffer1[1].B;
          }
          else
          {
            uint num2 = num1 - 10U;
            colorBuffer2[10 - (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[10 - (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[10 - (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[9 - (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[9 - (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[9 - (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[31 - (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[31 - (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[31 - (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[30 - (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[30 - (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[30 - (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[52 - (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[52 - (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[52 - (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[51 - (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[51 - (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[51 - (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[73 - (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[73 - (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[73 - (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[72 - (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[72 - (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[72 - (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[94 - (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[94 - (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[94 - (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[93 - (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[93 - (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[93 - (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[115 - (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[115 - (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[115 - (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[114 - (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[114 - (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[114 - (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[10 + (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[10 + (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[10 + (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[11 + (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[9 + (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[9 + (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[31 + (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[31 + (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[31 + (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[30 + (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[30 + (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[30 + (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[52 + (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[52 + (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[52 + (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[51 + (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[51 + (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[51 + (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[73 + (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[73 + (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[73 + (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[72 + (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[72 + (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[72 + (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[94 + (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[94 + (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[94 + (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[93 + (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[93 + (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[93 + (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[115 + (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[115 + (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[115 + (int) num2].B = colorBuffer1[2].B;
            colorBuffer2[114 + (int) num2].R = colorBuffer1[2].R;
            colorBuffer2[114 + (int) num2].G = colorBuffer1[2].G;
            colorBuffer2[114 + (int) num2].B = colorBuffer1[2].B;
          }
          this.Set_ITE_Effect_Type_UserMode(this.m_ap_effect_data.save_light, (byte) 0, colorBuffer2, bRefresh);
          Thread.Sleep(20 * (int) this.m_ap_effect_data.save_speed);
          ++num1;
          if (this.m_ap_effect_data.save_effect != (byte) 13)
            num1 = 0U;
        }
        else if (this.m_ap_effect_data.save_effect == (byte) 12)
        {
          colorBuffer1[0].R = this.m_ap_effect_data.save_layout_color.ColorBuffer[0].R;
          colorBuffer1[0].G = this.m_ap_effect_data.save_layout_color.ColorBuffer[0].G;
          colorBuffer1[0].B = this.m_ap_effect_data.save_layout_color.ColorBuffer[0].B;
          colorBuffer1[1].R = this.m_ap_effect_data.save_layout_color.ColorBuffer[1].R;
          colorBuffer1[1].G = this.m_ap_effect_data.save_layout_color.ColorBuffer[1].G;
          colorBuffer1[1].B = this.m_ap_effect_data.save_layout_color.ColorBuffer[1].B;
          colorBuffer1[2].R = this.m_ap_effect_data.save_layout_color.ColorBuffer[2].R;
          colorBuffer1[2].G = this.m_ap_effect_data.save_layout_color.ColorBuffer[2].G;
          colorBuffer1[2].B = this.m_ap_effect_data.save_layout_color.ColorBuffer[2].B;
          for (uint index = 0; index < 126U; ++index)
          {
            colorBuffer2[(int) index].ID = index;
            colorBuffer2[(int) index].R = (byte) 0;
            colorBuffer2[(int) index].G = (byte) 0;
            colorBuffer2[(int) index].B = (byte) 0;
          }
          uint num2 = num1 % 15U;
          uint num3 = num1 / 15U;
          byte r = colorBuffer1[(int) num3].R;
          byte g = colorBuffer1[(int) num3].G;
          byte b = colorBuffer1[(int) num3].B;
          colorBuffer2[52].R = r;
          colorBuffer2[52].G = g;
          colorBuffer2[52].B = b;
          switch (num2)
          {
            case 0:
              colorBuffer2[0].R = colorBuffer2[105].R = r;
              colorBuffer2[0].G = colorBuffer2[105].G = g;
              colorBuffer2[0].B = colorBuffer2[105].B = b;
              colorBuffer2[20].R = colorBuffer2[125].R = r;
              colorBuffer2[20].G = colorBuffer2[125].G = g;
              colorBuffer2[20].B = colorBuffer2[125].B = b;
              break;
            case 1:
              colorBuffer2[1].R = colorBuffer2[21].R = colorBuffer2[84].R = colorBuffer2[106].R = r;
              colorBuffer2[1].G = colorBuffer2[21].G = colorBuffer2[84].G = colorBuffer2[106].G = g;
              colorBuffer2[1].B = colorBuffer2[21].B = colorBuffer2[84].B = colorBuffer2[106].B = b;
              colorBuffer2[19].R = colorBuffer2[41].R = colorBuffer2[104].R = colorBuffer2[124].R = r;
              colorBuffer2[19].G = colorBuffer2[41].G = colorBuffer2[104].G = colorBuffer2[124].G = g;
              colorBuffer2[19].B = colorBuffer2[41].B = colorBuffer2[104].B = colorBuffer2[124].B = b;
              break;
            case 2:
              colorBuffer2[2].R = colorBuffer2[21].R = colorBuffer2[22].R = colorBuffer2[63].R = colorBuffer2[84].R = colorBuffer2[85].R = colorBuffer2[107].R = r;
              colorBuffer2[2].G = colorBuffer2[21].G = colorBuffer2[22].G = colorBuffer2[63].G = colorBuffer2[84].G = colorBuffer2[85].G = colorBuffer2[107].G = g;
              colorBuffer2[2].B = colorBuffer2[21].B = colorBuffer2[22].B = colorBuffer2[63].B = colorBuffer2[84].B = colorBuffer2[85].B = colorBuffer2[107].B = b;
              colorBuffer2[18].R = colorBuffer2[40].R = colorBuffer2[41].R = colorBuffer2[83].R = colorBuffer2[103].R = colorBuffer2[104].R = colorBuffer2[123].R = r;
              colorBuffer2[18].G = colorBuffer2[40].G = colorBuffer2[41].G = colorBuffer2[83].G = colorBuffer2[103].G = colorBuffer2[104].G = colorBuffer2[123].G = g;
              colorBuffer2[18].B = colorBuffer2[40].B = colorBuffer2[41].B = colorBuffer2[83].B = colorBuffer2[103].B = colorBuffer2[104].B = colorBuffer2[123].B = b;
              break;
            case 3:
              colorBuffer2[3].R = colorBuffer2[22].R = colorBuffer2[23].R = colorBuffer2[42].R = colorBuffer2[63].R = colorBuffer2[64].R = colorBuffer2[85].R = colorBuffer2[86].R = colorBuffer2[108].R = r;
              colorBuffer2[3].G = colorBuffer2[22].G = colorBuffer2[23].G = colorBuffer2[42].G = colorBuffer2[63].G = colorBuffer2[64].G = colorBuffer2[85].G = colorBuffer2[86].G = colorBuffer2[108].G = g;
              colorBuffer2[3].B = colorBuffer2[22].B = colorBuffer2[23].B = colorBuffer2[42].B = colorBuffer2[63].B = colorBuffer2[64].B = colorBuffer2[85].B = colorBuffer2[86].B = colorBuffer2[108].B = b;
              colorBuffer2[17].R = colorBuffer2[38].R = colorBuffer2[39].R = colorBuffer2[62].R = colorBuffer2[82].R = colorBuffer2[83].R = colorBuffer2[102].R = colorBuffer2[103].R = colorBuffer2[122].R = r;
              colorBuffer2[17].G = colorBuffer2[38].G = colorBuffer2[39].G = colorBuffer2[62].G = colorBuffer2[82].G = colorBuffer2[83].G = colorBuffer2[102].G = colorBuffer2[103].G = colorBuffer2[122].G = g;
              colorBuffer2[17].B = colorBuffer2[38].B = colorBuffer2[39].B = colorBuffer2[62].B = colorBuffer2[82].B = colorBuffer2[83].B = colorBuffer2[102].B = colorBuffer2[103].B = colorBuffer2[122].B = b;
              break;
            case 4:
              colorBuffer2[4].R = colorBuffer2[23].R = colorBuffer2[24].R = colorBuffer2[42].R = colorBuffer2[43].R = colorBuffer2[64].R = colorBuffer2[65].R = colorBuffer2[86].R = colorBuffer2[87].R = colorBuffer2[109].R = r;
              colorBuffer2[4].G = colorBuffer2[23].G = colorBuffer2[24].G = colorBuffer2[42].G = colorBuffer2[43].G = colorBuffer2[64].G = colorBuffer2[65].G = colorBuffer2[86].G = colorBuffer2[87].G = colorBuffer2[109].G = g;
              colorBuffer2[4].B = colorBuffer2[23].B = colorBuffer2[24].B = colorBuffer2[42].B = colorBuffer2[43].B = colorBuffer2[64].B = colorBuffer2[65].B = colorBuffer2[86].B = colorBuffer2[87].B = colorBuffer2[109].B = b;
              colorBuffer2[16].R = colorBuffer2[37].R = colorBuffer2[38].R = colorBuffer2[61].R = colorBuffer2[62].R = colorBuffer2[81].R = colorBuffer2[82].R = colorBuffer2[101].R = colorBuffer2[102].R = colorBuffer2[121].R = r;
              colorBuffer2[16].G = colorBuffer2[37].G = colorBuffer2[38].G = colorBuffer2[61].G = colorBuffer2[62].G = colorBuffer2[81].G = colorBuffer2[82].G = colorBuffer2[101].G = colorBuffer2[102].G = colorBuffer2[121].G = g;
              colorBuffer2[16].B = colorBuffer2[37].B = colorBuffer2[38].B = colorBuffer2[61].B = colorBuffer2[62].B = colorBuffer2[81].B = colorBuffer2[82].B = colorBuffer2[101].B = colorBuffer2[102].B = colorBuffer2[121].B = b;
              break;
            case 5:
              colorBuffer2[5].R = colorBuffer2[24].R = colorBuffer2[25].R = colorBuffer2[43].R = colorBuffer2[44].R = colorBuffer2[65].R = colorBuffer2[66].R = colorBuffer2[87].R = colorBuffer2[88].R = colorBuffer2[110].R = r;
              colorBuffer2[5].G = colorBuffer2[24].G = colorBuffer2[25].G = colorBuffer2[43].G = colorBuffer2[44].G = colorBuffer2[65].G = colorBuffer2[66].G = colorBuffer2[87].G = colorBuffer2[88].G = colorBuffer2[110].G = g;
              colorBuffer2[5].B = colorBuffer2[24].B = colorBuffer2[25].B = colorBuffer2[43].B = colorBuffer2[44].B = colorBuffer2[65].B = colorBuffer2[66].B = colorBuffer2[87].B = colorBuffer2[88].B = colorBuffer2[110].B = b;
              colorBuffer2[15].R = colorBuffer2[36].R = colorBuffer2[37].R = colorBuffer2[60].R = colorBuffer2[61].R = colorBuffer2[80].R = colorBuffer2[81].R = colorBuffer2[100].R = colorBuffer2[101].R = colorBuffer2[120].R = r;
              colorBuffer2[15].G = colorBuffer2[36].G = colorBuffer2[37].G = colorBuffer2[60].G = colorBuffer2[61].G = colorBuffer2[80].G = colorBuffer2[81].G = colorBuffer2[100].G = colorBuffer2[101].G = colorBuffer2[120].G = g;
              colorBuffer2[15].B = colorBuffer2[36].B = colorBuffer2[37].B = colorBuffer2[60].B = colorBuffer2[61].B = colorBuffer2[80].B = colorBuffer2[81].B = colorBuffer2[100].B = colorBuffer2[101].B = colorBuffer2[120].B = b;
              break;
            case 6:
              colorBuffer2[6].R = colorBuffer2[25].R = colorBuffer2[26].R = colorBuffer2[44].R = colorBuffer2[45].R = colorBuffer2[66].R = colorBuffer2[67].R = colorBuffer2[88].R = colorBuffer2[89].R = colorBuffer2[111].R = r;
              colorBuffer2[6].G = colorBuffer2[25].G = colorBuffer2[26].G = colorBuffer2[44].G = colorBuffer2[45].G = colorBuffer2[66].G = colorBuffer2[67].G = colorBuffer2[88].G = colorBuffer2[89].G = colorBuffer2[111].G = g;
              colorBuffer2[6].B = colorBuffer2[25].B = colorBuffer2[26].B = colorBuffer2[44].B = colorBuffer2[45].B = colorBuffer2[66].B = colorBuffer2[67].B = colorBuffer2[88].B = colorBuffer2[89].B = colorBuffer2[111].B = b;
              colorBuffer2[14].R = colorBuffer2[35].R = colorBuffer2[36].R = colorBuffer2[59].R = colorBuffer2[60].R = colorBuffer2[79].R = colorBuffer2[80].R = colorBuffer2[99].R = colorBuffer2[100].R = colorBuffer2[119].R = r;
              colorBuffer2[14].G = colorBuffer2[35].G = colorBuffer2[36].G = colorBuffer2[59].G = colorBuffer2[60].G = colorBuffer2[79].G = colorBuffer2[80].G = colorBuffer2[99].G = colorBuffer2[100].G = colorBuffer2[119].G = g;
              colorBuffer2[14].B = colorBuffer2[35].B = colorBuffer2[36].B = colorBuffer2[59].B = colorBuffer2[60].B = colorBuffer2[79].B = colorBuffer2[80].B = colorBuffer2[99].B = colorBuffer2[100].B = colorBuffer2[119].B = b;
              break;
            case 7:
              colorBuffer2[7].R = colorBuffer2[26].R = colorBuffer2[27].R = colorBuffer2[45].R = colorBuffer2[46].R = colorBuffer2[67].R = colorBuffer2[68].R = colorBuffer2[89].R = colorBuffer2[90].R = colorBuffer2[112].R = r;
              colorBuffer2[7].G = colorBuffer2[26].G = colorBuffer2[27].G = colorBuffer2[45].G = colorBuffer2[46].G = colorBuffer2[67].G = colorBuffer2[68].G = colorBuffer2[89].G = colorBuffer2[90].G = colorBuffer2[112].G = g;
              colorBuffer2[7].B = colorBuffer2[26].B = colorBuffer2[27].B = colorBuffer2[45].B = colorBuffer2[46].B = colorBuffer2[67].B = colorBuffer2[68].B = colorBuffer2[89].B = colorBuffer2[90].B = colorBuffer2[112].B = b;
              colorBuffer2[13].R = colorBuffer2[34].R = colorBuffer2[35].R = colorBuffer2[58].R = colorBuffer2[59].R = colorBuffer2[78].R = colorBuffer2[79].R = colorBuffer2[98].R = colorBuffer2[99].R = colorBuffer2[118].R = r;
              colorBuffer2[13].G = colorBuffer2[34].G = colorBuffer2[35].G = colorBuffer2[58].G = colorBuffer2[59].G = colorBuffer2[78].G = colorBuffer2[79].G = colorBuffer2[98].G = colorBuffer2[99].G = colorBuffer2[118].G = g;
              colorBuffer2[13].B = colorBuffer2[34].B = colorBuffer2[35].B = colorBuffer2[58].B = colorBuffer2[59].B = colorBuffer2[78].B = colorBuffer2[79].B = colorBuffer2[98].B = colorBuffer2[99].B = colorBuffer2[118].B = b;
              break;
            case 8:
              colorBuffer2[8].R = colorBuffer2[27].R = colorBuffer2[28].R = colorBuffer2[46].R = colorBuffer2[47].R = colorBuffer2[68].R = colorBuffer2[69].R = colorBuffer2[90].R = colorBuffer2[91].R = colorBuffer2[113].R = r;
              colorBuffer2[8].G = colorBuffer2[27].G = colorBuffer2[28].G = colorBuffer2[46].G = colorBuffer2[47].G = colorBuffer2[68].G = colorBuffer2[69].G = colorBuffer2[90].G = colorBuffer2[91].G = colorBuffer2[113].G = g;
              colorBuffer2[8].B = colorBuffer2[27].B = colorBuffer2[28].B = colorBuffer2[46].B = colorBuffer2[47].B = colorBuffer2[68].B = colorBuffer2[69].B = colorBuffer2[90].B = colorBuffer2[91].B = colorBuffer2[113].B = b;
              colorBuffer2[12].R = colorBuffer2[33].R = colorBuffer2[34].R = colorBuffer2[57].R = colorBuffer2[58].R = colorBuffer2[77].R = colorBuffer2[78].R = colorBuffer2[97].R = colorBuffer2[98].R = colorBuffer2[117].R = r;
              colorBuffer2[12].G = colorBuffer2[33].G = colorBuffer2[34].G = colorBuffer2[57].G = colorBuffer2[58].G = colorBuffer2[77].G = colorBuffer2[78].G = colorBuffer2[97].G = colorBuffer2[98].G = colorBuffer2[117].G = g;
              colorBuffer2[12].B = colorBuffer2[33].B = colorBuffer2[34].B = colorBuffer2[57].B = colorBuffer2[58].B = colorBuffer2[77].B = colorBuffer2[78].B = colorBuffer2[97].B = colorBuffer2[98].B = colorBuffer2[117].B = b;
              break;
            case 9:
              colorBuffer2[9].R = colorBuffer2[28].R = colorBuffer2[29].R = colorBuffer2[47].R = colorBuffer2[48].R = colorBuffer2[69].R = colorBuffer2[70].R = colorBuffer2[91].R = colorBuffer2[92].R = colorBuffer2[114].R = r;
              colorBuffer2[9].G = colorBuffer2[28].G = colorBuffer2[29].G = colorBuffer2[47].G = colorBuffer2[48].G = colorBuffer2[69].G = colorBuffer2[70].G = colorBuffer2[91].G = colorBuffer2[92].G = colorBuffer2[114].G = g;
              colorBuffer2[9].B = colorBuffer2[28].B = colorBuffer2[29].B = colorBuffer2[47].B = colorBuffer2[48].B = colorBuffer2[69].B = colorBuffer2[70].B = colorBuffer2[91].B = colorBuffer2[92].B = colorBuffer2[114].B = b;
              colorBuffer2[11].R = colorBuffer2[32].R = colorBuffer2[33].R = colorBuffer2[56].R = colorBuffer2[57].R = colorBuffer2[76].R = colorBuffer2[77].R = colorBuffer2[96].R = colorBuffer2[97].R = colorBuffer2[116].R = r;
              colorBuffer2[11].G = colorBuffer2[32].G = colorBuffer2[33].G = colorBuffer2[56].G = colorBuffer2[57].G = colorBuffer2[76].G = colorBuffer2[77].G = colorBuffer2[96].G = colorBuffer2[97].G = colorBuffer2[116].G = g;
              colorBuffer2[11].B = colorBuffer2[32].B = colorBuffer2[33].B = colorBuffer2[56].B = colorBuffer2[57].B = colorBuffer2[76].B = colorBuffer2[77].B = colorBuffer2[96].B = colorBuffer2[97].B = colorBuffer2[116].B = b;
              break;
            case 10:
              colorBuffer2[10].R = colorBuffer2[29].R = colorBuffer2[30].R = colorBuffer2[48].R = colorBuffer2[49].R = colorBuffer2[70].R = colorBuffer2[71].R = colorBuffer2[92].R = colorBuffer2[93].R = r;
              colorBuffer2[10].G = colorBuffer2[29].G = colorBuffer2[30].G = colorBuffer2[48].G = colorBuffer2[49].G = colorBuffer2[70].G = colorBuffer2[71].G = colorBuffer2[92].G = colorBuffer2[93].G = g;
              colorBuffer2[10].B = colorBuffer2[29].B = colorBuffer2[30].B = colorBuffer2[48].B = colorBuffer2[49].B = colorBuffer2[70].B = colorBuffer2[71].B = colorBuffer2[92].B = colorBuffer2[93].B = b;
              colorBuffer2[31].R = colorBuffer2[32].R = colorBuffer2[55].R = colorBuffer2[56].R = colorBuffer2[75].R = colorBuffer2[76].R = colorBuffer2[95].R = colorBuffer2[96].R = r;
              colorBuffer2[31].G = colorBuffer2[32].G = colorBuffer2[55].G = colorBuffer2[56].G = colorBuffer2[75].G = colorBuffer2[76].G = colorBuffer2[95].G = colorBuffer2[96].G = g;
              colorBuffer2[31].B = colorBuffer2[32].B = colorBuffer2[55].B = colorBuffer2[56].B = colorBuffer2[75].B = colorBuffer2[76].B = colorBuffer2[95].B = colorBuffer2[96].B = b;
              break;
            case 11:
              colorBuffer2[30].R = colorBuffer2[49].R = colorBuffer2[50].R = colorBuffer2[71].R = colorBuffer2[72].R = colorBuffer2[93].R = r;
              colorBuffer2[30].G = colorBuffer2[49].G = colorBuffer2[50].G = colorBuffer2[71].G = colorBuffer2[72].G = colorBuffer2[93].G = g;
              colorBuffer2[30].B = colorBuffer2[49].B = colorBuffer2[50].B = colorBuffer2[71].B = colorBuffer2[72].B = colorBuffer2[93].B = b;
              colorBuffer2[31].R = colorBuffer2[54].R = colorBuffer2[55].R = colorBuffer2[74].R = colorBuffer2[75].R = colorBuffer2[94].R = r;
              colorBuffer2[31].G = colorBuffer2[54].G = colorBuffer2[55].G = colorBuffer2[74].G = colorBuffer2[75].G = colorBuffer2[94].G = g;
              colorBuffer2[31].B = colorBuffer2[54].B = colorBuffer2[55].B = colorBuffer2[74].B = colorBuffer2[75].B = colorBuffer2[94].B = b;
              break;
            case 12:
              colorBuffer2[50].R = colorBuffer2[51].R = colorBuffer2[72].R = r;
              colorBuffer2[50].G = colorBuffer2[51].G = colorBuffer2[72].G = g;
              colorBuffer2[50].B = colorBuffer2[51].B = colorBuffer2[72].B = b;
              colorBuffer2[53].R = colorBuffer2[54].R = colorBuffer2[73].R = r;
              colorBuffer2[53].G = colorBuffer2[54].G = colorBuffer2[73].G = g;
              colorBuffer2[53].B = colorBuffer2[54].B = colorBuffer2[73].B = b;
              break;
            case 13:
              colorBuffer2[51].R = colorBuffer2[52].R = r;
              colorBuffer2[51].G = colorBuffer2[52].G = g;
              colorBuffer2[51].B = colorBuffer2[52].B = b;
              colorBuffer2[53].R = r;
              colorBuffer2[53].G = g;
              colorBuffer2[53].B = b;
              break;
            case 14:
              colorBuffer2[52].R = r;
              colorBuffer2[52].G = g;
              colorBuffer2[52].B = b;
              break;
          }
          this.Set_ITE_Effect_Type_UserMode(this.m_ap_effect_data.save_light, (byte) 0, colorBuffer2, bRefresh);
          Thread.Sleep(20 * (int) this.m_ap_effect_data.save_speed);
          ++num1;
          if (num1 == 45U)
            num1 = 0U;
          if (this.m_ap_effect_data.save_effect != (byte) 12)
            num1 = 0U;
        }
        bRefresh = false;
        if (this.m_effect_type != (byte) 4)
          break;
      }
      this.m_ap_effect_task = (Task) null;
    }

    public bool HID_Set_Effect_Type_08H(
      byte Control,
      byte Effect,
      byte Speed,
      byte Light,
      byte ColorIndex,
      byte Direction,
      byte Save)
    {
      this.m_HIDManager.WriteFeature(new byte[9]
      {
        (byte) 0,
        (byte) 8,
        Control,
        Effect,
        Speed,
        Light,
        ColorIndex,
        Direction,
        Save
      });
      Thread.Sleep(1);
      return true;
    }

    public bool HID_Get_Effect_Type_88H(
      ref byte Control,
      ref byte Effect,
      ref byte Speed,
      ref byte Light,
      ref byte ColorIndex,
      ref byte Direction)
    {
      byte[] buffer1 = new byte[9];
      buffer1[1] = (byte) 136;
      this.m_HIDManager.WriteFeature(buffer1);
      Thread.Sleep(1);
      byte[] buffer2 = new byte[9];
      this.m_HIDManager.GetFeature(buffer2);
      Thread.Sleep(1);
      Control = buffer2[2];
      Effect = buffer2[3];
      Speed = buffer2[4];
      Light = buffer2[5];
      ColorIndex = buffer2[6];
      Direction = buffer2[7];
      return true;
    }

    public bool HID_Set_Picture_12H(byte Saved)
    {
      byte[] buffer = new byte[9];
      buffer[1] = (byte) 18;
      buffer[4] = (byte) 8;
      buffer[5] = Saved;
      this.m_HIDManager.WriteFeature(buffer);
      Thread.Sleep(1);
      return true;
    }

    public bool HID_Set_Color_14H(byte Index, byte R, byte G, byte B)
    {
      byte[] buffer;
      if (this.m_ITE_KB_Type == RGBKB_Type.MEZone_2nd_101 || this.m_ITE_KB_Type == RGBKB_Type.MEZone_2nd_102)
      {
        RGB_S rgbS = WKDColor.cheatRGB_2ndME(R, G, B);
        buffer = new byte[9]
        {
          (byte) 0,
          (byte) 20,
          (byte) 0,
          Index,
          rgbS.R,
          rgbS.G,
          rgbS.B,
          (byte) 0,
          (byte) 0
        };
      }
      else if (this.m_ITE_KB_Type == RGBKB_Type.FourZone && (this.m_Project_ID == (byte) 6 || this.m_Project_ID == (byte) 7))
      {
        RGB_S rgbS = WKDColor.cheatRGB_4Zone(R, G, B);
        buffer = new byte[9]
        {
          (byte) 0,
          (byte) 20,
          (byte) 0,
          Index,
          rgbS.R,
          rgbS.G,
          rgbS.B,
          (byte) 0,
          (byte) 0
        };
      }
      else
        buffer = new byte[9]
        {
          (byte) 0,
          (byte) 20,
          (byte) 0,
          Index,
          R,
          G,
          B,
          (byte) 0,
          (byte) 0
        };
      this.m_HIDManager.WriteFeature(buffer);
      Thread.Sleep(1);
      return true;
    }

    public bool HID_Set_RowIndex_16H(byte RowIndex)
    {
      byte[] buffer = new byte[9];
      buffer[1] = (byte) 22;
      buffer[3] = RowIndex;
      this.m_HIDManager.WriteFeature(buffer);
      Thread.Sleep(1);
      return true;
    }

    public bool DLL_SetMusicMode(bool enable, byte light_level)
    {
      try
      {
        if (enable)
        {
          this.HID_Set_Effect_Type_08H((byte) 2, (byte) 34, (byte) 0, light_level, (byte) 0, (byte) 0, (byte) 0);
          int num = MusicMode.m_delStartMonitorAudio((ushort) 0, light_level) ? 1 : 0;
        }
        else
          MusicMode.m_delStopMonitorAudio();
      }
      catch (Exception ex)
      {
        Log.s(LOG_LEVEL.ERROR, string.Format("DLL_SetMusicMode failed,enable ={0} e={1}", (object) enable, (object) ex.ToString()));
      }
      return true;
    }

    public bool Disable_EC_OnkeyPressed()
    {
      if (this.m_enableOnkeyPressed)
        this.m_enableOnkeyPressed = false;
      return true;
    }

    public bool Enable_EC_OnkeyPressed(byte effect, byte direction)
    {
      if (this.m_enableOnkeyPressed || effect != (byte) 6 && effect != (byte) 14 && (effect != (byte) 17 && effect != (byte) 4) || direction != (byte) 1)
        return true;
      object data = (object) 0;
      WMIEC.WMIReadECRAM(1857UL, ref data);
      WMIEC.WMIWriteECRAM(1857UL, (ulong) (byte) ((uint) (byte) Convert.ToUInt64(data) | 8U));
      this.m_enableOnkeyPressed = true;
      return true;
    }

    public bool Set_ITE_Effect_Type_UserMode(
      byte Light,
      byte Save,
      RGB_S[] colorBuffer,
      bool bRefresh)
    {
      Log.s(LOG_LEVEL.TRACE, string.Format("RGBKeyboard_ITE | Set_ITE_Effect_Type_UserMode light ={0}", (object) Light));
      if (bRefresh)
      {
        this.Light_Lock();
        this.HID_Set_Effect_Type_08H((byte) 2, (byte) 51, (byte) 0, Light, (byte) 0, (byte) 0, Save);
      }
      byte[] buffer = new byte[65];
      buffer[0] = (byte) 0;
      buffer[1] = (byte) 0;
      for (byte RowIndex = 0; RowIndex < (byte) 6; ++RowIndex)
      {
        for (byte index1 = 0; index1 < (byte) 21; ++index1)
        {
          int num = 5 - (int) RowIndex;
          int index2 = (int) index1 + num * 21;
          if (this.m_ITE_KB_Type == RGBKB_Type.MEZone_2nd_101 || this.m_ITE_KB_Type == RGBKB_Type.MEZone_2nd_102)
          {
            RGB_S rgbS = WKDColor.cheatRGB_2ndME(colorBuffer[index2].R, colorBuffer[index2].G, colorBuffer[index2].B);
            buffer[2 + (int) index1] = rgbS.B;
            buffer[23 + (int) index1] = rgbS.G;
            buffer[44 + (int) index1] = rgbS.R;
          }
          else if (this.m_ITE_KB_Type == RGBKB_Type.FourZone && (this.m_Project_ID == (byte) 6 || this.m_Project_ID == (byte) 7))
          {
            RGB_S rgbS = WKDColor.cheatRGB_4Zone(colorBuffer[index2].R, colorBuffer[index2].G, colorBuffer[index2].B);
            buffer[2 + (int) index1] = rgbS.B;
            buffer[23 + (int) index1] = rgbS.G;
            buffer[44 + (int) index1] = rgbS.R;
          }
          else
          {
            buffer[2 + (int) index1] = colorBuffer[index2].B;
            buffer[23 + (int) index1] = colorBuffer[index2].G;
            buffer[44 + (int) index1] = colorBuffer[index2].R;
          }
        }
        this.Light_Lock();
        this.HID_Set_RowIndex_16H(RowIndex);
        this.Light_Lock();
        this.m_HIDDevice.Write(buffer, 0, buffer.Length);
        Thread.Sleep(1);
      }
      return true;
    }

    public bool Set_ITE_Effect_Type_StaticMode(
      byte Light,
      byte Save,
      RGB_S[] colorBuffer,
      bool bRefresh)
    {
      Log.s(LOG_LEVEL.TRACE, string.Format("RGBKeyboard_ITE | Set_ITE_Effect_Type_StaticMode light ={0}", (object) Light));
      if (bRefresh)
      {
        this.Light_Lock();
        this.HID_Set_Effect_Type_08H((byte) 2, (byte) 51, (byte) 0, Light, (byte) 0, (byte) 0, Save);
      }
      byte[] buffer = new byte[65];
      buffer[0] = (byte) 0;
      RGB_S rgbS = new RGB_S();
      if (this.m_ITE_KB_Type == RGBKB_Type.MEZone_2nd_101 || this.m_ITE_KB_Type == RGBKB_Type.MEZone_2nd_102)
        rgbS = WKDColor.cheatRGB_2ndME(colorBuffer[0].R, colorBuffer[0].G, colorBuffer[0].B);
      else if (this.m_ITE_KB_Type == RGBKB_Type.FourZone && (this.m_Project_ID == (byte) 6 || this.m_Project_ID == (byte) 7))
        rgbS = WKDColor.cheatRGB_4Zone(colorBuffer[0].R, colorBuffer[0].G, colorBuffer[0].B);
      for (byte index = 1; index < (byte) 65; index += (byte) 4)
      {
        buffer[(int) index] = (byte) 0;
        if (this.m_ITE_KB_Type == RGBKB_Type.MEZone_2nd_101 || this.m_ITE_KB_Type == RGBKB_Type.MEZone_2nd_102)
        {
          buffer[(int) index + 1] = rgbS.R;
          buffer[(int) index + 2] = rgbS.G;
          buffer[(int) index + 3] = rgbS.B;
        }
        else
        {
          buffer[(int) index + 1] = colorBuffer[0].R;
          buffer[(int) index + 2] = colorBuffer[0].G;
          buffer[(int) index + 3] = colorBuffer[0].B;
        }
      }
      this.Light_Lock();
      this.HID_Set_Picture_12H(Save);
      for (byte index = 0; index < (byte) 8; ++index)
      {
        this.Light_Lock();
        this.m_HIDDevice.Write(buffer, 0, buffer.Length);
        Thread.Sleep(1);
      }
      return true;
    }

    public bool Set_ITE_Effect_Type_FwMode(
      byte effect,
      byte light,
      byte speed,
      byte direction,
      byte save,
      RGBKB_Color layout_color)
    {
      Log.s(LOG_LEVEL.TRACE, string.Format("RGBKeyboard_ITE | Set_ITE_Effect_Type_FwMode effect={0} light ={1}", (object) effect, (object) light));
      byte num;
      if (layout_color.ColorBlocks == 0U)
        num = (byte) 0;
      else if (layout_color.ColorBlocks > 0U && layout_color.isCircular)
      {
        num = (byte) 8;
        for (uint index = 0; index < layout_color.ColorBlocks; ++index)
        {
          this.HID_Set_Color_14H((byte) (index + 1U), layout_color.ColorBuffer[(int) index].R, layout_color.ColorBuffer[(int) index].G, layout_color.ColorBuffer[(int) index].B);
          Thread.Sleep(1);
        }
      }
      else
      {
        num = (byte) 1;
        byte r = layout_color.ColorBuffer[0].R;
        byte g = layout_color.ColorBuffer[0].G;
        byte b = layout_color.ColorBuffer[0].B;
        this.HID_Set_Color_14H(num, r, g, b);
      }
      return this.HID_Set_Effect_Type_08H((byte) 2, effect, speed, light, num, direction, save);
    }

    public bool Set_ITE_Effect_Type_ApMode(
      byte effect,
      byte light,
      byte speed,
      byte direction,
      byte save,
      RGBKB_Color layout_color)
    {
      this.m_ap_effect_data.save_effect = effect;
      this.m_ap_effect_data.save_light = light;
      this.m_ap_effect_data.save_speed = speed;
      this.m_ap_effect_data.save_direction = direction;
      this.m_ap_effect_data.save_layout_color = layout_color;
      if (this.m_ap_effect_task == null)
        this.m_ap_effect_task = new Task(new Action(this.AP_Effect_Task));
      this.m_ap_effect_task_stop = false;
      if (this.m_ap_effect_task.Status != TaskStatus.Running)
        this.m_ap_effect_task.Start();
      return true;
    }

    public bool Set_ITE_Effect_Type_ApMode_Stop()
    {
      this.m_ap_effect_task_stop = true;
      for (uint index = 3000; this.m_ap_effect_task != null || index == 0U; --index)
        Thread.Sleep(1);
      return true;
    }

    public void Save_Lighting_Effect_Data(
      byte save_effect,
      byte save_light,
      byte save_speed,
      byte save_direction,
      RGBKB_Color save_layout_color)
    {
      this.m_save_lighting_data.bSaved = true;
      this.m_save_lighting_data.save_effect = save_effect;
      this.m_save_lighting_data.save_light = save_light;
      this.m_save_lighting_data.save_speed = save_speed;
      this.m_save_lighting_data.save_direction = save_direction;
      this.m_save_lighting_data.save_layout_color = save_layout_color;
    }

    public void Save_Lighting_Color_Data(RGBKB_Color save_layout_color)
    {
      if (save_layout_color.ColorBlocks < 1U)
        return;
      for (int index = 0; (long) index < (long) this.m_save_lighting_data.save_layout_color.ColorBlocks; ++index)
      {
        if ((int) this.m_save_lighting_data.save_layout_color.ColorBuffer[index].ID == (int) save_layout_color.ColorBuffer[0].ID)
        {
          this.m_save_lighting_data.save_layout_color.ColorBuffer[index].R = save_layout_color.ColorBuffer[0].R;
          this.m_save_lighting_data.save_layout_color.ColorBuffer[index].G = save_layout_color.ColorBuffer[0].G;
          this.m_save_lighting_data.save_layout_color.ColorBuffer[index].B = save_layout_color.ColorBuffer[0].B;
          break;
        }
      }
    }

    public bool Set_Lighting_Effect(
      byte control,
      byte effect,
      byte light,
      byte speed,
      byte direction,
      byte save,
      RGBKB_Color layout_color)
    {
      Log.s(LOG_LEVEL.TRACE, string.Format("RGBKeyboard_ITE | Set_Lighting_Effect effect={0} light ={1}", (object) effect, (object) light));
      if (this.m_effect_type == (byte) 3)
        this.DLL_SetMusicMode(false, (byte) 0);
      else if (this.m_effect_type == (byte) 4 && effect != (byte) 15 && (effect != (byte) 12 && effect != (byte) 13))
        this.Set_ITE_Effect_Type_ApMode_Stop();
      switch (effect)
      {
        case 1:
          if (this.m_ITE_KB_Type == RGBKB_Type.FourZone)
          {
            this.m_effect_type = (byte) 0;
            this.Set_ITE_Effect_Type_FwMode(effect, light, speed, direction, save, layout_color);
            break;
          }
          this.m_effect_type = (byte) 2;
          RGB_S[] colorBuffer1 = layout_color.ColorBuffer;
          this.Set_ITE_Effect_Type_StaticMode(light, save, colorBuffer1, true);
          break;
        case 34:
          if (this.m_effect_type == (byte) 1 || this.m_effect_type == (byte) 2 || this.m_effect_type == (byte) 4)
            this.HID_Set_Effect_Type_08H((byte) 1, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0);
          this.m_effect_type = (byte) 3;
          this.DLL_SetMusicMode(true, light);
          break;
        case 51:
          this.m_effect_type = (byte) 1;
          RGB_S[] colorBuffer2 = layout_color.ColorBuffer;
          this.Set_ITE_Effect_Type_UserMode(light, save, colorBuffer2, true);
          break;
        case byte.MaxValue:
          return false;
        default:
          if (effect == (byte) 15 || effect == (byte) 12 || effect == (byte) 13)
          {
            this.m_effect_type = (byte) 4;
            this.Set_ITE_Effect_Type_ApMode(effect, light, speed, direction, save, layout_color);
            break;
          }
          if (effect == (byte) 5 && this.m_ITE_KB_Type == RGBKB_Type.FourZone)
          {
            this.m_effect_type = (byte) 0;
            this.HID_Set_Color_14H((byte) 1, byte.MaxValue, (byte) 0, (byte) 0);
            Thread.Sleep(1);
            this.HID_Set_Color_14H((byte) 2, (byte) 0, byte.MaxValue, (byte) 0);
            Thread.Sleep(1);
            this.HID_Set_Color_14H((byte) 3, (byte) 0, (byte) 0, byte.MaxValue);
            Thread.Sleep(1);
            this.HID_Set_Color_14H((byte) 4, byte.MaxValue, (byte) 0, byte.MaxValue);
            Thread.Sleep(1);
            this.Set_ITE_Effect_Type_FwMode(effect, light, speed, direction, save, layout_color);
            break;
          }
          this.m_effect_type = (byte) 0;
          this.Set_ITE_Effect_Type_FwMode(effect, light, speed, direction, save, layout_color);
          break;
      }
      this.Enable_EC_OnkeyPressed(effect, direction);
      return true;
    }

    public bool Get_ITE_Light_Value(ref byte light)
    {
      byte Control = 0;
      byte Effect = 0;
      byte Speed = 0;
      byte ColorIndex = 0;
      byte Direction = 0;
      this.HID_Get_Effect_Type_88H(ref Control, ref Effect, ref Speed, ref light, ref ColorIndex, ref Direction);
      return true;
    }

    public bool Set_Welcome_Effect_Enable(bool Enable, byte timeoutEffect, byte timeout)
    {
      byte num1 = Enable ? (byte) 1 : (byte) 0;
      try
      {
        if (this.m_Project_ID == (byte) 0 || this.m_Project_ID == (byte) 1 || (this.m_Project_ID == (byte) 2 || this.m_Project_ID == (byte) 3) || (this.m_Project_ID == (byte) 4 || this.m_Project_ID == (byte) 5))
        {
          Audio.Mute();
          OemService.Write(string.Format("ledkb /setdata 0x1A {0} {1} {2} 0x00 0x00 0x00 0x00", (object) timeoutEffect.ToString(), (object) num1.ToString(), (object) timeout.ToString()));
          Audio.UnMute();
        }
        else
        {
          ulong num2 = (ulong) (1873497444986126336L + ((long) timeoutEffect << 48) + ((long) num1 << 40) + ((long) timeout << 32));
          try
          {
            WMIEC.WMIWriteBiosRom(num2);
          }
          catch
          {
            Audio.Mute();
            OemService.Write(string.Format("ledkb /setdata 0x1A {0} {1} {2} 0x00 0x00 0x00 0x00", (object) timeoutEffect.ToString(), (object) num1.ToString(), (object) timeout.ToString()));
            Audio.UnMute();
          }
        }
      }
      catch
      {
      }
      return true;
    }

    public bool Set_Welcome_Effect(
      byte control,
      byte effect,
      byte light,
      byte speed,
      byte direction,
      byte save,
      RGBKB_Color layout_color)
    {
      switch (effect)
      {
        case 1:
          if (layout_color.ColorBlocks == 0U)
            return false;
          byte num1 = 8;
          uint num2 = 7;
          byte r = layout_color.ColorBuffer[0].R;
          byte g = layout_color.ColorBuffer[0].G;
          byte b = layout_color.ColorBuffer[0].B;
          for (uint index = 0; index < num2; ++index)
            this.HID_Set_Color_14H((byte) (index + 9U), r, g, b);
          effect = (byte) 3;
          if (this.m_Project_ID == (byte) 0 || this.m_Project_ID == (byte) 1 || (this.m_Project_ID == (byte) 2 || this.m_Project_ID == (byte) 3) || (this.m_Project_ID == (byte) 4 || this.m_Project_ID == (byte) 5))
          {
            Audio.Mute();
            OemService.Write(string.Format("ledkb /setdata 0x08 {0} {1} {2} {3} {4} {5} 0x00", (object) control.ToString(), (object) effect.ToString(), (object) speed.ToString(), (object) light.ToString(), (object) num1.ToString(), (object) direction.ToString()));
            Audio.UnMute();
          }
          else
          {
            ulong num3 = (ulong) (576460752303423488L + ((long) control << 48) + ((long) effect << 40) + ((long) speed << 32) + ((long) light << 24) + ((long) num1 << 16) + ((long) direction << 8));
            try
            {
              WMIEC.WMIWriteBiosRom(num3);
            }
            catch
            {
              Audio.Mute();
              OemService.Write(string.Format("ledkb /setdata 0x08 {0} {1} {2} {3} {4} {5} 0x00", (object) control.ToString(), (object) effect.ToString(), (object) speed.ToString(), (object) light.ToString(), (object) num1.ToString(), (object) direction.ToString()));
              Audio.UnMute();
            }
          }
          return true;
        case byte.MaxValue:
          return false;
        default:
          byte num4;
          if (layout_color.ColorBlocks == 0U)
            num4 = (byte) 0;
          else if (layout_color.ColorBlocks > 0U && layout_color.isCircular)
          {
            num4 = (byte) 8;
            for (uint index = 0; index < layout_color.ColorBlocks; ++index)
              this.HID_Set_Color_14H((byte) (index + 9U), layout_color.ColorBuffer[(int) index].R, layout_color.ColorBuffer[(int) index].G, layout_color.ColorBuffer[(int) index].B);
          }
          else
          {
            num4 = (byte) 9;
            for (byte Index = 9; Index <= (byte) 15; ++Index)
              this.HID_Set_Color_14H(Index, layout_color.ColorBuffer[0].R, layout_color.ColorBuffer[0].G, layout_color.ColorBuffer[0].B);
          }
          if (this.m_Project_ID == (byte) 0 || this.m_Project_ID == (byte) 1 || (this.m_Project_ID == (byte) 2 || this.m_Project_ID == (byte) 3) || (this.m_Project_ID == (byte) 4 || this.m_Project_ID == (byte) 5))
          {
            Audio.Mute();
            OemService.Write(string.Format("ledkb /setdata 0x08 {0} {1} {2} {3} {4} {5} 0x00", (object) control.ToString(), (object) effect.ToString(), (object) speed.ToString(), (object) light.ToString(), (object) num4.ToString(), (object) direction.ToString()));
            Audio.UnMute();
          }
          else
          {
            ulong num3 = (ulong) (576460752303423488L + ((long) control << 48) + ((long) effect << 40) + ((long) speed << 32) + ((long) light << 24) + ((long) num4 << 16) + ((long) direction << 8));
            try
            {
              WMIEC.WMIWriteBiosRom(num3);
            }
            catch
            {
              Audio.Mute();
              OemService.Write(string.Format("ledkb /setdata 0x08 {0} {1} {2} {3} {4} {5} 0x00", (object) control.ToString(), (object) effect.ToString(), (object) speed.ToString(), (object) light.ToString(), (object) num4.ToString(), (object) direction.ToString()));
              Audio.UnMute();
            }
          }
          return true;
      }
    }

    private RGBKB_Effect Translate_LM_EffectIndex(byte fw_effect_id)
    {
      switch (fw_effect_id)
      {
        case 1:
          return RGBKB_Effect.Single;
        case 2:
          return RGBKB_Effect.Breathing;
        case 3:
          return RGBKB_Effect.Wave;
        case 5:
          return RGBKB_Effect.Rainbow;
        case 6:
          return RGBKB_Effect.Ripple;
        case 9:
          return RGBKB_Effect.Marquee;
        case 10:
          return RGBKB_Effect.Raindrop;
        case 14:
          return RGBKB_Effect.Aurora;
        case 17:
          return RGBKB_Effect.Spark;
        case 22:
          return RGBKB_Effect.RippleO;
        case 34:
          return RGBKB_Effect.Music;
        case 51:
          return RGBKB_Effect.UserMode;
        default:
          return RGBKB_Effect.UnKnown;
      }
    }

    private byte Translate_ITE_EffectIndex(RGBKB_Effect layoutEffect)
    {
      switch (layoutEffect)
      {
        case RGBKB_Effect.Single:
          return 1;
        case RGBKB_Effect.Breathing:
          return 2;
        case RGBKB_Effect.Wave:
          return 3;
        case RGBKB_Effect.Reactive:
          return 4;
        case RGBKB_Effect.Rainbow:
          return 5;
        case RGBKB_Effect.Ripple:
          return 6;
        case RGBKB_Effect.Raindrop:
          return 10;
        case RGBKB_Effect.Neon:
          return 15;
        case RGBKB_Effect.Marquee:
          return 9;
        case RGBKB_Effect.Stack:
          return 12;
        case RGBKB_Effect.Impact:
          return 13;
        case RGBKB_Effect.Spark:
          return 17;
        case RGBKB_Effect.Aurora:
          return 14;
        case RGBKB_Effect.Music:
          return 34;
        case RGBKB_Effect.UserMode:
          return 51;
        case RGBKB_Effect.Flash:
          return 18;
        case RGBKB_Effect.Mix:
          return 19;
        case RGBKB_Effect.RippleO:
          return 22;
        default:
          return byte.MaxValue;
      }
    }

    private byte Translate_ITE_LightValue(uint layoutLight)
    {
      switch (layoutLight)
      {
        case 0:
          return 0;
        case 1:
          return 8;
        case 2:
          return 22;
        case 3:
          return 36;
        case 4:
          return 50;
        default:
          return 0;
      }
    }

    private byte Translate_ITE_SpeedValue(uint layoutSpeed)
    {
      switch (layoutSpeed)
      {
        case 0:
          return 10;
        case 1:
          return 7;
        case 2:
          return 5;
        case 3:
          return 3;
        case 4:
          return 1;
        default:
          return 1;
      }
    }

    private byte Translate_ITE_DirectionValue(RGBKB_Direction layoutDirection)
    {
      switch (layoutDirection)
      {
        case RGBKB_Direction.None:
          return 0;
        case RGBKB_Direction.LeftRight:
          return 1;
        case RGBKB_Direction.RightLeft:
          return 2;
        case RGBKB_Direction.DownUp:
          return 3;
        case RGBKB_Direction.UpDown:
          return 4;
        case RGBKB_Direction.OnKeyPressed:
          return 1;
        default:
          return 1;
      }
    }

    private uint Translate_Layout_LightValue(byte ite_light)
    {
      switch (ite_light)
      {
        case 0:
          return 0;
        case 8:
          return 1;
        case 22:
          return 2;
        case 36:
          return 3;
        case 50:
          return 4;
        default:
          return 0;
      }
    }

    public LM_ITE_RGB()
    {
      this.m_save_lighting_data.bSaved = false;
    }

    public bool ILM_RGBKB_Init(RGBKB_Event_Handler event_handler)
    {
      object data = (object) 0;
      WMIEC.WMIReadECRAM(1856UL, ref data);
      this.m_Project_ID = (byte) Convert.ToUInt64(data);
      this.m_HIDManager = new HIDManager();
      if (!this.m_HIDManager.Init((ushort) 1165, (ushort) 52736, (ushort) 1))
        return false;
      switch (this.m_HIDManager.GetUsagePage())
      {
        case 65282:
          this.m_ITE_KB_Type = RGBKB_Type.MEZone_1st;
                    //return;
          break;
        case 65283:
          try
          {
            switch (Convert.ToByte(URegistry.RegistryValueRead("", "KBTypeID", (object) 25)))
            {
              case 17:
              case 33:
                this.m_ITE_KB_Type = RGBKB_Type.MEZone_2nd_102;
                break;
              case 25:
              case 41:
                this.m_ITE_KB_Type = RGBKB_Type.MEZone_2nd_101;
                break;
              default:
                this.m_ITE_KB_Type = RGBKB_Type.MEZone_2nd_101;
                break;
            }
          }
          catch
          {
            this.m_ITE_KB_Type = RGBKB_Type.MEZone_2nd_101;
            Log.s(LOG_LEVEL.ERROR, "RGBKeyboard_ITE|ILM_RGBKB_Init : USAGE_PAGE_ME_2ND, query KBID failed");
            break;
          }
                    break;
        case 65298:
          this.m_ITE_KB_Type = RGBKB_Type.FourZone;
          break;
        default:
          return false;
      }
      this.m_HIDDevice = new FileStream(new SafeFileHandle(this.m_HIDManager.m_Handle, false), FileAccess.ReadWrite, 65, true);
      LM_ITE_RGB.m_Layout_Event_handler += event_handler;
      if (WMIEC.LMScanCodeEvent == null)
        WMIEC.LMScanCodeEvent += new WMIEC.LM_ScanCode_EventHander(this.ScanCode_Hnadler);
      return true;
    }

    public bool ILM_RGBKB_SetPower(RGBKB_PowerStatus PowerStatus)
    {
      Log.s(LOG_LEVEL.TRACE, string.Format("RGBKeyboard_ITE | ILM_RGBKB_SetPower powerstatus = {0}", (object) PowerStatus));
      bool flag = true;
      if (PowerStatus == RGBKB_PowerStatus.Off || PowerStatus == RGBKB_PowerStatus.Lighting_off)
      {
        //this.DLL_SetMusicMode(false, (byte) 0);
        this.Disable_EC_OnkeyPressed();
        Log.s(LOG_LEVEL.TRACE, "RGBKeyboard_ITE | ILM_RGBKB_SetPower HID_Set_Effect_Type_08H LED_OFF");
        if (this.m_effect_type == (byte) 4)
          this.Set_ITE_Effect_Type_ApMode_Stop();
        this.HID_Set_Effect_Type_08H((byte) 1, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0);
      }
      else if (PowerStatus == RGBKB_PowerStatus.On || PowerStatus == RGBKB_PowerStatus.Lighting_on)
      {
        if (this.m_effect_type == (byte) 3)
                    //this.DLL_SetMusicMode(true, this.m_save_lighting_data.save_light);
            Log.s(LOG_LEVEL.TRACE, "RGBKeyboard_ITE | ILM_RGBKB_SetPower music mode is bad so disabled");
        else if (this.m_save_lighting_data.bSaved)
        {
          Log.s(LOG_LEVEL.TRACE, string.Format("RGBKeyboard_ITE | ILM_RGBKB_SetPower Set_Lighting_Effect effect={0} light ={1}", (object) this.m_save_lighting_data.save_effect, (object) this.m_save_lighting_data.save_light));
          this.Set_Lighting_Effect((byte) 2, this.m_save_lighting_data.save_effect, this.m_save_lighting_data.save_light, this.m_save_lighting_data.save_speed, this.m_save_lighting_data.save_direction, (byte) 0, this.m_save_lighting_data.save_layout_color);
        }
        else
        {
          Log.s(LOG_LEVEL.TRACE, "RGBKeyboard_ITE | ILM_RGBKB_SetPower becuse ITE FW not support keep status, so return false to AP re-set again");
          flag = false;
        }
      }
      if (PowerStatus == RGBKB_PowerStatus.Off || PowerStatus == RGBKB_PowerStatus.Welcome_off)
        this.Set_Welcome_Effect_Enable(false, (byte) 5, (byte) 0);
      else if (PowerStatus == RGBKB_PowerStatus.On || PowerStatus == RGBKB_PowerStatus.Welcome_on)
        this.Set_Welcome_Effect_Enable(true, (byte) 5, (byte) 20);
      return flag;
    }

    public bool ILM_RGBKB_GetPower(ref RGBKB_PowerStatus PowerStatus)
    {
      return false;
    }

    public RGBKB_Type ILM_RGBKB_GetRGBKeyboardType()
    {
      return this.m_ITE_KB_Type;
    }

    public string ILM_RGBKB_GetFirmwareVersion()
    {
      byte[] buffer1 = new byte[9];
      buffer1[1] = (byte) 128;
      if (!this.m_HIDManager.WriteFeature(buffer1))
      {
        Log.s(LOG_LEVEL.ERROR, "RGBKeyboard_ITE|ILM_RGBKB_GetFirmwareVersion : WriteFeature failed");
      }
      else
      {
        Thread.Sleep(1);
        byte[] buffer2 = new byte[9];
        if (!this.m_HIDManager.GetFeature(buffer2))
        {
          Log.s(LOG_LEVEL.ERROR, "RGBKeyboard_ITE|ILM_RGBKB_GetFirmwareVersion : GetFeature failed");
        }
        else
        {
          Thread.Sleep(1);
          return string.Format("{0:X}.{1:X}.{2:X}.{3:X}", (object) buffer2[2], (object) buffer2[3], (object) buffer2[4], (object) buffer2[5]);
        }
      }
      return "";
    }

    public bool ILM_RGBKB_SetEffectALL(
      RGBKB_Mode layout_mode,
      RGBKB_Effect layout_effect,
      uint layout_light,
      uint layout_speed,
      RGBKB_Direction layout_direction,
      RGBKB_Color layout_color,
      RGBKB_NV_SAVE layout_save)
    {
      byte num1 = this.Translate_ITE_EffectIndex(layout_effect);
      byte num2 = this.Translate_ITE_LightValue(layout_light);
      byte num3 = this.Translate_ITE_SpeedValue(layout_speed);
      byte num4 = this.Translate_ITE_DirectionValue(layout_direction);
      byte save = (byte) layout_save;
      switch (layout_mode)
      {
        case RGBKB_Mode.Lighting:
          this.Save_Lighting_Effect_Data(num1, num2, num3, num4, layout_color);
          this.Set_Lighting_Effect((byte) 2, num1, num2, num3, num4, save, layout_color);
          break;
        case RGBKB_Mode.Welcome:
          this.Set_Welcome_Effect((byte) 3, num1, num2, num3, num4, save, layout_color);
          break;
      }
      return false;
    }

    public bool ILM_RGBKB_GetEffectALL(
      RGBKB_Mode layout_mode,
      ref RGBKB_Effect layout_effect,
      ref uint layout_light,
      ref uint layout_speed,
      ref RGBKB_Direction layout_direction,
      ref RGBKB_Color layout_color)
    {
        return false;
    }

    public bool ILM_RGBKB_SetEffect(RGBKB_Mode layout_mode, RGBKB_Effect layout_effect)
    {
      return false;
    }

    public bool ILM_RGBKB_GetEffect(RGBKB_Mode layout_mode, ref RGBKB_Effect layout_effect)
    {
      if (layout_mode == RGBKB_Mode.Lighting || layout_mode != RGBKB_Mode.Welcome)
        return false;
      string[] bufferCmd = new string[3]
      {
        "OemServiceWinApp.exe",
        "ledkb",
        "/getstatus"
      };
      int lenRead = 512;
      byte[] bufferRead = new byte[512];
      OemService.Exec(bufferCmd.Length, bufferCmd, lenRead, bufferRead);
      byte fw_effect_id = Convert.ToByte(string.Format("{0}{1}", (object) (char) bufferRead[6], (object) (char) bufferRead[7]), 16);
      layout_effect = this.Translate_LM_EffectIndex(fw_effect_id);
      return false;
    }

    public bool ILM_RGBKB_SetBrighntess(uint layout_brightness)
    {
      return false;
    }

    public bool ILM_RGBKB_GetBrighntess(ref uint layout_brightness)
    {
      byte light = 0;
      if (!this.Get_ITE_Light_Value(ref light))
        return false;
      layout_brightness = (uint) (byte) this.Translate_Layout_LightValue(light);
      return true;
    }

    public bool ILM_RGBKB_SetSpeed(uint layout_speed)
    {
      return false;
    }

    public bool ILM_RGBKB_SetColor(
      RGBKB_Mode layout_mode,
      RGBKB_Effect layout_effect,
      RGBKB_Color layout_color)
    {
      if (layout_color.ColorBlocks == 0U)
        return false;
      if (layout_mode == RGBKB_Mode.Lighting)
      {
        this.Save_Lighting_Color_Data(layout_color);
        if (layout_effect == RGBKB_Effect.Single)
        {
          if (this.m_ITE_KB_Type == RGBKB_Type.FourZone)
          {
            this.HID_Set_Color_14H((byte) (layout_color.ColorBuffer[0].ID + 1U), layout_color.ColorBuffer[0].R, layout_color.ColorBuffer[0].G, layout_color.ColorBuffer[0].B);
            return true;
          }
          byte[] buffer = new byte[65];
          buffer[0] = (byte) 0;
          RGB_S rgbS = new RGB_S();
          if (this.m_ITE_KB_Type == RGBKB_Type.MEZone_2nd_101 || this.m_ITE_KB_Type == RGBKB_Type.MEZone_2nd_102)
            rgbS = WKDColor.cheatRGB_2ndME(layout_color.ColorBuffer[0].R, layout_color.ColorBuffer[0].G, layout_color.ColorBuffer[0].B);
          else if (this.m_ITE_KB_Type == RGBKB_Type.FourZone && (this.m_Project_ID == (byte) 6 || this.m_Project_ID == (byte) 7))
            rgbS = WKDColor.cheatRGB_4Zone(layout_color.ColorBuffer[0].R, layout_color.ColorBuffer[0].G, layout_color.ColorBuffer[0].B);
          for (byte index = 1; index < (byte) 65; index += (byte) 4)
          {
            buffer[(int) index] = (byte) 0;
            if (this.m_ITE_KB_Type == RGBKB_Type.MEZone_2nd_101 || this.m_ITE_KB_Type == RGBKB_Type.MEZone_2nd_102)
            {
              buffer[(int) index + 1] = rgbS.R;
              buffer[(int) index + 2] = rgbS.G;
              buffer[(int) index + 3] = rgbS.B;
            }
            else
            {
              buffer[(int) index + 1] = layout_color.ColorBuffer[0].R;
              buffer[(int) index + 2] = layout_color.ColorBuffer[0].G;
              buffer[(int) index + 3] = layout_color.ColorBuffer[0].B;
            }
          }
          this.HID_Set_Picture_12H((byte) 0);
          for (byte index = 0; index < (byte) 8; ++index)
          {
            this.m_HIDDevice.Write(buffer, 0, buffer.Length);
            Thread.Sleep(1);
          }
          return true;
        }
        this.HID_Set_Color_14H((byte) (layout_color.ColorBuffer[0].ID + 1U), layout_color.ColorBuffer[0].R, layout_color.ColorBuffer[0].G, layout_color.ColorBuffer[0].B);
      }
      return true;
    }

    public bool ILM_RGBKB_SaveLightingLevel(uint layout_light)
    {
      this.m_save_lighting_data.save_light = this.Translate_ITE_LightValue(layout_light);
      return true;
    }
  }
}
