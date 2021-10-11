using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Randomizer_WPF.UserControls
{
    public class SortAdorner : Adorner
    {
        private static Geometry ascGeometry = Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");
        private static Geometry dscGeometry = Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

        public ListSortDirection Direction { get; private set; }

        public SortAdorner(UIElement element, ListSortDirection dir)
            : base(element)
        {
            this.Direction = dir;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (AdornedElement.RenderSize.Width < 20)
                return;

            TranslateTransform transform = new TranslateTransform
                (
                    AdornedElement.RenderSize.Width - 15,
                    (AdornedElement.RenderSize.Height - 5) / 2
                );
            drawingContext.PushTransform(transform);

            Geometry geometry = ascGeometry;
            if (this.Direction == ListSortDirection.Descending)
                geometry = dscGeometry;
            drawingContext.DrawGeometry(Brushes.Black, null, geometry);

            drawingContext.Pop();
        }

        internal static void SortColumn(ListView lv, ref GridViewColumnHeader column, ref SortAdorner adorner, GridViewColumnHeader newColumn, string sortBy)
        {
            if (lv == null) return;

            if (column != null)
            {
                AdornerLayer.GetAdornerLayer(column)?.Remove(adorner);
                lv.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (column == newColumn && adorner?.Direction == newDir)
                newDir = ListSortDirection.Descending;

            column = newColumn;
            adorner = new SortAdorner(column, newDir);
            AdornerLayer.GetAdornerLayer(column)?.Add(adorner);
            lv.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));
        }
    }
}
