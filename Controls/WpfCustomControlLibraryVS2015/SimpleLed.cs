using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfCustomControlLibraryVS2015.Converters;

namespace WpfCustomControlLibraryVS2015
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibraryVS2015.StateIndicators.Led"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibraryVS2015.StateIndicators.Led;assembly=WpfCustomControlLibraryVS2015.StateIndicators.Led"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:SimpleLed/>
    ///
    /// </summary>
    public class SimpleLed : Control
    {
        static SimpleLed()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SimpleLed), new FrameworkPropertyMetadata(typeof(SimpleLed)));
        }
        #region Colors
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("CurrentColor", typeof(Color), typeof(SimpleLed), 
                new PropertyMetadata(Colors.Gray));

        private Color CurrentColor
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly DependencyProperty ColorOnProperty =
            DependencyProperty.Register("ColorOn", typeof(Color), typeof(SimpleLed),
                new PropertyMetadata(Colors.LimeGreen, OnColorStatePropertyChanged));

        private static void OnColorStatePropertyChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            UpdateStateLed((SimpleLed)element, (element as SimpleLed).CurrentState);
        }

        public Color ColorOn
        {
            get { return (Color)GetValue(ColorOnProperty); }
            set { SetValue(ColorOnProperty, value); }
        }

        public static readonly DependencyProperty ColorOffProperty =
            DependencyProperty.Register("ColorOff", typeof(Color), typeof(SimpleLed), 
                new PropertyMetadata(Colors.Red, OnColorStatePropertyChanged));

        public Color ColorOff
        {
            get { return (Color)GetValue(ColorOffProperty); }
            set { SetValue(ColorOffProperty, value); }
        }

        public static readonly DependencyProperty ColorDisabledProperty =
            DependencyProperty.Register("ColorDisabled", typeof(Color), typeof(SimpleLed),
                new PropertyMetadata(Colors.Gray, OnColorStatePropertyChanged));

        public Color ColorDisabled
        {
            get { return (Color)GetValue(ColorDisabledProperty); }
            set { SetValue(ColorDisabledProperty, value); }
        }


        public TypeStateColorEnum CurrentState
        {
            get { return (TypeStateColorEnum)GetValue(CurrentStateProperty); }
            set { SetValue(CurrentStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentStateProperty =
            DependencyProperty.Register("CurrentState", typeof(TypeStateColorEnum), typeof(SimpleLed), 
                new PropertyMetadata(TypeStateColorEnum.Disabled, CurrentStateChanged, CoerceCurrentState));

        private static void CurrentStateChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
        {
            //var control = (SimpleLed)element;
            //control.InvalidateProperty(ColorProperty);
            Debug.Print(">>>>> Changed");
        }

        private static object CoerceCurrentState(DependencyObject element, object baseValue)
        {
            Debug.Print(">>>>> Coerce");
            return UpdateStateLed((SimpleLed)element, (TypeStateColorEnum)baseValue);
        }

        private static object UpdateStateLed(SimpleLed control, TypeStateColorEnum currentState)
        {
            switch (currentState)
            {
                case TypeStateColorEnum.On:
                    control.CurrentColor = control.ColorOn;
                    break;
                case TypeStateColorEnum.Off:
                    control.CurrentColor = control.ColorOff;
                    break;
                case TypeStateColorEnum.Disabled:
                    control.CurrentColor = control.ColorDisabled;
                    break;
                default:
                    break;
            }
            return currentState;
        }
        #endregion

        public Color ColorBorderLed
        {
            get { return (Color)GetValue(ColorBorderProperty); }
            set { SetValue(ColorBorderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ColorBorderLed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorBorderProperty =
            DependencyProperty.Register("ColorBorderLed", typeof(Color), typeof(SimpleLed), new PropertyMetadata(Colors.Black));

        public double BorderThicknessLed
        {
            get { return (double)GetValue(BorderThicknessLedProperty); }
            set { SetValue(BorderThicknessLedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderThicknessLed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderThicknessLedProperty =
            DependencyProperty.Register("BorderThicknessLed", typeof(double), typeof(SimpleLed), 
                new PropertyMetadata(20.0));



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(SimpleLed), new PropertyMetadata(""));



        public double LedRadius
        {
            get { return (double)GetValue(LedRadiusProperty); }
            set { SetValue(LedRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LedRadiusProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LedRadiusProperty =
            DependencyProperty.Register("LedRadius", typeof(double), typeof(SimpleLed), 
                new PropertyMetadata(14.0));
    }
}
