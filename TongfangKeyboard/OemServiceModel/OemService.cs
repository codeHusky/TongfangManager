// Decompiled with JetBrains decompiler
// Type: OemServiceModel.OemService
// Assembly: GamingCenter, Version=1.0.0.35, Culture=neutral, PublicKeyToken=null
// MVID: CF6E1F4E-70FD-4705-A8F7-86A4F7835797
// Assembly location: C:\Program Files\OEM\GamingCenter\GamingCenter.exe

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace OemServiceModel
{
  public static class OemService
  {
    [DllImport("OemServiceWinApp.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
    public static extern bool OemSvcHook(
      int lenCmd,
      string[] bufferCmd,
      IntPtr buuferRead,
      int ReadSize);

    public static void Exec(int lenCmd, string[] bufferCmd, int lenRead, byte[] bufferRead)
    {
      try
      {
        IntPtr num = Marshal.AllocHGlobal(lenRead);
        OemService.OemSvcHook(lenCmd, bufferCmd, num, lenRead);
        Marshal.Copy(num, bufferRead, 0, lenRead);
        Marshal.FreeHGlobal(num);
      }
      catch
      {
      }
    }

    public static string Read(string cmd)
    {
      try
      {
        Process process = new Process();
        ProcessStartInfo processStartInfo = new ProcessStartInfo();
        processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        processStartInfo.FileName = "OemServiceWinApp.exe";
        processStartInfo.Arguments = cmd;
        processStartInfo.RedirectStandardOutput = true;
        processStartInfo.WorkingDirectory = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo = processStartInfo;
        process.Start();
        string end = process.StandardOutput.ReadToEnd();
        process.WaitForExit(5000);
        return end;
      }
      catch
      {
        return (string) null;
      }
    }

    public static void Write(string cmd)
    {
      try
      {
        Process process = new Process();
        process.StartInfo = new ProcessStartInfo()
        {
          WindowStyle = ProcessWindowStyle.Hidden,
          FileName = "OemServiceWinApp.exe",
          Arguments = cmd,
          WorkingDirectory = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName
        };
        process.Start();
        process.WaitForExit(5000);
      }
      catch
      {
      }
    }
  }
}
