using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace LogoUI.Samples.Client.Gui.Shared.Views.Controls
{
    public class GeometryButton : ButtonBase
    {
        static GeometryButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GeometryButton), new FrameworkPropertyMetadata(typeof(GeometryButton)));
        }

        public Geometry AnimatedPath
        {
            get { return (Geometry)GetValue(AnimatedPathProperty); }
            set { SetValue(AnimatedPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimatedPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimatedPathProperty =
            DependencyProperty.Register("AnimatedPath", typeof(Geometry), typeof(GeometryButton), new PropertyMetadata(null));
    }
}