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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            Loaded += Window1_Loaded;
            Unloaded += Window1_Unloaded;
        }

        void Window1_Unloaded(object sender, RoutedEventArgs e)
        {
            _device.Dispose();
        }

        XkeysKeyboardDevice _device;
        void Window1_Loaded(object sender, RoutedEventArgs e)
        {
          _device= new  XkeysKeyboardDevice();
          _device.DeviceClosed += () => {
              this.Dispatcher.Invoke(new Action(() =>
              {
                  this.Title = "Closed";
              }));

          };    
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _device.TryOpen(SupportedXkeysKeyboardDevice.XK80);
            this.Title = _device.Status.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _device.TryClose();
            this.Title = _device.Status.ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
