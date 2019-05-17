// Decompiled with JetBrains decompiler
// Type: Utility.URegistry
// Assembly: GamingCenter, Version=1.0.0.35, Culture=neutral, PublicKeyToken=null
// MVID: CF6E1F4E-70FD-4705-A8F7-86A4F7835797
// Assembly location: C:\Program Files\OEM\GamingCenter\GamingCenter.exe

using Microsoft.Win32;

namespace Utility
{
  public static class URegistry
  {
    public const string TreeKey = "SOFTWARE\\OEM\\GamingCenter\\";

    public static object RegistryValueRead(string SubKey, string Name, object defaultvalue)
    {
      try
      {
        return RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\OEM\\GamingCenter\\" + SubKey).GetValue(Name, defaultvalue);
      }
      catch
      {
        Log.s(LOG_LEVEL.ERROR, string.Format("Registry|RegistryValueRead Failed subkey ={0} Name={1}", (object) ("SOFTWARE\\OEM\\GamingCenter\\" + SubKey), (object) Name));
        return (object) 0;
      }
    }

    public static string[] RegistryKeyRead(string SubKey)
    {
      try
      {
        return RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey("SOFTWARE\\OEM\\GamingCenter\\" + SubKey).GetSubKeyNames();
      }
      catch
      {
        Log.s(LOG_LEVEL.ERROR, string.Format("Registry|RegistryKeyRead Failed subkey ={0}", (object) ("SOFTWARE\\OEM\\GamingCenter\\" + SubKey)));
        return (string[]) null;
      }
    }

    public static void RegistryValueWrite(
      string SubKey,
      string Name,
      object setvalue,
      RegistryValueKind valuekind)
    {
      try
      {
        RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).CreateSubKey("SOFTWARE\\OEM\\GamingCenter\\" + SubKey).SetValue(Name, setvalue, valuekind);
      }
      catch
      {
        Log.s(LOG_LEVEL.ERROR, string.Format("Registry|RegistryValueWrite Failed subkey ={0} Name={1} SetValue={2}", (object) ("SOFTWARE\\OEM\\GamingCenter\\" + SubKey), (object) Name, setvalue));
      }
    }
  }
}
