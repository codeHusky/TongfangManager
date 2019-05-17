// Decompiled with JetBrains decompiler
// Type: OSDView.OSD
// Assembly: GamingCenter, Version=1.0.0.35, Culture=neutral, PublicKeyToken=null
// MVID: CF6E1F4E-70FD-4705-A8F7-86A4F7835797
// Assembly location: C:\Program Files\OEM\GamingCenter\GamingCenter.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Threading;

namespace OSDView
{
  public partial class OSD : Window, IComponentConnector
  {

      [DllImport("user32.dll")]
    public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

    public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
    {
      IntPtr num1 = IntPtr.Zero;
      OSD.SetLastError(0);
      int lastWin32Error;
      if (IntPtr.Size == 4)
      {
        int num2 = OSD.IntSetWindowLong(hWnd, nIndex, OSD.IntPtrToInt32(dwNewLong));
        lastWin32Error = Marshal.GetLastWin32Error();
        num1 = new IntPtr(num2);
      }
      else
      {
        num1 = OSD.IntSetWindowLongPtr(hWnd, nIndex, dwNewLong);
        lastWin32Error = Marshal.GetLastWin32Error();
      }
      if (num1 == IntPtr.Zero && lastWin32Error != 0)
        throw new Win32Exception(lastWin32Error);
      return num1;
    }

    [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
    private static extern IntPtr IntSetWindowLongPtr(
      IntPtr hWnd,
      int nIndex,
      IntPtr dwNewLong);

    [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
    private static extern int IntSetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    private static int IntPtrToInt32(IntPtr intPtr)
    {
      return (int) intPtr.ToInt64();
    }

    [DllImport("kernel32.dll")]
    public static extern void SetLastError(int dwErrorCode);

    public OSD()
    {
      this.InitializeComponent();
      this.StartCloseTimer();
    }

    private void StartCloseTimer()
    {
      DispatcherTimer dispatcherTimer = new DispatcherTimer();
      dispatcherTimer.Interval = TimeSpan.FromSeconds(3.0);
      dispatcherTimer.Tick += new EventHandler(this.TimerTick);
      dispatcherTimer.Start();
    }

    private void TimerTick(object sender, EventArgs e)
    {
      DispatcherTimer dispatcherTimer = (DispatcherTimer) sender;
      dispatcherTimer.Stop();
      dispatcherTimer.Tick -= new EventHandler(this.TimerTick);
      this.Close();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      WindowInteropHelper windowInteropHelper = new WindowInteropHelper((Window) this);
      OSD.SetWindowLong(windowInteropHelper.Handle, -20, (IntPtr) ((int) OSD.GetWindowLong(windowInteropHelper.Handle, -20) | 128 | 134217728));
    }

    [Flags]
    public enum ExtendedWindowStyles
    {
      WS_EX_TOOLWINDOW = 128, // 0x00000080
      WS_EX_NOACTIVATE = 134217728, // 0x08000000
    }

    public enum GetWindowLongFields
    {
      GWL_EXSTYLE = -20, // -0x00000014
    }
  }
}
