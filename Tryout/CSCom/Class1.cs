using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSCom
{

    public delegate void CallBack(int data);

    [Guid("19A6C9DC-F9BF-47C7-8940-04C79EFCBE29")]
    public interface IKeypadCS
    {
        [DispId(1)]
        byte Open_USB();
        [DispId(2)]
        byte Accept_LED(int value);
        [DispId(3)]
        void Set_Callback(CallBack pointer);
        [DispId(4)]
        byte read_POS_key_USB(int[] cpData, UInt32 dwTime);
    }


    [Guid("8CE12AD2-C2EC-401E-8C47-63054337CCF1"),
    ClassInterface(ClassInterfaceType.None)]
    public class KeypadCS :IKeypadCS
    {
        public byte Open_USB()
        {
            return MwxUSBDll.Open_USB();  
        }

        public byte Accept_LED(int value)
        {
            return MwxUSBDll.Accept_LED(value);
        }

        public void Set_Callback(CallBack pointer)
        {
            MwxUSBDll.Set_Callback(pointer);
        }

        public byte read_POS_key_USB(int[] cpData, uint dwTime)
        {
            return MwxUSBDll.read_POS_key_USB(cpData,dwTime);
        }
    }


    #region API

    class MwxUSBDll
    {
        [DllImport("MwxUsb.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte Open_USB();

        [DllImport("MwxUsb.dll")]
        public static extern byte Get_Counter_USB();

        [DllImport("MwxUsb.dll")]
        public static extern byte Check_USB();

        [DllImport("MwxUsb.dll")]
        public static extern byte Close_USB();

        [DllImport("MwxUsb.dll")]
        public static extern byte Clear_USB();

        [DllImport("MwxUsb.dll")]
        public static extern byte snddata_USB(byte ucdata);

        [DllImport("MwxUsb.dll")]
        public static extern byte read_MSR_data_USB(StringBuilder ucpData1, int dwLen1,
                                                    StringBuilder ucpData2, int dwLen2,
                                                    StringBuilder ucpData3, int dwLen3, UInt32 dwTime);

        [DllImport("MwxUsb.dll")]
        public static extern byte read_Version_USB(StringBuilder ucpData, int dwLen, int uLevel);

        [DllImport("MwxUsb.dll")]
        public static extern byte read_TCO_data_USB(StringBuilder ucpData, int dwLen, UInt32 dwTime);

        [DllImport("MwxUsb.dll")]
        public static extern int read_Keylock();

        [DllImport("MwxUsb.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte Accept_LED(int value);

        [DllImport("MwxUsb.dll")]
        public static extern byte Init_Sound_USB(int uFreq1, int uFreq2, int uFreq3,
                                                 int uVol1, int uVol2, int uVol3);

        [DllImport("MwxUsb.dll")]
        public static extern byte Sound_USB(int uNumber);

        [DllImport("MwxUsb.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern byte read_POS_key_USB(int[] cpData, UInt32 dwTime);

        [DllImport("MwxUsb.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Set_Callback(CallBack Pointer);
    }

    #endregion
}
