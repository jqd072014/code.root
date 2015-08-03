using PIEHid64Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
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
        XkeysKeyboardDevice _device;

        public Light()
        {
            InitializeComponent();
            _device = new XkeysKeyboardDevice();
            _device.TryOpen(SupportedXkeysKeyboardDevice.XK80);
            this.Closing += Light_Closed;
            _device.w = this;

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
