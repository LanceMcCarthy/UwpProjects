/*
Code provided as-is, no warranty implied
Thanks to https://github.com/r2d2rigo for the restricted aspect ratio formula and MinItemWidth MinItemHeight approach
*/

using System;
using System.Diagnostics;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UwpHelpers.Controls.ListControls
{
    /// <summary>
    /// An adaptive GridView, where items are restricted to an aspect ratio set by MinWidth and MinHeight
    /// </summary>
    public class AdaptiveGridView : GridView
    {
        #region DependencyProperties
        
        /// <summary>
        /// Minimum height for item
        /// </summary>
        public double MinItemHeight
        {
            get { return (double) GetValue(AdaptiveGridView.MinItemHeightProperty); }
            set { SetValue(AdaptiveGridView.MinItemHeightProperty, value); }
        }

        public static readonly DependencyProperty MinItemHeightProperty =
            DependencyProperty.Register(
                "MinItemHeight",
                typeof(double),
                typeof(AdaptiveGridView),
                new PropertyMetadata(1.0, (s, a) =>
                {
                    if (!double.IsNaN((double) a.NewValue))
                    {
                        ((AdaptiveGridView) s).InvalidateMeasure();
                    }
                }));

        /// <summary>
        /// Minimum width for item (must be greater than zero)
        /// </summary>
        public double MinItemWidth
        {
            get { return (double) GetValue(AdaptiveGridView.MinimumItemWidthProperty); }
            set { SetValue(AdaptiveGridView.MinimumItemWidthProperty, value); }
        }

        public static readonly DependencyProperty MinimumItemWidthProperty =
            DependencyProperty.Register(
                "MinItemWidth",
                typeof(double),
                typeof(AdaptiveGridView),
                new PropertyMetadata(1.0, (s, a) =>
                {
                    if (!Double.IsNaN((double) a.NewValue))
                    {
                        ((AdaptiveGridView) s).InvalidateMeasure();
                    }
                }));

        #endregion

        public AdaptiveGridView()
        {
            if (this.ItemContainerStyle == null)
            {
                this.ItemContainerStyle = new Style(typeof(GridViewItem));
            }

            this.ItemContainerStyle.Setters.Add(new Setter(HorizontalContentAlignmentProperty, HorizontalAlignment.Stretch));

            this.Loaded += (s, a) =>
            {
                if (this.ItemsPanelRoot != null)
                {
                    this.InvalidateMeasure();
                }
            };
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            //Bug: Moved from MeasureOverride to ArrangeOverride to fix issue where items were not being measured when Grouping
            var panel = this.ItemsPanelRoot as ItemsWrapGrid;
            if (panel != null)
            {
                if (MinItemWidth == 0 || MinItemHeight == 0)
                {
                    throw new ArgumentException("You need to set MinItemHeight and MinItemWidth to a value greater than 0");
                }

                var availableWidth = finalSize.Width - (this.Padding.Right + this.Padding.Left);

                var numColumns = Math.Floor(availableWidth / MinItemWidth);
                numColumns = numColumns == 0 ? 1 : numColumns;

                //Not used yet (for horizontal scrolling scenarios)
                //var numRows = Math.Ceiling(this.Items.Count / numColumns);

                var itemWidth = availableWidth / numColumns;
                var aspectRatio = MinItemHeight / MinItemWidth;
                var itemHeight = itemWidth * aspectRatio;

                panel.ItemWidth = itemWidth;
                panel.ItemHeight = itemHeight;
            }

            Debug.WriteLine($"----AdaptiveGridView ArrangeOverride----\r\nItemWidth: {(this.ItemsPanelRoot as ItemsWrapGrid)?.ItemWidth} - ItemHeight: {(this.ItemsPanelRoot as ItemsWrapGrid)?.ItemHeight}");
            
            return base.ArrangeOverride(finalSize);
        }
    }
}
