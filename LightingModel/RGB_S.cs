// Decompiled with JetBrains decompiler
// Type: LightingModel.RGB_S
// Assembly: GamingCenter, Version=1.0.0.35, Culture=neutral, PublicKeyToken=null
// MVID: CF6E1F4E-70FD-4705-A8F7-86A4F7835797
// Assembly location: C:\Program Files\OEM\GamingCenter\GamingCenter.exe

namespace LightingModel
{
  public struct RGB_S
  {
    public uint ID;
    public byte R;
    public byte G;
    public byte B;

    public RGB_S(uint _id, byte _r, byte _g, byte _b)
    {
      this.ID = _id;
      this.R = _r;
      this.G = _g;
      this.B = _b;
    }
  }
}
