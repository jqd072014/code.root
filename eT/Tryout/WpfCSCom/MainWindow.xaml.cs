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

namespace WpfCSCom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CSCom.KeypadCS k = new CSCom.KeypadCS();
        CSCom.CallBack myc;
        public MainWindow()
        {
            InitializeComponent();
        }

        //open
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TB.Text = "ErrorCode: " + k.Open_USB().ToString();
            myc = new CSCom.CallBack(CB);
        }

        //close
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TB.Text = "ErrorCode: " + k.Accept_LED(1).ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TB.Text = "ErrorCode: " + k.Accept_LED(2).ToString();
        }

        void CB(int d)
        {
            TB.Text = d.ToString();
        }
    }
}
