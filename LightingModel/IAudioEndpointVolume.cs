// Decompiled with JetBrains decompiler
// Type: LightingModel.IAudioEndpointVolume
// Assembly: GamingCenter, Version=1.0.0.35, Culture=neutral, PublicKeyToken=null
// MVID: CF6E1F4E-70FD-4705-A8F7-86A4F7835797
// Assembly location: C:\Program Files\OEM\GamingCenter\GamingCenter.exe

using System;
using System.Runtime.InteropServices;

namespace LightingModel
{
  [Guid("5CDF2C82-841E-4546-9722-0CF74078229A")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  internal interface IAudioEndpointVolume
  {
    int fun1();

    int fun2();

    int fun3();

    int fun4();

    int SetMasterVolumeLevelScalar(float fLevel, Guid pguidEventContext);

    int fun5();

    int GetMasterVolumeLevelScalar(out float pfLevel);

    int fun6();

    int fun7();

    int fun8();

    int fun9();

    int SetMute([MarshalAs(UnmanagedType.Bool)] bool bMute, Guid pguidEventContext);

    int GetMute(out bool pbMute);
  }
}
