// Decompiled with JetBrains decompiler
// Type: LightingModel.MusicMode
// Assembly: GamingCenter, Version=1.0.0.35, Culture=neutral, PublicKeyToken=null
// MVID: CF6E1F4E-70FD-4705-A8F7-86A4F7835797
// Assembly location: C:\Program Files\OEM\GamingCenter\GamingCenter.exe

using System;
using System.Runtime.InteropServices;

namespace LightingModel
{
  public static class MusicMode
  {
    internal static IntPtr m_hAudio = MusicMode.LoadLibrary("audiostealer.dll");
    internal static IntPtr m_pStartMonitorAudio = MusicMode.GetProcAddress(MusicMode.m_hAudio, "StartMonitorAudio");
    internal static IntPtr m_pStopMonitorAudio = MusicMode.GetProcAddress(MusicMode.m_hAudio, "StopMonitorAudio");
    internal static MusicMode.STARTMONITORAUDIO m_delStartMonitorAudio = null;//(MusicMode.STARTMONITORAUDIO) Marshal.GetDelegateForFunctionPointer(MusicMode.m_pStartMonitorAudio, typeof (MusicMode.STARTMONITORAUDIO));
    internal static MusicMode.STOPMONITORAUDIO m_delStopMonitorAudio = null;//(MusicMode.STOPMONITORAUDIO) Marshal.GetDelegateForFunctionPointer(MusicMode.m_pStopMonitorAudio, typeof (MusicMode.STOPMONITORAUDIO));

    [DllImport("kernel32.dll")]
    internal static extern IntPtr LoadLibrary(string dllToLoad);

    [DllImport("kernel32.dll")]
    internal static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

    [DllImport("kernel32.dll")]
    internal static extern bool FreeLibrary(IntPtr hModule);

    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    internal delegate bool STARTMONITORAUDIO(ushort nParam, byte bLight);

    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    internal delegate void STOPMONITORAUDIO();
  }
}
