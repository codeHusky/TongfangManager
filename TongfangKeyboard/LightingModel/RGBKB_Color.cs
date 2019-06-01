// Decompiled with JetBrains decompiler
// Type: LightingModel.RGBKB_Color
// Assembly: GamingCenter, Version=1.0.0.35, Culture=neutral, PublicKeyToken=null
// MVID: CF6E1F4E-70FD-4705-A8F7-86A4F7835797
// Assembly location: C:\Program Files\OEM\GamingCenter\GamingCenter.exe

namespace LightingModel
{
  public struct RGBKB_Color
  {
    public bool isCircular;
    public uint ColorBlocks;
    public RGB_S[] ColorBuffer;

    public RGBKB_Color(bool bCircular, uint Length)
    {
      this.isCircular = bCircular;
      this.ColorBlocks = Length;
      this.ColorBuffer = new RGB_S[(int) Length];
      for (uint index = 0; index < Length; ++index)
      {
        this.ColorBuffer[(int) index].ID = index;
        this.ColorBuffer[(int) index].R = (byte) 0;
        this.ColorBuffer[(int) index].G = (byte) 0;
        this.ColorBuffer[(int) index].B = (byte) 0;
      }
    }
  }
}
