// Decompiled with JetBrains decompiler
// Type: OSDView.Win32
// Assembly: GamingCenter, Version=1.0.0.35, Culture=neutral, PublicKeyToken=null
// MVID: CF6E1F4E-70FD-4705-A8F7-86A4F7835797
// Assembly location: C:\Program Files\OEM\GamingCenter\GamingCenter.exe

using System;
using System.Runtime.InteropServices;

namespace OSDView
{
  internal static class Win32
  {
    internal const int DISPLAY_DEVICE_ACTIVE = 1;
    internal const int DISPLAY_DEVICE_ATTACHED = 2;
    internal const int EDD_GET_DEVICE_INTERFACE_NAME = 1;
    internal const int AttachedToDesktop = 1;
    internal const int MultiDriver = 2;
    internal const int PrimaryDevice = 4;
    internal const int MirroringDriver = 8;
    internal const int VGACompatible = 16;
    internal const int Removable = 32;
    internal const int ModesPruned = 134217728;
    internal const int Remote = 67108864;
    internal const int Disconnect = 33554432;
    internal const uint GENERIC_READ = 2147483648;
    internal const uint GENERIC_WRITE = 1073741824;
    internal const uint FILE_SHARE_WRITE = 2;
    internal const uint FILE_SHARE_READ = 1;
    internal const uint FILE_FLAG_OVERLAPPED = 1073741824;
    internal const uint OPEN_EXISTING = 3;
    internal const uint OPEN_ALWAYS = 4;
    internal const int WM_SYSCOMMAND = 274;
    internal const int SC_MONITORPOWER = 61808;
    internal const int MONITOR_ON = -1;
    internal const int MONITOR_STANBY = 1;
    internal const int MONITOR_OFF = 2;
    internal const int MOUSEEVENTF_MOVE = 1;

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern short GetKeyState(int keyCode);

    [DllImport("user32.dll")]
    internal static extern bool EnumDisplayDevices(
      string lpDevice,
      uint iDevNum,
      ref Win32.DISPLAY_DEVICE lpDisplayDevice,
      uint dwFlags);

    [DllImport("user32.dll")]
    internal static extern int SendMessage(int hwnd, int wMsg, int wParam, int lParam);

    [DllImport("user32")]
    internal static extern void mouse_event(
      int dwFlags,
      int dx,
      int dy,
      int dwData,
      int dwExtraInfo);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern IntPtr CreateFile(
      [MarshalAs(UnmanagedType.LPStr)] string strName,
      uint nAccess,
      uint nShareMode,
      IntPtr lpSecurity,
      uint nCreationFlags,
      uint nAttributes,
      IntPtr lpTemplate);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern bool CloseHandle(IntPtr hObject);

    [DllImport("kernel32.dll", SetLastError = true)]
    internal static extern bool GetDevicePowerState(IntPtr handle, out bool state);

    internal struct DISPLAY_DEVICE
    {
      [MarshalAs(UnmanagedType.U4)]
      internal int cb;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
      internal string DeviceName;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
      internal string DeviceString;
      [MarshalAs(UnmanagedType.U4)]
      internal int StateFlags;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
      internal string DeviceID;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
      internal string DeviceKey;
    }
  }
}
