// Decompiled with JetBrains decompiler
// Type: Utility.WMIEC
// Assembly: GamingCenter, Version=1.0.0.35, Culture=neutral, PublicKeyToken=null
// MVID: CF6E1F4E-70FD-4705-A8F7-86A4F7835797
// Assembly location: C:\Program Files\OEM\GamingCenter\GamingCenter.exe

using System;
using System.Management;

namespace Utility
{
  public static class WMIEC
  {
    private static ManagementEventWatcher watcher = (ManagementEventWatcher) null;
    private static readonly WMIEC.Destructor finalObj = new WMIEC.Destructor();
    public static WMIEC.ScanCode_EventHander ScanCodeEvent;
    public static WMIEC.LM_ScanCode_EventHander LMScanCodeEvent;

    private static void WMIHandleEvent(object sender, EventArrivedEventArgs e)
    {
      try
      {
        int int32 = Convert.ToInt32(e.NewEvent.SystemProperties["ULong"].Value.ToString());
        WMIEC.ScanCode_EventHander scanCodeEvent = WMIEC.ScanCodeEvent;
        Log.s(LOG_LEVEL.TRACE, string.Format("WMIEC|event: " + scanCodeEvent.ToString()));
        if (scanCodeEvent != null)
          scanCodeEvent(int32);
        WMIEC.LM_ScanCode_EventHander lmScanCodeEvent = WMIEC.LMScanCodeEvent;
        if (lmScanCodeEvent == null)
          return;
        lmScanCodeEvent(int32);
      }
      catch
      {
      }
    }

    private static void StartWMIReceiveEvent(EventArrivedEventHandler WMIHandleEvent)
    {
      try
      {
        WMIEC.watcher = new ManagementEventWatcher(new ManagementScope("\\\\.\\Root\\WMI"), (EventQuery) new WqlEventQuery("SELECT * FROM AcpiTest_EventULong"));
        WMIEC.watcher.EventArrived += WMIHandleEvent;
        WMIEC.watcher.Start();
      }
      catch (ManagementException ex)
      {
        Log.s(LOG_LEVEL.ERROR, string.Format("WMIEC|StartWMIReceiveEvent : An error occurred while trying to receive an event: " + ex.Message));
      }
    }

    private static void EndWMIRecieveEvent()
    {
      if (WMIEC.watcher == null)
        return;
      WMIEC.watcher.Stop();
      WMIEC.watcher = (ManagementEventWatcher) null;
    }

    public static bool WMIReadECRAM(ulong Addr, ref object data)
    {
      try
      {
        ManagementObject managementObject = new ManagementObject("root\\WMI", "AcpiTest_MULong.InstanceName='ACPI\\PNP0C14\\1_1'", (ObjectGetOptions) null);
        ManagementBaseObject methodParameters = managementObject.GetMethodParameters("GetSetULong");
        Addr = 1099511627776UL + Addr;
        methodParameters["Data"] = (object) Addr;
        ManagementBaseObject managementBaseObject = managementObject.InvokeMethod("GetSetULong", methodParameters, (InvokeMethodOptions) null);
        data = managementBaseObject["Return"];
        return true;
      }
      catch (ManagementException ex)
      {
        Log.s(LOG_LEVEL.ERROR, string.Format("WMIEC|WMIReadECRAM : Failed" + ex.Message));
        return false;
      }
    }

    public static void WMIWriteECRAM(ulong Addr, ulong Value)
    {
      try
      {
        ManagementObject managementObject = new ManagementObject("root\\WMI", "AcpiTest_MULong.InstanceName='ACPI\\PNP0C14\\1_1'", (ObjectGetOptions) null);
        ManagementBaseObject methodParameters = managementObject.GetMethodParameters("GetSetULong");
        Value <<= 16;
        Addr = Value + Addr;
        methodParameters["Data"] = (object) Addr;
        managementObject.InvokeMethod("GetSetULong", methodParameters, (InvokeMethodOptions) null);
      }
      catch (ManagementException ex)
      {
        Log.s(LOG_LEVEL.ERROR, string.Format("WMIEC|WMIWriteECRAM : Failed" + ex.Message));
      }
    }

    public static void WMIWriteBiosRom(ulong Value)
    {
      ManagementObject managementObject = new ManagementObject("root\\WMI", "AcpiODM_Demo.InstanceName='ACPI\\PNP0C14\\2_0'", (ObjectGetOptions) null);
      ManagementBaseObject methodParameters = managementObject.GetMethodParameters("GetUlongEx7");
      methodParameters["Data"] = (object) Value;
      managementObject.InvokeMethod("GetUlongEx7", methodParameters, (InvokeMethodOptions) null);
    }

    static WMIEC()
    {
      WMIEC.StartWMIReceiveEvent(new EventArrivedEventHandler(WMIEC.WMIHandleEvent));
    }

    public delegate void ScanCode_EventHander(int scancode);

    public delegate void LM_ScanCode_EventHander(int scancode);

    private class Destructor
    {
      ~Destructor()
      {
        WMIEC.EndWMIRecieveEvent();
      }
    }
  }
}
