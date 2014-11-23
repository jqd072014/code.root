using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace VertocalDepth
{
    public class VDBehavior :Behavior<DataGrid>
    {
        public static readonly DependencyProperty SelectingItemProperty = DependencyProperty.RegisterAttached(
           "SelectingItem",
           typeof(MySelectionType),
           typeof(SelectingItemAttachedProperty),
           new PropertyMetadata(default(MySelectionType), OnSelectingItemChanged));

        protected override void OnAttached()
        {
            base.OnAttached();
          //  AssociatedObject.ScrollIntoView()
        }

        static void OnSelectingItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var grid = sender as DataGrid;
            if (grid == null || grid.SelectedItem == null)
                return;

            // Works with .Net 4.5
            grid.Dispatcher.InvokeAsync(() =>
            {
                grid.UpdateLayout();
                grid.ScrollIntoView(grid.SelectedItem, null);
            });

            // Works with .Net 4.0
            grid.Dispatcher.BeginInvoke((Action)(() =>
            {
                grid.UpdateLayout();
                grid.ScrollIntoView(grid.SelectedItem, null);
            }));
        }
    }
}
