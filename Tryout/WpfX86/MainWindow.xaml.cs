using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WpfX86
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Wrapper2Lib.Keypad k = new Wrapper2Lib.Keypad();
        public MainWindow()
        {
            InitializeComponent();
            k.Callback += k_Callback;
        }

        void k_Callback(int data)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                Title = data.ToString();
            }));
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
            k.Open_USB();
        }
    }
}
