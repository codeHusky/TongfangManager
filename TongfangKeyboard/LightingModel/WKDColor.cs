// Decompiled with JetBrains decompiler
// Type: LightingModel.WKDColor
// Assembly: GamingCenter, Version=1.0.0.35, Culture=neutral, PublicKeyToken=null
// MVID: CF6E1F4E-70FD-4705-A8F7-86A4F7835797
// Assembly location: C:\Program Files\OEM\GamingCenter\GamingCenter.exe

using System;
using Utility;

namespace LightingModel
{
  public static class WKDColor
  {
    private const ulong m_SupportByte5 = 1858;
    private const byte m_bitLiteon = 4;
    private const byte m_bitEverlight = 8;
    private static byte m_LED_Vender;

    static WKDColor()
    {
      object data = (object) 0;
      WMIEC.WMIReadECRAM(1858UL, ref data);
      if (((long) Convert.ToUInt64(data) & 4L) == 4L)
      {
        WKDColor.m_LED_Vender = (byte) 4;
      }
      else
      {
        if (((long) Convert.ToUInt64(data) & 8L) != 8L)
          return;
        WKDColor.m_LED_Vender = (byte) 8;
      }
    }

    public static RGB_S cheatRGB_2ndME(byte R, byte G, byte B)
    {
      RGB_S rgbS = new RGB_S();
      switch (WKDColor.m_LED_Vender)
      {
        case 4:
          if (R == byte.MaxValue && G == byte.MaxValue && B == byte.MaxValue)
          {
            rgbS.R = byte.MaxValue;
            rgbS.G = (byte) 50;
            rgbS.B = (byte) 170;
            break;
          }
          if (R == (byte) 243 && G == (byte) 152 && B == (byte) 0)
          {
            rgbS.R = byte.MaxValue;
            rgbS.G = (byte) 19;
            rgbS.B = (byte) 0;
            break;
          }
          if (R == byte.MaxValue && G == (byte) 241 && B == (byte) 0)
          {
            rgbS.R = byte.MaxValue;
            rgbS.G = (byte) 50;
            rgbS.B = (byte) 0;
            break;
          }
          if (R == (byte) 0 && G == byte.MaxValue && B == byte.MaxValue)
          {
            rgbS.R = (byte) 0;
            rgbS.G = (byte) 50;
            rgbS.B = (byte) 170;
            break;
          }
          if (R == (byte) 138 && G == (byte) 0 && B == byte.MaxValue)
          {
            rgbS.R = (byte) 138;
            rgbS.G = (byte) 0;
            rgbS.B = (byte) 170;
            break;
          }
          if (R > (byte) 249 && G < (byte) 20 && B < (byte) 128)
          {
            if (B < (byte) 100)
            {
              rgbS.R = R;
              rgbS.G = G;
              rgbS.B = (byte) ((uint) B / 10U);
              break;
            }
            rgbS.R = R;
            rgbS.G = G;
            rgbS.B = (byte) ((uint) B - 10U);
            break;
          }
          if (R < (byte) 20 && (int) B < (int) G)
          {
            rgbS.R = R;
            rgbS.G = G;
            rgbS.B = (byte) (170 * (int) B / (int) byte.MaxValue);
            break;
          }
          if (G < (byte) 20 && (int) R < (int) B)
          {
            rgbS.R = R;
            rgbS.G = G;
            rgbS.B = B;
            break;
          }
          rgbS.R = R;
          rgbS.G = (byte) (50 * (int) G / (int) byte.MaxValue);
          rgbS.B = (byte) (170 * (int) B / (int) byte.MaxValue);
          break;
        case 8:
          if (R == byte.MaxValue && G == byte.MaxValue && B == byte.MaxValue)
          {
            rgbS.R = byte.MaxValue;
            rgbS.G = (byte) 70;
            rgbS.B = (byte) 80;
            break;
          }
          if (R == (byte) 243 && G == (byte) 152 && B == (byte) 0)
          {
            rgbS.R = byte.MaxValue;
            rgbS.G = (byte) 25;
            rgbS.B = (byte) 0;
            break;
          }
          if (R == byte.MaxValue && G == (byte) 241 && B == (byte) 0)
          {
            rgbS.R = byte.MaxValue;
            rgbS.G = (byte) 70;
            rgbS.B = (byte) 0;
            break;
          }
          if (R == (byte) 0 && G == byte.MaxValue && B == byte.MaxValue)
          {
            rgbS.R = (byte) 0;
            rgbS.G = (byte) 70;
            rgbS.B = (byte) 80;
            break;
          }
          if (R == (byte) 138 && G == (byte) 0 && B == byte.MaxValue)
          {
            rgbS.R = (byte) 138;
            rgbS.G = (byte) 0;
            rgbS.B = (byte) 80;
            break;
          }
          if (R > (byte) 249 && G < (byte) 20 && B < (byte) 128)
          {
            if (B < (byte) 100)
            {
              rgbS.R = R;
              rgbS.G = G;
              rgbS.B = (byte) ((uint) B / 10U);
              break;
            }
            rgbS.R = R;
            rgbS.G = G;
            rgbS.B = (byte) ((uint) B - 10U);
            break;
          }
          if (R < (byte) 20 && (int) B < (int) G)
          {
            rgbS.R = R;
            rgbS.G = G;
            rgbS.B = (byte) (80 * (int) B / (int) byte.MaxValue);
            break;
          }
          if (G < (byte) 20 && (int) R < (int) B)
          {
            rgbS.R = R;
            rgbS.G = G;
            rgbS.B = B;
            break;
          }
          rgbS.R = R;
          rgbS.G = (byte) (70 * (int) G / (int) byte.MaxValue);
          rgbS.B = (byte) (80 * (int) B / (int) byte.MaxValue);
          break;
        default:
          if (R == byte.MaxValue && G == byte.MaxValue && B == byte.MaxValue)
          {
            rgbS.R = byte.MaxValue;
            rgbS.G = (byte) 180;
            rgbS.B = (byte) 200;
            break;
          }
          if (R == (byte) 243 && G == (byte) 152 && B == (byte) 0)
          {
            rgbS.R = byte.MaxValue;
            rgbS.G = (byte) 42;
            rgbS.B = (byte) 0;
            break;
          }
          if (R == byte.MaxValue && G == (byte) 241 && B == (byte) 0)
          {
            rgbS.R = byte.MaxValue;
            rgbS.G = (byte) 180;
            rgbS.B = (byte) 0;
            break;
          }
          if (R == (byte) 0 && G == byte.MaxValue && B == byte.MaxValue)
          {
            rgbS.R = (byte) 0;
            rgbS.G = (byte) 180;
            rgbS.B = (byte) 200;
            break;
          }
          if (R == (byte) 138 && G == (byte) 0 && B == byte.MaxValue)
          {
            rgbS.R = (byte) 138;
            rgbS.G = (byte) 0;
            rgbS.B = (byte) 200;
            break;
          }
          if (R > (byte) 249 && G < (byte) 20 && B < (byte) 128)
          {
            if (B < (byte) 100)
            {
              rgbS.R = R;
              rgbS.G = G;
              rgbS.B = (byte) ((uint) B / 10U);
              break;
            }
            rgbS.R = R;
            rgbS.G = G;
            rgbS.B = (byte) ((uint) B - 10U);
            break;
          }
          if (R < (byte) 20 && (int) B < (int) G)
          {
            rgbS.R = R;
            rgbS.G = G;
            rgbS.B = (byte) (200 * (int) B / (int) byte.MaxValue);
            break;
          }
          if (G < (byte) 20 && (int) R < (int) B)
          {
            rgbS.R = R;
            rgbS.G = G;
            rgbS.B = B;
            break;
          }
          rgbS.R = R;
          rgbS.G = (byte) (180 * (int) G / (int) byte.MaxValue);
          rgbS.B = (byte) (200 * (int) B / (int) byte.MaxValue);
          break;
      }
      return rgbS;
    }

    public static RGB_S cheatRGB_4Zone(byte R, byte G, byte B)
    {
      RGB_S rgbS = new RGB_S();
      /*if (R == (byte) 243 && G == (byte) 152 && B == (byte) 0)
      {
        rgbS.R = byte.MaxValue;
        rgbS.G = (byte) 60;
        rgbS.B = (byte) 0;
      }
      else if (R == byte.MaxValue && G == (byte) 241 && B == (byte) 0)
      {
        rgbS.R = byte.MaxValue;
        rgbS.G = (byte) 180;
        rgbS.B = (byte) 0;
      }
      else
      {*/
        rgbS.R = R;
        //rgbS.G = (byte) (180 * (int) G / (int) byte.MaxValue);
        rgbS.G = G;
        rgbS.B = B;
      //}
      return rgbS;
    }
  }
}
