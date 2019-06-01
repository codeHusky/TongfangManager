// Decompiled with JetBrains decompiler
// Type: LightingModel.RGBKB_Event_Data
// Assembly: GamingCenter, Version=1.0.0.35, Culture=neutral, PublicKeyToken=null
// MVID: CF6E1F4E-70FD-4705-A8F7-86A4F7835797
// Assembly location: C:\Program Files\OEM\GamingCenter\GamingCenter.exe

namespace LightingModel
{
  public struct RGBKB_Event_Data
  {
    public RGBKB_EventID event_id;
    public uint envet_data_len;
    public byte[] event_data;
  }
}
