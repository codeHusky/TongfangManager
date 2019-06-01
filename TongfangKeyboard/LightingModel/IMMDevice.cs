// Decompiled with JetBrains decompiler
// Type: LightingModel.IMMDevice
// Assembly: GamingCenter, Version=1.0.0.35, Culture=neutral, PublicKeyToken=null
// MVID: CF6E1F4E-70FD-4705-A8F7-86A4F7835797
// Assembly location: C:\Program Files\OEM\GamingCenter\GamingCenter.exe

using System;
using System.Runtime.InteropServices;

namespace LightingModel
{
  [Guid("D666063F-1587-4E43-81F1-B948E807363F")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IMMDevice
  {
    int Activate(ref Guid id, int clsCtx, int activationParams, out IAudioEndpointVolume aev);
  }
}
