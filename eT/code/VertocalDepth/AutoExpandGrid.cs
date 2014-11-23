using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace VertocalDepth
{
    public class AutoExpandGrid : Grid
    {
        protected override System.Windows.Media.Visual GetVisualChild(int index)
        {
            var visualChild = base.GetVisualChild(index);

            var uiElement = visualChild as UIElement;
            if (uiElement != null)
                EnsureEnoughRowsAndColumns(uiElement);

            return visualChild;
        }

        private void EnsureEnoughRowsAndColumns(UIElement child)
        {
            int minRows = GetRow(child) + GetRowSpan(child);
            int minColumns = GetColumn(child) + GetColumnSpan(child);

            while (minRows > RowDefinitions.Count)
            {
                RowDefinitions.Add(new RowDefinition());
            }

            while (minColumns > ColumnDefinitions.Count)
            {
                ColumnDefinitions.Add(new ColumnDefinition());
            }
        }
    }
}
