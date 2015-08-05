using PIEHid64Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CBTest
{
    /// <summary>
    /// Interaction logic for Light.xaml
    /// </summary>
    public partial class Light : Window
    {
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_STYLE = -16;
        private const int WS_MINIMIZE = -131073;
        XkeysKeyboardDevice _device;

        public Light()
        {
            InitializeComponent();
            _device = new XkeysKeyboardDevice();
            _device.TryOpen(SupportedXkeysKeyboardDevice.XK80);
            this.Closing += Light_Closed;
            _device.w = this;
            this.SourceInitialized += (s, e) =>
            {
                var hwnd = new WindowInteropHelper(this).Handle;
                var value = GetWindowLong(hwnd, GWL_STYLE);
                SetWindowLong(hwnd, GWL_STYLE, (int)(value & WS_MINIMIZE));

            };

        }

        public void OnTop()
        {
            

            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (IsActive) return;
                var orig = WindowState;
                WindowState = WindowState.Minimized;

                WindowState = orig;

            }));

        }

        void Light_Closed(object sender, EventArgs e)
        {
            try
            {
                _device.Dispose();
            }
            catch(Exception )
            {

            }
            
        }

    }
}
