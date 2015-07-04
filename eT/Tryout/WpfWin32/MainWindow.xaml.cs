using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfWin32
{
    public delegate void CallBack(int data);
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CallBack myCallBack;
        public MainWindow()
        {
            InitializeComponent();
            myCallBack = new CallBack(State);
            MwxUSBDll.Set_Callback(myCallBack);
        }

        private void State(int data)
        {
            string temp = "";

            switch (data)
            {
                case 1: temp = "POS Keyboard down";
                    break;
                case 2: temp = "POS Keyboard up";
                    break;
                case 3: temp = "MSR Event";
                    break;
                case 4: temp = "Keylock Event";
                    break;
                case 5: temp = "Version, TCO Event";
                    break;
                case 8: temp = "Scanner Event";
                    break;
                case 9: temp = "Restart Keyhook Event";
                    break;
            }
            Dispatcher.BeginInvoke(new Action(() =>
            {
                TB.Text = temp;
            }));

         //   MessageBox.Show(temp);
            
        }

        //open
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TB.Text = "ErrorCode: " + MwxUSBDll.Open_USB().ToString();
            MwxUSBDll.Accept_LED(0);
        }

        //close
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TB.Text = "ErrorCode: " + MwxUSBDll.Close_USB().ToString();
        }

        //green
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TB.Text = "gr: " + MwxUSBDll.Accept_LED(1).ToString();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            TB.Text = "red: " + MwxUSBDll.Accept_LED(2).ToString();
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
