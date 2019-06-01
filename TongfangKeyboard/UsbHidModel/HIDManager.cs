// Decompiled with JetBrains decompiler
// Type: UsbHidModel.HIDManager
// Assembly: GamingCenter, Version=1.0.0.35, Culture=neutral, PublicKeyToken=null
// MVID: CF6E1F4E-70FD-4705-A8F7-86A4F7835797
// Assembly location: C:\Program Files\OEM\GamingCenter\GamingCenter.exe

using System;
using System.Runtime.InteropServices;
using Utility;

namespace UsbHidModel
{
    public class HIDManager : HIDNativeMethods
  {
    public IntPtr m_Handle;
    public int m_FeatureSize;
    public ushort m_UsagePage;

    private static string GetDevicePath(
      IntPtr hInfoSet,
      ref HIDNativeMethods.DeviceInterfaceData oInterface)
    {
      uint nRequiredSize = 0;
      if (!HIDNativeMethods.SetupDiGetDeviceInterfaceDetail(hInfoSet, ref oInterface, IntPtr.Zero, 0U, ref nRequiredSize, IntPtr.Zero))
        Log.s(LOG_LEVEL.ERROR, "HID_Manager|GetDevicePath : SetupDiGetDeviceInterfaceDetail failed");
      HIDNativeMethods.DeviceInterfaceDetailData oDetailData = new HIDNativeMethods.DeviceInterfaceDetailData();
      oDetailData.Size = Marshal.SizeOf(typeof (IntPtr)) == 8 ? 8 : 5;
      if (!HIDNativeMethods.SetupDiGetDeviceInterfaceDetail(hInfoSet, ref oInterface, ref oDetailData, nRequiredSize, ref nRequiredSize, IntPtr.Zero))
        Log.s(LOG_LEVEL.ERROR, "HID_Manager|GetDevicePath : SetupDiGetDeviceInterfaceDetail failed");
      return oDetailData.DevicePath;
    }

    public bool Init(ushort VID, ushort PID, ushort USAGE)
    {
      try
      {
        Guid gHid;
        HIDNativeMethods.HidD_GetHidGuid(out gHid);
        IntPtr classDevs = HIDNativeMethods.SetupDiGetClassDevs(ref gHid, (string) null, IntPtr.Zero, 18U);
        if (classDevs == HIDNativeMethods.InvalidHandleValue)
        {
          Log.s(LOG_LEVEL.ERROR, "HID_Manager|Init : SetupDiGetClassDevs failed");
          return false;
        }
        HIDNativeMethods.DeviceInterfaceData structure = new HIDNativeMethods.DeviceInterfaceData();
        structure.Size = Marshal.SizeOf<HIDNativeMethods.DeviceInterfaceData>(structure);
        int num;
        for (num = 0; HIDNativeMethods.SetupDiEnumDeviceInterfaces(classDevs, 0U, ref gHid, (uint) num, ref structure); ++num)
        {
          IntPtr file = HIDNativeMethods.CreateFile(HIDManager.GetDevicePath(classDevs, ref structure), 3221225472U, 3U, IntPtr.Zero, 3U, 1073741824U, IntPtr.Zero);
          if (file != HIDNativeMethods.InvalidHandleValue)
          {
            HIDNativeMethods.HIDD_ATTRIBUTES attributes = new HIDNativeMethods.HIDD_ATTRIBUTES();
            attributes.Size = (uint) Marshal.SizeOf<HIDNativeMethods.HIDD_ATTRIBUTES>(attributes);
            if (!HIDNativeMethods.HidD_GetAttributes(file, ref attributes))
              Log.s(LOG_LEVEL.ERROR, "HID_Manager|Init : HidD_GetAttributes failed");
            Log.s(LOG_LEVEL.INIT, string.Format("HID_Manager|Init : PID ={0} VID={1}", (object) attributes.ProductID, (object) attributes.VendorID));
            IntPtr lpData;
            if ((int) attributes.ProductID == (int) PID && (int) attributes.VendorID == (int) VID && HIDNativeMethods.HidD_GetPreparsedData(file, out lpData))
            {
              HIDNativeMethods.HidCaps oCaps;
              if (HIDNativeMethods.HidP_GetCaps(lpData, out oCaps) != 1114112)
              {
                Log.s(LOG_LEVEL.ERROR, "HID_Manager|Init HidP_GetCaps failed");
              }
              else
              {
                Log.s(LOG_LEVEL.INIT, string.Format("HID_Manager|Init :  usageid={0:x} usagepage={1:x} InputReportLen={2} OutputReportLen={3} FeatureReportLen{4}", (object) oCaps.Usage, (object) oCaps.UsagePage, (object) oCaps.InputReportByteLength, (object) oCaps.OutputReportByteLength, (object) oCaps.FeatureReportByteLength));
                if ((int) oCaps.Usage == (int) USAGE)
                {
                  this.m_Handle = file;
                  this.m_UsagePage = oCaps.UsagePage;
                  this.m_FeatureSize = (int) oCaps.FeatureReportByteLength;
                  if (!HIDNativeMethods.SetupDiDestroyDeviceInfoList(classDevs))
                    Log.s(LOG_LEVEL.ERROR, "HID_Manager|Init : SetupDiDestroyDeviceInfoList failed ");
                  return true;
                }
              }
            }
          }
          HIDNativeMethods.CloseHandle(file);
        }
        if (!HIDNativeMethods.SetupDiDestroyDeviceInfoList(classDevs))
          Log.s(LOG_LEVEL.ERROR, "HID_Manager|Init : SetupDiDestroyDeviceInfoList failed ");
        Log.s(LOG_LEVEL.INIT, string.Format("HID_Manager|Init :  Find HID Device Interface = {0}", (object) num));
        return false;
      }
      catch (Exception ex)
      {
        Log.s(LOG_LEVEL.ERROR, string.Format("HID_Manager|Init : Failed {0}", (object) ex.ToString()));
        return false;
      }
    }

    public void Deinit()
    {
      if (!(this.m_Handle != IntPtr.Zero))
        return;
      HIDNativeMethods.CloseHandle(this.m_Handle);
    }

    public ushort GetUsagePage()
    {
      return this.m_UsagePage;
    }

    public bool WriteFeature(byte[] buffer)
    {
      return HIDNativeMethods.HidD_SetFeature(this.m_Handle, buffer, this.m_FeatureSize);
    }

    public bool GetFeature(byte[] buffer)
    {
      return HIDNativeMethods.HidD_GetFeature(this.m_Handle, buffer, this.m_FeatureSize);
    }
  }
}
