// Decompiled with JetBrains decompiler
// Type: UsbHidModel.HIDNativeMethods
// Assembly: GamingCenter, Version=1.0.0.35, Culture=neutral, PublicKeyToken=null
// MVID: CF6E1F4E-70FD-4705-A8F7-86A4F7835797
// Assembly location: C:\Program Files\OEM\GamingCenter\GamingCenter.exe

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace UsbHidModel
{
    public class HIDNativeMethods
  {
    public static IntPtr NullHandle = IntPtr.Zero;
    public static IntPtr InvalidHandleValue = new IntPtr(-1);
    public const int DIGCF_PRESENT = 2;
    public const int DIGCF_DEVICEINTERFACE = 16;
    public const int HIDP_STATUS_SUCCESS = 1114112;
    public const int HIDP_STATUS_NULL = -2146369535;
    public const int HIDP_STATUS_INVALID_PREPARSED_DATA = -1072627711;
    public const int HIDP_STATUS_INVALID_REPORT_TYPE = -1072627710;
    public const int HIDP_STATUS_INVALID_REPORT_LENGTH = -1072627709;
    public const int HIDP_STATUS_USAGE_NOT_FOUND = -1072627708;
    public const int HIDP_STATUS_VALUE_OUT_OF_RANGE = -1072627707;
    public const int HIDP_STATUS_BAD_LOG_PHY_VALUES = -1072627706;
    public const int HIDP_STATUS_BUFFER_TOO_SMALL = -1072627705;
    public const int HIDP_STATUS_INTERNAL_ERROR = -1072627704;
    public const int HIDP_STATUS_I8042_TRANS_UNKNOWN = -1072627703;
    public const int HIDP_STATUS_INCOMPATIBLE_REPORT_ID = -1072627702;
    public const int HIDP_STATUS_NOT_VALUE_ARRAY = -1072627701;
    public const int HIDP_STATUS_IS_VALUE_ARRAY = -1072627700;
    public const int HIDP_STATUS_DATA_INDEX_NOT_FOUND = -1072627699;
    public const int HIDP_STATUS_DATA_INDEX_OUT_OF_RANGE = -1072627698;
    public const int HIDP_STATUS_BUTTON_NOT_PRESSED = -1072627697;
    public const int HIDP_STATUS_REPORT_DOES_NOT_EXIST = -1072627696;
    public const int HIDP_STATUS_NOT_IMPLEMENTED = -1072627680;
    public const uint GENERIC_READ = 2147483648;
    public const uint GENERIC_WRITE = 1073741824;
    public const uint FILE_SHARE_WRITE = 2;
    public const uint FILE_SHARE_READ = 1;
    public const uint FILE_FLAG_OVERLAPPED = 1073741824;
    public const uint OPEN_EXISTING = 3;
    public const uint OPEN_ALWAYS = 4;

    [DllImport("setupapi.dll", SetLastError = true)]
    public static extern bool SetupDiGetDeviceInterfaceDetail(
      IntPtr lpDeviceInfoSet,
      ref HIDNativeMethods.DeviceInterfaceData oInterfaceData,
      IntPtr lpDeviceInterfaceDetailData,
      uint nDeviceInterfaceDetailDataSize,
      ref uint nRequiredSize,
      IntPtr lpDeviceInfoData);

    [DllImport("setupapi.dll", SetLastError = true)]
    public static extern bool SetupDiGetDeviceInterfaceDetail(
      IntPtr lpDeviceInfoSet,
      ref HIDNativeMethods.DeviceInterfaceData oInterfaceData,
      ref HIDNativeMethods.DeviceInterfaceDetailData oDetailData,
      uint nDeviceInterfaceDetailDataSize,
      ref uint nRequiredSize,
      IntPtr lpDeviceInfoData);

    [DllImport("setupapi.dll", SetLastError = true)]
    public static extern bool SetupDiEnumDeviceInterfaces(
      IntPtr lpDeviceInfoSet,
      uint nDeviceInfoData,
      ref Guid gClass,
      uint nIndex,
      ref HIDNativeMethods.DeviceInterfaceData oInterfaceData);

    [DllImport("setupapi.dll", SetLastError = true)]
    public static extern bool SetupDiDestroyDeviceInfoList(IntPtr lpInfoSet);

    [DllImport("setupapi.dll", SetLastError = true)]
    public static extern IntPtr SetupDiGetClassDevs(
      ref Guid gClass,
      [MarshalAs(UnmanagedType.LPStr)] string strEnumerator,
      IntPtr hParent,
      uint nFlags);

    [DllImport("hid.dll", SetLastError = true)]
    public static extern void HidD_GetHidGuid(out Guid gHid);

    [DllImport("hid.dll", SetLastError = true)]
    public static extern bool HidD_GetPreparsedData(IntPtr hFile, out IntPtr lpData);

    [DllImport("hid.dll", SetLastError = true)]
    public static extern bool HidD_FreePreparsedData(int lData);

    [DllImport("hid.dll", SetLastError = true)]
    public static extern int HidP_GetCaps(IntPtr lpData, out HIDNativeMethods.HidCaps oCaps);

    [DllImport("hid.dll", SetLastError = true)]
    public static extern bool HidD_FreePreparsedData(ref IntPtr pData);

    [DllImport("hid.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool HidD_GetManufacturerString(
      IntPtr hFile,
      StringBuilder buffer,
      int bufferLength);

    [DllImport("hid.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool HidD_GetProductString(
      IntPtr hFile,
      StringBuilder buffer,
      int bufferLength);

    [DllImport("hid.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static extern bool HidD_GetSerialNumberString(
      IntPtr hDevice,
      StringBuilder buffer,
      int bufferLength);

    [DllImport("hid.dll", SetLastError = true)]
    public static extern bool HidD_GetAttributes(
      IntPtr hFile,
      ref HIDNativeMethods.HIDD_ATTRIBUTES attributes);

    [DllImport("hid.dll", SetLastError = true)]
    public static extern bool HidD_SetOutputReport(
      IntPtr hFile,
      byte[] lpReportBuffer,
      int reportBufferLength);

    [DllImport("hid.dll", SetLastError = true)]
    public static extern bool HidD_GetFeature(
      IntPtr hFile,
      byte[] lpReportBuffer,
      int reportBufferLengths);

    [DllImport("hid.dll", SetLastError = true)]
    public static extern bool HidD_SetFeature(
      IntPtr hFile,
      byte[] lpReportBuffer,
      int reportBufferLengths);

    [DllImport("hid.dll", SetLastError = true)]
    public static extern int HidP_GetButtonCaps(
      HIDNativeMethods.HIDP_REPORT_TYPE reportType,
      [In, Out] HIDNativeMethods.HidP_Button_Caps[] buttonCaps,
      ref short buttonCapsLength,
      IntPtr preparsedData);

    [DllImport("hid.dll", SetLastError = true)]
    public static extern int HidP_GetValueCaps(
      HIDNativeMethods.HIDP_REPORT_TYPE reportType,
      [In, Out] HIDNativeMethods.HidP_Value_Caps[] valueCaps,
      ref short valueCapsLength,
      IntPtr preparsedData);

    [DllImport("hid.dll", SetLastError = true)]
    public static extern int HidP_InitializeReportForID(
      HIDNativeMethods.HIDP_REPORT_TYPE reportType,
      byte reportID,
      IntPtr preparsedData,
      [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] report,
      int reportLength);

    [DllImport("hid.dll", SetLastError = true)]
    public static extern int HidP_SetUsages(
      HIDNativeMethods.HIDP_REPORT_TYPE reportType,
      short usagePage,
      short linkCollection,
      [In, Out] HIDNativeMethods.HIDP_DATA[] usageList,
      ref int usageLength,
      IntPtr preparsedData,
      [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] report,
      int reportLength);

    [DllImport("hid.dll", SetLastError = true)]
    public static extern int HidP_SetData(
      HIDNativeMethods.HIDP_REPORT_TYPE reportType,
      [In, Out] HIDNativeMethods.HIDP_DATA[] dataList,
      ref int dataLength,
      IntPtr preparsedData,
      [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)] byte[] report,
      int reportLength);

    [DllImport("hid.dll", SetLastError = true)]
    public static extern int HidP_GetData(
      HIDNativeMethods.HIDP_REPORT_TYPE reportType,
      [In, Out] HIDNativeMethods.HIDP_DATA[] dataList,
      ref int dataLength,
      IntPtr preparsedData,
      [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)] byte[] report,
      int reportLength);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr CreateFile(
      [MarshalAs(UnmanagedType.LPStr)] string strName,
      uint nAccess,
      uint nShareMode,
      IntPtr lpSecurity,
      uint nCreationFlags,
      uint nAttributes,
      IntPtr lpTemplate);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool CloseHandle(IntPtr hObject);

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DeviceInterfaceData
    {
      public int Size;
      public Guid InterfaceClassGuid;
      public int Flags;
      public IntPtr Reserved;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct DeviceInterfaceDetailData
    {
      public int Size;
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
      public string DevicePath;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HidCaps
    {
      public ushort Usage;
      public ushort UsagePage;
      public ushort InputReportByteLength;
      public ushort OutputReportByteLength;
      public ushort FeatureReportByteLength;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
      public ushort[] Reserved;
      public ushort NumberLinkCollectionNodes;
      public ushort NumberInputButtonCaps;
      public ushort NumberInputValueCaps;
      public ushort NumberInputDataIndices;
      public ushort NumberOutputButtonCaps;
      public ushort NumberOutputValueCaps;
      public ushort NumberOutputDataIndices;
      public ushort NumberFeatureButtonCaps;
      public ushort NumberFeatureValueCaps;
      public ushort NumberFeatureDataIndices;
    }

    public struct HIDD_ATTRIBUTES
    {
      public uint Size;
      public ushort VendorID;
      public ushort ProductID;
      public ushort VersionNumber;
    }

    public struct HidP_Range
    {
      public short UsageMin;
      public short UsageMax;
      public short StringMin;
      public short StringMax;
      public short DesignatorMin;
      public short DesignatorMax;
      public short DataIndexMin;
      public short DataIndexMax;
    }

    public struct HidP_NotRange
    {
      public short Usage;
      public short Reserved1;
      public short StringIndex;
      public short Reserved2;
      public short DesignatorIndex;
      public short Reserved3;
      public short DataIndex;
      public short Reserved4;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct HidP_Button_Caps
    {
      [FieldOffset(0)]
      public short UsagePage;
      [FieldOffset(2)]
      public byte ReportID;
      [MarshalAs(UnmanagedType.U1)]
      [FieldOffset(3)]
      public bool IsAlias;
      [FieldOffset(4)]
      public short BitField;
      [FieldOffset(6)]
      public short LinkCollection;
      [FieldOffset(8)]
      public short LinkUsage;
      [FieldOffset(10)]
      public short LinkUsagePage;
      [MarshalAs(UnmanagedType.U1)]
      [FieldOffset(12)]
      public bool IsRange;
      [MarshalAs(UnmanagedType.U1)]
      [FieldOffset(13)]
      public bool IsStringRange;
      [MarshalAs(UnmanagedType.U1)]
      [FieldOffset(14)]
      public bool IsDesignatorRange;
      [MarshalAs(UnmanagedType.U1)]
      [FieldOffset(15)]
      public bool IsAbsolute;
      [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
      [FieldOffset(16)]
      public int[] Reserved;
      [FieldOffset(56)]
      public HIDNativeMethods.HidP_Range Range;
      [FieldOffset(56)]
      public HIDNativeMethods.HidP_NotRange NotRange;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct HidP_Value_Caps
    {
      [FieldOffset(0)]
      public short UsagePage;
      [FieldOffset(2)]
      public byte ReportID;
      [MarshalAs(UnmanagedType.I1)]
      [FieldOffset(3)]
      public bool IsAlias;
      [FieldOffset(4)]
      public short BitField;
      [FieldOffset(6)]
      public short LinkCollection;
      [FieldOffset(8)]
      public short LinkUsage;
      [FieldOffset(10)]
      public short LinkUsagePage;
      [MarshalAs(UnmanagedType.I1)]
      [FieldOffset(12)]
      public bool IsRange;
      [MarshalAs(UnmanagedType.I1)]
      [FieldOffset(13)]
      public bool IsStringRange;
      [MarshalAs(UnmanagedType.I1)]
      [FieldOffset(14)]
      public bool IsDesignatorRange;
      [MarshalAs(UnmanagedType.I1)]
      [FieldOffset(15)]
      public bool IsAbsolute;
      [MarshalAs(UnmanagedType.I1)]
      [FieldOffset(16)]
      public bool HasNull;
      [FieldOffset(17)]
      public char Reserved;
      [FieldOffset(18)]
      public short BitSize;
      [FieldOffset(20)]
      public short ReportCount;
      [FieldOffset(22)]
      public short Reserved2a;
      [FieldOffset(24)]
      public short Reserved2b;
      [FieldOffset(26)]
      public short Reserved2c;
      [FieldOffset(28)]
      public short Reserved2d;
      [FieldOffset(30)]
      public short Reserved2e;
      [FieldOffset(32)]
      public short UnitsExp;
      [FieldOffset(34)]
      public short Units;
      [FieldOffset(36)]
      public short LogicalMin;
      [FieldOffset(38)]
      public short LogicalMax;
      [FieldOffset(40)]
      public short PhysicalMin;
      [FieldOffset(42)]
      public short PhysicalMax;
      [FieldOffset(44)]
      public HIDNativeMethods.HidP_Range Range;
      [FieldOffset(44)]
      public HIDNativeMethods.HidP_NotRange NotRange;
    }

    public enum HIDP_REPORT_TYPE
    {
      HidP_Input,
      HidP_Output,
      HidP_Feature,
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct HIDP_DATA
    {
      [FieldOffset(0)]
      public short DataIndex;
      [FieldOffset(2)]
      public short Reserved;
      [FieldOffset(4)]
      public int RawValue;
      [MarshalAs(UnmanagedType.U1)]
      [FieldOffset(4)]
      public bool On;
    }
  }
}
