// Decompiled with JetBrains decompiler
// Type: Utility.Log
// Assembly: GamingCenter, Version=1.0.0.35, Culture=neutral, PublicKeyToken=null
// MVID: CF6E1F4E-70FD-4705-A8F7-86A4F7835797
// Assembly location: C:\Program Files\OEM\GamingCenter\GamingCenter.exe

using System;
using System.IO;
using System.Reflection;

namespace Utility
{
  public static class Log
  {
    private static readonly bool m_bEnable = false;
    private static bool m_logsEnabled = true;
    private static string m_logName = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName + "\\RGBKeyboard.log";

    static Log()
    {
      try
      {
        Log.m_bEnable = (int) URegistry.RegistryValueRead("RGBKeyboardView\\Debug", "enable", (object) 0) > 0;
      }
      catch
      {
        Log.m_bEnable = false;
        return;
      }
      if (!Log.m_bEnable)
        return;
      try
      {
        if ((int) URegistry.RegistryValueRead("RGBKeyboardView\\Debug", "create", (object) 0) > 0)
          File.Delete(Log.m_logName);
      }
      catch
      {
      }

      if (m_logsEnabled)
      {
          Log.s(LOG_LEVEL.INIT, "---------------------------------");
          Log.s(LOG_LEVEL.INIT, "Log|Log_Init : Start to log");
      }
    }

    public static void setLogEnabled(bool enabled)
    {
        m_logsEnabled = enabled;
    }

    public static void s(LOG_LEVEL log_level, string strLog)
    {
        if(! m_logsEnabled) return;
      string str = "[" + log_level.ToString() + "]" + strLog;
      if (Log.m_bEnable)
      {
        using (StreamWriter streamWriter = File.AppendText(Log.m_logName))
          streamWriter.WriteLine(str);
      }
      else
        Console.WriteLine(str);
    }

    public static void EnableLog(bool enalbe)
    {
    }
  }
}
