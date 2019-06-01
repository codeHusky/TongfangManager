// Decompiled with JetBrains decompiler
// Type: OSDView.OSDManager
// Assembly: GamingCenter, Version=1.0.0.35, Culture=neutral, PublicKeyToken=null
// MVID: CF6E1F4E-70FD-4705-A8F7-86A4F7835797
// Assembly location: C:\Program Files\OEM\GamingCenter\GamingCenter.exe

using LightingModel;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Utility;

namespace OSDView
{
  public class OSDManager : OSDDefine
  {
    private LM_Manager m_LM;

    private void SetPanelStatus()
    {
      Win32.DISPLAY_DEVICE lpDisplayDevice1 = new Win32.DISPLAY_DEVICE();
      lpDisplayDevice1.cb = Marshal.SizeOf<Win32.DISPLAY_DEVICE>(lpDisplayDevice1);
      try
      {
        for (uint iDevNum1 = 0; Win32.EnumDisplayDevices((string) null, iDevNum1, ref lpDisplayDevice1, 0U); ++iDevNum1)
        {
          if ((lpDisplayDevice1.StateFlags & 1) == 1)
          {
            Console.WriteLine("{0}, {1}", (object) lpDisplayDevice1.DeviceName, (object) lpDisplayDevice1.StateFlags);
            Win32.DISPLAY_DEVICE lpDisplayDevice2 = new Win32.DISPLAY_DEVICE();
            lpDisplayDevice2.cb = Marshal.SizeOf<Win32.DISPLAY_DEVICE>(lpDisplayDevice2);
            for (uint iDevNum2 = 0; Win32.EnumDisplayDevices(lpDisplayDevice1.DeviceName, iDevNum2, ref lpDisplayDevice2, 1U); ++iDevNum2)
            {
              if ((lpDisplayDevice2.StateFlags & 1) == 1)
              {
                Console.WriteLine("{0}, {1}", (object) lpDisplayDevice2.DeviceName, (object) lpDisplayDevice2.StateFlags);
                IntPtr file = Win32.CreateFile(lpDisplayDevice2.DeviceID, 3221225472U, 1U, IntPtr.Zero, 3U, 0U, IntPtr.Zero);
                if (file != IntPtr.Zero)
                {
                  bool state = false;
                  Win32.GetDevicePowerState(file, out state);
                  if (state)
                  {
                    Win32.SendMessage(-1, 274, 61808, 2);
                  }
                  else
                  {
                    Win32.mouse_event(1, 0, 1, 0, 0);
                    Thread.Sleep(40);
                    Win32.mouse_event(1, 0, -1, 0, 0);
                  }
                  Win32.CloseHandle(file);
                }
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(string.Format("{0}", (object) ex.ToString()));
      }
    }

    private string getPNGName(int scancode)
    {
      switch (scancode)
      {
        case 4:
          return "touchpad_1.png";
        case 5:
          return "touchpad_0.png";
        case 26:
          return "wlan_1.png";
        case 27:
          return "wlan_0.png";
        case 49:
          return "power_1.png";
        case 50:
          return "power_0.png";
        case 64:
          return "OSD_home_lock.png";
        case 65:
          return "OSD_home_unlock.png";
        case 167:
          object data1 = (object) 0;
          WMIEC.WMIReadECRAM(1873UL, ref data1);
          return ((long) Convert.ToUInt64(data1) & 64L) == 64L ? "fanboost_on.png" : "fanboost_off.png";
        case 176:
          object data2 = (object) 0;
          WMIEC.WMIReadECRAM(1873UL, ref data2);
          return (byte) ((uint) (byte) (Convert.ToUInt64(data2) & (ulong) byte.MaxValue) & 192U) == (byte) 0 ? "gaming mode_296X62.png" : "office mode_296X62.png";
        default:
          return "";
      }
    }

    private void ShowVKOSD(int scancode)
    {
      string str;
      switch (scancode)
      {
        case 1:
          str = (((int) Win32.GetKeyState(20) & 1) == 1 ? 1 : 0) != 0 ? "capslock_1.png" : "capslock_0.png";
          break;
        case 2:
          str = (((int) Win32.GetKeyState(144) & 1) == 1 ? 1 : 0) != 0 ? "numlock_1.png" : "numlock_0.png";
          break;
        case 3:
          str = (((int) Win32.GetKeyState(145) & 1) == 1 ? 1 : 0) != 0 ? "scrolllock_1.png" : "scrolllock_0.png";
          break;
        default:
          return;
      }
      OSD osd = new OSD();
      osd.img.Source = (ImageSource) new BitmapImage(new Uri("pack://application:,,,/osdview/image/" + str));
      osd.img.Visibility = Visibility.Visible;
      osd.Visibility = Visibility.Visible;
    }

    public void ShowOSD(int scancode)
    {
      OSD osd = new OSD();
      osd.img.Source = (ImageSource) new BitmapImage(new Uri("pack://application:,,,/osdview/image/" + this.getPNGName(scancode)));
      osd.img.Visibility = Visibility.Visible;
      osd.Visibility = Visibility.Visible;
    }

    private void ShowBLOSD(int BL_Level)
    {
      string str;
      switch (BL_Level)
      {
        case 0:
          str = "OSD_KB_Light_4-01.png";
          break;
        case 1:
          str = "OSD_KB_Light_4-02.png";
          break;
        case 2:
          str = "OSD_KB_Light_4-03.png";
          break;
        case 3:
          str = "OSD_KB_Light_4-04.png";
          break;
        case 4:
          str = "OSD_KB_Light_4-05.png";
          break;
        default:
          str = "OSD_KB_Light_4-01.png";
          break;
      }
      OSD osd = new OSD();
      string uriString = "pack://application:,,,/osdview/image/" + str;
      osd.Width = osd.img_bk.Width;
      osd.Height = osd.img_bk.Height;
      osd.img_bk.Source = (ImageSource) new BitmapImage(new Uri(uriString));
      osd.img_bk.Visibility = Visibility.Visible;
      osd.Visibility = Visibility.Visible;
    }

    public void ScanCode_Handler(int scancode)
    {
      Application.Current.Dispatcher.Invoke((Action) (() =>
      {
        switch (scancode)
        {
          case 1:
          case 2:
          case 3:
            this.ShowVKOSD(scancode);
            break;
          case 4:
          case 5:
          case 26:
          case 27:
          case 49:
          case 50:
          case 64:
          case 65:
          case 167:
            this.ShowOSD(scancode);
            break;
          case 59:
            this.ShowBLOSD(0);
            break;
          case 60:
            this.ShowBLOSD(1);
            break;
          case 61:
            this.ShowBLOSD(2);
            break;
          case 62:
            this.ShowBLOSD(3);
            break;
          case 63:
            this.ShowBLOSD(4);
            break;
          case 169:
            this.SetPanelStatus();
            break;
        }
      }));
    }

    public void Event_LM(RGBKB_Event_Data event_data)
    {
      if (event_data.event_id != RGBKB_EventID.Brightness_update)
        return;
      int brightness = (int) event_data.event_data[0];
      Application.Current.Dispatcher.Invoke((Action) (() => this.ShowBLOSD(brightness)));
    }

    public void Start()
    {
      WMIEC.ScanCodeEvent += new WMIEC.ScanCode_EventHander(this.ScanCode_Handler);
      this.m_LM = new LM_Manager();
      this.m_LM.LM_Init(new RGBKB_Event_Handler(this.Event_LM));
    }

    public void Stop()
    {
      WMIEC.ScanCodeEvent -= new WMIEC.ScanCode_EventHander(this.ScanCode_Handler);
      if (this.m_LM == null)
        return;
      this.m_LM.LM_DeInit();
    }
  }
}
