// Decompiled with JetBrains decompiler
// Type: LightingModel.IMMDeviceEnumerator
// Assembly: GamingCenter, Version=1.0.0.35, Culture=neutral, PublicKeyToken=null
// MVID: CF6E1F4E-70FD-4705-A8F7-86A4F7835797
// Assembly location: C:\Program Files\OEM\GamingCenter\GamingCenter.exe

using System.Runtime.InteropServices;

namespace LightingModel
{
  [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IMMDeviceEnumerator
  {
    int fun1();

    int GetDefaultAudioEndpoint(int dataFlow, int role, out IMMDevice endpoint);
  }
}
