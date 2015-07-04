using PIEHid64Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CBTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, PIEDataHandler, PIEErrorHandler
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }
        PIEDevice _xkeyPad;
        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {


          
        }

        PIEKeyboardContext ctx = new PIEKeyboardContext();
        public void HandlePIEHidData(byte[] data, PIEDevice sourceDevice, int error)
        {
           
            string keyCode = ctx.GetKeyCode(data);
            //for (int i = 3; i < 13; i++)
            //{
            //    Debug.Write(data[i]);
            //}
            //foreach (var d in data)
            //{

            //    Debug.Write(d);
            //}
            //Debug.WriteLine("");
            Debug.WriteLine(keyCode);

                
        }

        public void HandlePIEHidError(PIEDevice sourceDevices, int error)
        {
            MessageBox.Show("");
        }

        public void HandlePIEHidData(byte[] data, PIEDevice sourceDevice)
        {
            MessageBox.Show("");
        }

        public void HandlePIEHidError(int error, PIEDevice sourceDevices)
        {
             MessageBox.Show("");
        }

        #region top 3 buttons--off, red,blue

//        Byte1       Byte2        Byte3      Bytes4-36 
//        Constant   Command    LED Control   Constant 
//         0          186           LEDs        0 

        //LEDs: Bits 1-6=0, bit 7=1 to turn on Green LED or 0 to turn off Green LED, bit 8=1 to turn on Red LED or 0 to turn off Red LED.

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //off
            byte[] wData = null;
            wData = new byte[_xkeyPad.WriteLength];
            for (int j = 0; j < _xkeyPad.WriteLength - 1; j++) //don't clear out last byte, the LED byte
            {
                wData[j] = 0;
            }

        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Red
            byte[] wData = null;
            wData = new byte[_xkeyPad.WriteLength];
            for (int j = 0; j < _xkeyPad.WriteLength - 1; j++) //don't clear out last byte, the LED byte
            {
                wData[j] = 0;
            }
            wData[0] = 0;
            wData[1] = 186;
            wData[2] = 2;  //??? not in spec,  LEDs bits7=1 should be green???
            int result = _xkeyPad.WriteData(wData);
            Debug.WriteLine("Return WriteData " + result);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            byte[] wData = null;
            wData = new byte[_xkeyPad.WriteLength];
            for (int j = 0; j < _xkeyPad.WriteLength - 1; j++) //don't clear out last byte, the LED byte
            {
                wData[j] = 0;
            }
            wData[0] = 2;
            wData[1] = 186;
            wData[7] = 1;// (byte)(wData[7] | 64);  ??? not per spec  wData[7] is 8 Byte not bit
            //wData[7] = (byte)(wData[7] | 128);
            int result = _xkeyPad.WriteData(wData);
            Debug.WriteLine("Return WriteData " + result);
        }

        #endregion

        #region Bank on/off


        //   Byte1      Byte2     Byte3     Byte4    Bytes 5-36 
        //  Constant   Command   Bank #     State    Constant 
        //     0         182     Bank        OnOff     0 


        // OnOff: For all bits 0 for no backlighting, 1 for backlighting. 
        // Bit 0 = 1st row, bit 1=2nd row, bit 2=3rd row, bit 3=4th row, bit 4=5th row, bit 5=6th row. 
        // Note the intensities are not affected by this command. 


        private void BankOff_Click(object sender, RoutedEventArgs e)
        {
            byte[] wData = null;
            wData = new byte[_xkeyPad.WriteLength];
            for (int j = 0; j < _xkeyPad.WriteLength - 1; j++) //don't clear out last byte, the LED byte
            {
                wData[j] = 0;
            }
            wData[0] = 0;
            wData[1] = 182;
            wData[2] = 1;
            wData[3] = 0;
            int result = _xkeyPad.WriteData(wData);
            Debug.WriteLine("Return WriteData " + result);
        }

        private void BankOn_Click(object sender, RoutedEventArgs e)
        {
            byte[] wData = null;
            wData = new byte[_xkeyPad.WriteLength];
            for (int j = 0; j < _xkeyPad.WriteLength - 1; j++) //don't clear out last byte, the LED byte
            {
                wData[j] = 0;
            }
            wData[0] = 0;
            wData[1] = 182;
            wData[2] = 1;
            wData[3] = 5;
            int result = _xkeyPad.WriteData(wData);
            Debug.WriteLine("Return WriteData " + result);
        }

        #endregion


        #region on/off all and individual backlight

//        Byte 1    Byte 2      Byte 3      Byte 4       Bytes 5-36 
//      Constant    Command     Key Index   State         Constant 
//          0          181          Index   State           0 


        //State: 0 = off, 1 = on and 2 = flash
        private void KeyBackLightingOn_Click(object sender, RoutedEventArgs e)
        {
            byte[] wData = null;
            wData = new byte[_xkeyPad.WriteLength];
            for (int j = 0; j < _xkeyPad.WriteLength - 1; j++) //don't clear out last byte, the LED byte
            {
                wData[j] = 0;
            }
            wData[0] = 0;
            wData[1] = 181;
            wData[2] = byte.Parse(keyn.Text);
            wData[3] = 2;
            int result = _xkeyPad.WriteData(wData);
            Debug.WriteLine("Return WriteData " + result);
        }

        private void bkloff_Click(object sender, RoutedEventArgs e)
        {
            byte[] wData = null;
            wData = new byte[_xkeyPad.WriteLength];
            for (int j = 0; j < _xkeyPad.WriteLength - 1; j++) //don't clear out last byte, the LED byte
            {
                wData[j] = 0;
            }
            wData[0] = 0;
            wData[1] = 181;

                //wData[2] = byte.Parse(keyn.Text);
                //wData[3] = 0;

            for (byte i=0;i<160;i++)
            {
                wData[2] = i;
                wData[3] = 0;
                int result = _xkeyPad.WriteData(wData);
                Debug.WriteLine("Return WriteData " + result);
            }
            
        }

        #endregion


        #region unrelated to LED/Backlighting

        PIEDevice[] _devices;
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            _devices = PIEDevice.EnumeratePIE(1523);
            _xkeyPad = _devices[0];
            _xkeyPad.SetupInterface();
            _xkeyPad.SetDataCallback(this);
            _xkeyPad.SetErrorCallback(this);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (_devices != null)
                foreach (var d in _devices)
                {
                    d.CloseInterface();  
                }


        }

        #endregion

        private void keyn_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        void TurnOffAllBackLightsAndLED()
        {
            byte[] wData = null;
            wData = new byte[_xkeyPad.WriteLength];
            for (int j = 0; j < _xkeyPad.WriteLength - 1; j++) //don't clear out last byte, the LED byte
            {
                wData[j] = 0;
            }
            wData[0] = 0;
            wData[1] = 181;
            for (byte i = 0; i < 160; i++)
            {
                wData[2] = i;
                wData[3] = 0;
                int result = _xkeyPad.WriteData(wData);
                Debug.WriteLine("Return WriteData " + result);
            }

            byte[] wData2 = null;
            wData2 = new byte[_xkeyPad.WriteLength];
            for (int j = 0; j < _xkeyPad.WriteLength - 1; j++) //don't clear out last byte, the LED byte
            {
                wData2[j] = 0;
            }
            wData2[0] = 0;
            wData2[1] = 186;
            wData2[2] = 0;  
            int result2 = _xkeyPad.WriteData(wData2);
            Debug.WriteLine("Return WriteData " + result2);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            
          byte[] rdata = null;
          while (0 == _xkeyPad.ReadData(ref rdata))
          {
              //if (rdata[0] == 2)
              //{

                  MessageBox.Show(rdata[10].ToString());
              //}
          }
          
        }
    }
}
