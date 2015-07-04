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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.Regions;
using DataGridWithColumnStyle.View;
using Microsoft.Practices.ServiceLocation;

namespace DataGridWithColumnStyle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

       
        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             IRegionManager regMgr = ServiceLocator.Current.GetInstance<IRegionManager>();
            var s = sender as ComboBox;
            ComboBoxItem i = s.SelectedItem as ComboBoxItem;

            if (regMgr.Regions.ContainsRegionWithName("PricingGridRegion"))
            {
                var views = regMgr.Regions["PricingGridRegion"].Views;
                foreach (var v in views)
                {
                    regMgr.Regions["PricingGridRegion"].Remove(v);
                }
            }

            if (i.Name == "DurTrader")
            {
                regMgr.RegisterViewWithRegion("PricingGridRegion", typeof(DurationTraderView));
            }

            if (i.Name == "_5_10YR")
            {
                regMgr.RegisterViewWithRegion("PricingGridRegion", typeof(_5_10YRView));
            }

        }
    }
}
