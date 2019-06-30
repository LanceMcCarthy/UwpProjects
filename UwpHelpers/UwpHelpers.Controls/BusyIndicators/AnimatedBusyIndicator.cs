using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls;

namespace UwpHelpers.Controls.BusyIndicators
{
    [TemplatePart(Name = "PART_Indicator", Type = typeof(AnimatedVisualPlayer))]
    [TemplatePart(Name = "PART_BusyContentPresenter", Type = typeof(ContentPresenter))]
    public sealed class AnimatedBusyIndicator : Control
    {
        private AnimatedVisualPlayer player;
        private ContentPresenter presenter;

        public AnimatedBusyIndicator()
        {
            this.DefaultStyleKey = typeof(AnimatedBusyIndicator);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.player = (AnimatedVisualPlayer)GetTemplateChild("PART_Indicator");
            this.presenter = (ContentPresenter)GetTemplateChild("PART_BusyContentPresenter");
        }

        public static readonly DependencyProperty AnimationContentProperty = DependencyProperty.Register(
            "AnimationContent", typeof(IAnimatedVisualSource), typeof(AnimatedBusyIndicator), new PropertyMetadata(default(IAnimatedVisualSource)));

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            "IsActive", typeof(bool), typeof(AnimatedBusyIndicator), new PropertyMetadata(default(bool), OnIsActiveChanged));

        public static readonly DependencyProperty BusyContentProperty = DependencyProperty.Register(
            "BusyContent", typeof(object), typeof(AnimatedBusyIndicator), new PropertyMetadata(default, OnBusyContentChanged));

        public static readonly DependencyProperty AnimationHeightProperty = DependencyProperty.Register(
            "AnimationHeight", typeof(double), typeof(AnimatedBusyIndicator), new PropertyMetadata(200.0));

        public static readonly DependencyProperty AnimationWidthProperty = DependencyProperty.Register(
            "AnimationWidth", typeof(double), typeof(AnimatedBusyIndicator), new PropertyMetadata(200.0));

        public static readonly DependencyProperty AnimationHorizontalAlignmentProperty = DependencyProperty.Register(
            "AnimationHorizontalAlignment", typeof(HorizontalAlignment), typeof(AnimatedBusyIndicator), new PropertyMetadata(HorizontalAlignment.Center));

        public static readonly DependencyProperty AnimationVerticalAlignmentProperty = DependencyProperty.Register(
            "AnimationVerticalAlignment", typeof(VerticalAlignment), typeof(AnimatedBusyIndicator), new PropertyMetadata(VerticalAlignment.Center));

        public IAnimatedVisualSource AnimationContent
        {
            get => (IAnimatedVisualSource)GetValue(AnimationContentProperty);
            set => SetValue(AnimationContentProperty, value);
        }

        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        public object BusyContent
        {
            get => (object)GetValue(BusyContentProperty);
            set => SetValue(BusyContentProperty, value);
        }

        public double AnimationHeight
        {
            get => (double)GetValue(AnimationHeightProperty);
            set => SetValue(AnimationHeightProperty, value);
        }

        public double AnimationWidth
        {
            get => (double)GetValue(AnimationWidthProperty);
            set => SetValue(AnimationWidthProperty, value);
        }

        public HorizontalAlignment AnimationHorizontalAlignment
        {
            get => (HorizontalAlignment)GetValue(AnimationHorizontalAlignmentProperty);
            set => SetValue(AnimationHorizontalAlignmentProperty, value);
        }

        public VerticalAlignment AnimationVerticalAlignment
        {
            get => (VerticalAlignment)GetValue(AnimationVerticalAlignmentProperty);
            set => SetValue(AnimationVerticalAlignmentProperty, value);
        }

        private static async void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AnimatedBusyIndicator self && e.NewValue != null)
            {
                if ((bool)e.NewValue)
                {
                    self.Visibility = Visibility.Visible;

                    self.player.AutoPlay = true;

                    if (!self.player.IsPlaying && self.player.IsAnimatedVisualLoaded)
                    {
                        await self.player.PlayAsync(0, 100, true);
                    }
                }
                else
                {
                    self.player.Stop();

                    self.Visibility = Visibility.Collapsed;
                }
            }
        }

        private static void OnBusyContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AnimatedBusyIndicator self && self.presenter != null)
            {
                self.presenter.Content = e.NewValue;
            }
        }
    }
}
