// Decompiled with JetBrains decompiler
// Type: LightingModel.Audio
// Assembly: GamingCenter, Version=1.0.0.35, Culture=neutral, PublicKeyToken=null
// MVID: CF6E1F4E-70FD-4705-A8F7-86A4F7835797
// Assembly location: C:\Program Files\OEM\GamingCenter\GamingCenter.exe

using System;
using System.Runtime.InteropServices;

namespace LightingModel
{
  internal static class Audio
  {
    internal static IAudioEndpointVolume m_IAEV = (IAudioEndpointVolume) null;
    private static Audio.InitAudioC m_Init = new Audio.InitAudioC();
    private static float m_Volume = 0.0f;

    internal static float Volume
    {
      get
      {
        if (Audio.m_IAEV == null)
          return 0.0f;
        float pfLevel = -1f;
        Marshal.ThrowExceptionForHR(Audio.m_IAEV.GetMasterVolumeLevelScalar(out pfLevel));
        return pfLevel;
      }
      set
      {
        if (Audio.m_IAEV == null)
          return;
        Marshal.ThrowExceptionForHR(Audio.m_IAEV.SetMasterVolumeLevelScalar(value, Guid.Empty));
      }
    }

    internal static bool Muter
    {
      get
      {
        if (Audio.m_IAEV == null)
          return false;
        bool pbMute;
        Marshal.ThrowExceptionForHR(Audio.m_IAEV.GetMute(out pbMute));
        return pbMute;
      }
      set
      {
        if (Audio.m_IAEV == null)
          return;
        Marshal.ThrowExceptionForHR(Audio.m_IAEV.SetMute(value, Guid.Empty));
      }
    }

    internal static void Mute()
    {
      Audio.m_Volume = Audio.Volume;
      Audio.Volume = 0.0f;
    }

    internal static void UnMute()
    {
      Audio.Volume = Audio.m_Volume;
    }

    private class InitAudioC
    {
      internal InitAudioC()
      {
        try
        {
          IMMDeviceEnumerator deviceEnumerator = new MMDeviceEnumeratorComObject() as IMMDeviceEnumerator;
          IMMDevice mmDevice = (IMMDevice) null;
          ref IMMDevice local = ref mmDevice;
          Marshal.ThrowExceptionForHR(deviceEnumerator.GetDefaultAudioEndpoint(0, 1, out local));
          Guid guid = typeof (IAudioEndpointVolume).GUID;
          Marshal.ThrowExceptionForHR(mmDevice.Activate(ref guid, 23, 0, out Audio.m_IAEV));
        }
        catch
        {
        }
      }
    }
  }
}
