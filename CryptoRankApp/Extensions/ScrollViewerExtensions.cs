using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace CryptoRankApp.Extensions
{
    public static class ScrollViewerExtensions
    {
        public static readonly DependencyProperty ResetScrollProperty = DependencyProperty.RegisterAttached(
            "ResetScroll", typeof(bool), typeof(ScrollViewerExtensions), new PropertyMetadata(false, OnResetScrollChanged));

        public static void SetResetScroll(DependencyObject element, bool value)
        {
            element.SetValue(ResetScrollProperty, value);
        }

        public static bool GetResetScroll(DependencyObject element)
        {
            return (bool)element.GetValue(ResetScrollProperty);
        }

        private static void OnResetScrollChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ScrollViewer scrollViewer && (bool)e.NewValue)
            {
                scrollViewer.ScrollToTop();
            }
        }
    }

}
