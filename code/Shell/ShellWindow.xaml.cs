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
using Xceed.Wpf.AvalonDock.Layout;
using Trading.View;
using System.IO;
using Xceed.Wpf.AvalonDock.Layout.Serialization;
using Trading;
using Microsoft.Practices.Unity;
using Microsoft.Practices.ServiceLocation;

namespace Shell
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : Window
    {
        IUnityContainer _container;
        public ShellWindow(IUnityContainer container)
        {
            InitializeComponent();
            _container = container;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IDockingViewManager dockingViewManager = _container.Resolve<DockingViewManager>(); // ServiceLocator.Current.GetInstance<DockingViewManager>();
            string viewType = (sender as Button).Content as string;
            UserControl view = new UserControl();
            if (viewType == "Dur") view = dockingViewManager.GetDockingView(WellknowViewName.DurationTraderView);
            if (viewType == "5-10YR") view = dockingViewManager.GetDockingView(WellknowViewName._5_10Yr);
            
            LayoutAnchorable a1 = new LayoutAnchorable() {Content=view, Title="Floating", FloatingWidth = 600, FloatingHeight = 400, FloatingLeft=50, FloatingTop=50 };
            a1.AddToLayout(dockingManager, AnchorableShowStrategy.Most);
            a1.Float();
          
        }

        XmlLayoutSerializer ser;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
             ser = new XmlLayoutSerializer(dockingManager);
             StreamWriter sw = new StreamWriter(@"c:\working\1.ser");
            ser.Serialize(sw);
            sw.Close();
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
             ser = new XmlLayoutSerializer(dockingManager);
            ser.LayoutSerializationCallback+=new EventHandler<LayoutSerializationCallbackEventArgs>(ser_LayoutSerializationCallback);
             using (StreamReader sr = new StreamReader(@"c:\working\1.ser"))
             {
                 ser.Deserialize(sr);
             }
        }

        void ser_LayoutSerializationCallback(object sender, LayoutSerializationCallbackEventArgs e)
        {
            e.Content = new UserControl();
        }
    }
}
