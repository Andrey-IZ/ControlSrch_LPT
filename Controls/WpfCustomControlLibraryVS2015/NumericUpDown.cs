using ExpressionEvaluator;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace WpfCustomControlLibraryVS2015
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibraryVS2015"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WpfCustomControlLibraryVS2015;assembly=WpfCustomControlLibraryVS2015"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    [TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
    [TemplatePart(Name = "PART_IncreaseButton", Type = typeof(RepeatButton))]
    [TemplatePart(Name = "PART_DecreaseButton", Type = typeof(RepeatButton))]
    [TemplatePart(Name = "PART_Border", Type = typeof(Border))]
    public class NumericUpDown : Control
    {
        static NumericUpDown()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), 
                new FrameworkPropertyMetadata(typeof(NumericUpDown)));
        }

        public NumericUpDown()
        {
            Culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            Culture.NumberFormat.NumberDecimalDigits = DecimalPlaces;
            Loaded += NumericUpDown_Loaded;
        }

        private void NumericUpDown_Loaded(object sender, RoutedEventArgs e)
        {
            InvalidateProperty(ValueProperty);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            AttachToVisualTree();
            AttachCommands();
        }

        private void AttachToVisualTree()
        {
            AttachBorder();
            AttachTextBox();
            AttachIncreaseButton();
            AttachDecreaseButton();
        }

        #region Public Commands
        public ICommand IncreaseCommand
        {
            get { return (ICommand)GetValue(IncreaseCommandProperty); }
            set { SetValue(IncreaseCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IncreaseCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IncreaseCommandProperty =
            DependencyProperty.Register("IncreaseCommand", typeof(ICommand), typeof(NumericUpDown),
                new UIPropertyMetadata(null));

        public ICommand IncreaseHltCommand
        {
            get { return (ICommand)GetValue(IncreaseHltCommandProperty); }
            set { SetValue(IncreaseHltCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IncreaseHltCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IncreaseHltCommandProperty =
            DependencyProperty.Register("IncreaseHltCommand", typeof(ICommand), typeof(NumericUpDown),
                new PropertyMetadata(new RoutedUICommand("IncreaseHltCommand", "IncreaseHltCommand", typeof(NumericUpDown))));


        public ICommand DecreaseCommand
        {
            get { return (ICommand)GetValue(DecreaseCommandProperty); }
            set { SetValue(DecreaseCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DecreaseCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DecreaseCommandProperty =
            DependencyProperty.Register("DecreaseCommand", typeof(ICommand), typeof(NumericUpDown),
                new UIPropertyMetadata(null));


        public ICommand DecreaseHltCommand
        {
            get { return (ICommand)GetValue(DecreaseHltCommandProperty); }
            set { SetValue(DecreaseHltCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DecreaseHltCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DecreaseHltCommandProperty =
            DependencyProperty.Register("DecreaseHltCommand", typeof(ICommand), typeof(NumericUpDown),
                new PropertyMetadata(new RoutedUICommand("DecreaseHltCommand", "DecreaseHltCommand", typeof(NumericUpDown))));

        #endregion

        protected Border Border;

        private void AttachBorder()
        {
            var border = GetTemplateChild("PART_Border") as Border;

            if(border != null)
            {
                Border = border;
            }
        }

        protected TextBox TextBox;

        private void AttachTextBox()
        {
            var textBox = GetTemplateChild("PART_TextBox") as TextBox;

            // A null check is advised
            if (textBox != null)
            {
                TextBox = textBox;
                TextBox.TextChanged += TextBox_TextChanged;
                TextBox.LostFocus += TextBox_LostFocus;
                TextBox.PreviewMouseLeftButtonUp += TextBoxOnPreviewMouseLeftButtonUp;

                TextBox.UndoLimit = 1;
                TextBox.IsUndoEnabled = true;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = sender as TextBox;    
            if (tb != null)
            {
                if (IsAnimChangedValue && !IsSystemChangedValue)
                    IsStartAnimChangedValue = true;
                IsSystemChangedValue = false;
            }
        }

        private void TextBoxOnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (IsAutoSelectionActive)
            {
                TextBox.SelectAll();
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Value = ParseStringToDecimal(TextBox.Text);
        }

        protected RepeatButton IncreaseButton;

        private void AttachIncreaseButton()
        {
            var increaseButton = GetTemplateChild("PART_IncreaseButton") as RepeatButton;
            if (increaseButton != null)
            {
                IncreaseButton = increaseButton;
                IncreaseButton.Focusable = false;
                IncreaseButton.Command = _minorIncreaseValueCommand;
                IncreaseButton.PreviewMouseLeftButtonDown += (sender, args) => RemoveFocus();
                IncreaseButton.PreviewMouseRightButtonDown += ButtonOnPreviewMouseRightButtonDown;
            }
        }
        protected RepeatButton DecreaseButton;

        private void AttachDecreaseButton()
        {
            var decreaseButton = GetTemplateChild("PART_DecreaseButton") as RepeatButton;
            if (decreaseButton != null)
            {
                DecreaseButton = decreaseButton;
                DecreaseButton.Focusable = false;
                DecreaseButton.Command = _minorDecreaseValueCommand;
                DecreaseButton.PreviewMouseLeftButtonDown += (sender, args) => RemoveFocus();
                DecreaseButton.PreviewMouseRightButtonDown += ButtonOnPreviewMouseRightButtonDown; ;
            }
        }

        protected void ButtonOnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Value = 0;
        }

        protected void RemoveFocus()
        {
            // Passes focus here and then just deletes it
            Focusable = true;
            Focus();
            Focusable = false;
        }

        #region Value

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(Decimal), typeof(NumericUpDown),
                                        new PropertyMetadata(0m, OnValueChanged, CoerceValue));

        public Decimal Value
        {
            get { return (Decimal)GetValue(ValueProperty); }
            set
            {
                SetValue(ValueProperty, value);
                IsSystemChangedValue = false;
            }
        }

        private static void OnValueChanged(DependencyObject element,
                                            DependencyPropertyChangedEventArgs e)
        {
            var control = (NumericUpDown) element;
            if (control.TextBox != null)
            {
                control.TextBox.UndoLimit = 0;
                control.TextBox.UndoLimit = 1;
            }
        }

        private static object CoerceValue(DependencyObject element, object baseValue)
        {
            var control = (NumericUpDown)element;
            var value = (Decimal)baseValue;

            control.CoerceValueToBounds(ref value);

            // Get the text representation of Value
            var valueString = value.ToString(control.Culture);

            // Count all decimal places
            var decimalPlaces = control.GetDecimalPlacesCount(valueString);

            if (decimalPlaces > control.DecimalPlaces)
            {
                if (control.IsDecimalPointDynamic)
                {
                    // Assigning DecimalPlaces will coerce the number
                    control.DecimalPlaces = decimalPlaces;

                    // If the specified number of decimal places is still too much
                    if (decimalPlaces > control.DecimalPlaces)
                    {
                        value = control.TruncateValue(valueString, control.DecimalPlaces);
                    }
                }
                else
                {
                    // Remove all overflowing decimal places
                    value = control.TruncateValue(valueString, decimalPlaces);
                }
            }
            else if (control.IsDecimalPointDynamic)
            {
                control.DecimalPlaces = decimalPlaces;
            }

            // Change formatting based on this flag
            if (control.IsThousandSeparatorVisible)
            {
                if (control.TextBox != null)
                {
                    control.TextBox.Text = value.ToString("N", control.Culture);
                }
            }
            else
            {
                if (control.TextBox != null)
                {
                    // Don't let to start the animation after changing a tab switch (reinitializing)
                    if (control.IsAnimChangedValue)
                        IsSystemChangedValue = true; 
                    control.TextBox.Text = value.ToString("F", control.Culture);
                }
            }
            return value;
        }

        #endregion

        #region MaxValue Property
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue", typeof(Decimal), typeof(NumericUpDown),
                                new PropertyMetadata(1000000000m, OnMaxValueChanged,
                                                        CoerceMaxValue));
        public Decimal MaxValue
        {
            get { return (Decimal)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        private static void OnMaxValueChanged(DependencyObject element,
                                                DependencyPropertyChangedEventArgs e)
        {
            var control = (NumericUpDown)element;
            var maxValue = (Decimal)e.NewValue;

            // If maxValue steps over MinValue, shift it
            if (maxValue < control.MinValue)
            {
                control.MinValue = maxValue;
            }

            if (maxValue <= control.Value)
            {
                control.Value = maxValue;
            }
        }

        private static object CoerceMaxValue(DependencyObject element, Object baseValue)
        {
            var maxValue = (Decimal)baseValue;

            return maxValue;
        }
        #endregion

        #region MinValue Property
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(Decimal), typeof(NumericUpDown),
                                new PropertyMetadata(0m, OnMinValueChanged,
                                                        CoerceMinValue));

        public Decimal MinValue
        {
            get { return (Decimal)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        private static void OnMinValueChanged(DependencyObject element,
                                                DependencyPropertyChangedEventArgs e)
        {
            var control = (NumericUpDown)element;
            var minValue = (Decimal)e.NewValue;

            // If minValue steps over MaxValue, shift it
            if (minValue > control.MaxValue)
            {
                control.MaxValue = minValue;
            }

            if (minValue >= control.Value)
            {
                control.Value = minValue;
            }
        }

        private static object CoerceMinValue(DependencyObject element, Object baseValue)
        {
            var minValue = (Decimal)baseValue;

            return minValue;
        }
        #endregion

        #region Public Commands
        #endregion

        #region commands

        private readonly RoutedUICommand _minorIncreaseValueCommand =
            new RoutedUICommand("MinorIncreaseValue", "MinorIncreaseValue", typeof(NumericUpDown));

        private readonly RoutedUICommand _minorDecreaseValueCommand =
            new RoutedUICommand("MinorDecreaseValue", "MinorDecreaseValue", typeof(NumericUpDown));

        private readonly RoutedUICommand _updateValueStringCommand =
            new RoutedUICommand("UpdateValueString", "UpdateValueString", typeof(NumericUpDown));

        private readonly RoutedUICommand _majorDecreaseValueCommand =
            new RoutedUICommand("MajorDecreaseValue", "MajorDecreaseValue", typeof(NumericUpDown));

        private readonly RoutedUICommand _majorIncreaseValueCommand =
            new RoutedUICommand("MajorIncreaseValue", "MajorIncreaseValue", typeof(NumericUpDown));

        private readonly RoutedUICommand _cancelChangesCommand =
            new RoutedUICommand("CancelChanges", "CancelChanges", typeof(NumericUpDown));

        private void AttachCommands()
        {
            CommandBindings.Add(new CommandBinding(_minorIncreaseValueCommand, (a, b) =>
            {
                IncreaseHltCommand.Execute(a);

                // Whether to do minor's increment or to execute bounded command
                if (IncreaseCommand != null) IncreaseCommand.Execute(a);
                else IncreaseValue(true);
            }));
            
            CommandBindings.Add(new CommandBinding(_minorDecreaseValueCommand, (a, b) =>
            {
                DecreaseHltCommand.Execute(a);

                // Whether to do minor's increment or to execute bounded command
                if (DecreaseCommand != null) DecreaseCommand.Execute(a);
                else DecreaseValue(true);
            }));
            CommandBindings.Add(new CommandBinding(_majorIncreaseValueCommand, (a, b) => IncreaseValue(false)));
            CommandBindings.Add(new CommandBinding(_majorDecreaseValueCommand, (a, b) => DecreaseValue(false)));
            CommandBindings.Add(new CommandBinding(_updateValueStringCommand, (a, b) =>
            {
                Value = ParseStringToDecimal(TextBox.Text);
            }));
            CommandBindings.Add(new CommandBinding(_cancelChangesCommand, (a, b) => CancelChanges()));

            CommandBindings.Add(new CommandBinding(IncreaseHltCommand, (sender, args) =>
            {
                var sb = this.TryFindResource("StoryboardRepeatButtonUp") as Storyboard;
                sb?.Begin(IncreaseButton);
            }));
            CommandBindings.Add(new CommandBinding(DecreaseHltCommand, (sender, args) =>
            {
                var sb = this.TryFindResource("StoryboardRepeatButtonDown") as Storyboard;
                sb?.Begin(IncreaseButton);
            }));

            TextBox.InputBindings.Add(new KeyBinding(_minorIncreaseValueCommand, new KeyGesture(Key.Up)));
            TextBox.InputBindings.Add(new KeyBinding(_minorDecreaseValueCommand, new KeyGesture(Key.Down)));
            TextBox.InputBindings.Add(new KeyBinding(_updateValueStringCommand, new KeyGesture(Key.Enter)));

            TextBox.InputBindings.Add(new KeyBinding(_majorIncreaseValueCommand, new KeyGesture(Key.PageUp)));
            TextBox.InputBindings.Add(new KeyBinding(_majorDecreaseValueCommand, new KeyGesture(Key.PageDown)));

            TextBox.InputBindings.Add(new KeyBinding(_cancelChangesCommand, new KeyGesture(Key.Escape)));
        }

        private void ActionAnimChangedValue(bool isStart = true)
        {
            ActionAnimation(isStart, "StoryboardNotifyChangedValue");
        }

        private void ActionCustomAnimation(bool isStart = true)
        {
            ActionAnimation(isStart, "StoryboardNotifyCustomOperation");
        }

        private void ActionAnimation(bool isStart, string storyboardName)
        {
            var sb = this.TryFindResource(storyboardName) as Storyboard;
            if (sb != null && Border != null)
            {
                if (isStart) sb.Begin(Border, true);
                else sb.Stop(Border);
            }
        }

        private void CancelChanges()
        {
            TextBox.Undo();
        }

        protected void IncreaseValue(Boolean minor)
        {
            // Get result by processing of formula
            var valueFormula = ParseStringToFormula(TextBox.Text, MinorIncreaseDeltaFormula);
            if (valueFormula.HasValue)
            {
                Value = valueFormula.Value;
                return;
            }

            // Get the value that's currently in the TextBox.Text
            var value = ParseStringToDecimal(TextBox.Text);

            // Coerce the value to min/max
            CoerceValueToBounds(ref value);

            // Only change the value if it has any meaning
            if (value <= MaxValue)
            {
                if (minor)
                {
                    if (IsValueWrapAllowed && value + MinorDelta > MaxValue)
                    {
                        value = MinValue;
                    }
                    else if (value + MinorDelta <= MaxValue)
                    {
                        value += MinorDelta;
                    }
                }
                else
                {
                    if (IsValueWrapAllowed && value + MajorDelta > MaxValue)
                    {
                        value = MinValue;
                    }
                    else if(value + MinorDelta <= MaxValue)
                    {
                        value += MajorDelta;
                    }
                }
            }
            Value = value;
        }

        private int? ParseStringToFormula(string text, string delta)
        {
            if (!delta.StartsWith("="))
                return null;

            var t = new TypeRegistry();
            t.RegisterSymbol("x", int.Parse(text));
            t.RegisterSymbol("Math", typeof(Math));
            var expression = new CompiledExpression {StringToParse = delta.Substring(1), TypeRegistry = t};
            int result;
            try
            {
               result = Convert.ToInt32(expression.Eval());
            }
            catch (ExpressionEvaluator.Parser.ExpressionParseException e)
            {
                Debug.Print($">>> Error:\n + {e}");
                return null;
            }
            return result;
        }

        protected void DecreaseValue(Boolean minor)
        {
            // Get result by processing of formula
            var valueFormula = ParseStringToFormula(TextBox.Text, MinorDecreaseDeltaFormula);
            if (valueFormula.HasValue)
            {
                Value = valueFormula.Value;
                return;
            }

            // Get the value that's currently in the _textBox.Text
            var value = ParseStringToDecimal(TextBox.Text);

            // Coerce the value to min/max
            CoerceValueToBounds(ref value);

            // Only change the value if it has any meaning
            if (value >= MinValue)
            {
                if (minor)
                {
                    if (IsValueWrapAllowed && value - MinorDelta < MinValue)
                    {
                        value = MaxValue;
                    }
                    else if (value - MinorDelta >= MinValue)
                    {
                        value -= MinorDelta;
                    }
                }
                else
                {
                    if (IsValueWrapAllowed && value - MajorDelta < MinValue)
                    {
                        value = MaxValue;
                    }
                    else if (value - MajorDelta >= MinValue)
                    {
                        value -= MajorDelta;
                    }
                }
            }
            Value = value;
        }
        #endregion
        #region Keyboard Input
        private Decimal ParseStringToDecimal(String source)
        {
            Decimal value;
            Decimal.TryParse(source, out value);

            return value;
        }
        #endregion

        private void CoerceValueToBounds(ref Decimal value)
        {
            if (value < MinValue)
            {
                value = MinValue;
            }
            else if (value > MaxValue)
            {
                value = MaxValue;
            }
        }

        #region Decimal Places
        public static readonly DependencyProperty DecimalPlacesProperty =
            DependencyProperty.Register("DecimalPlaces", typeof(Int32), typeof(NumericUpDown),
                                new PropertyMetadata(0, OnDecimalPlacesChanged,
                                                        CoerceDecimalPlaces));

        public Int32 DecimalPlaces
        {
            get { return (Int32)GetValue(DecimalPlacesProperty); }
            set { SetValue(DecimalPlacesProperty, value); }
        }

        private static void OnDecimalPlacesChanged(DependencyObject element,
                                                    DependencyPropertyChangedEventArgs e)
        {
            var control = (NumericUpDown)element;
            var decimalPlaces = (Int32)e.NewValue;

            control.Culture.NumberFormat.NumberDecimalDigits = decimalPlaces;

            if (control.IsDecimalPointDynamic)
            {
                control.IsDecimalPointDynamic = false;
                control.InvalidateProperty(ValueProperty);
                control.IsDecimalPointDynamic = true;
            }
            else
            {
                control.InvalidateProperty(ValueProperty);
            }
        }

        private static object CoerceDecimalPlaces(DependencyObject element, Object baseValue)
        {
            var decimalPlaces = (Int32)baseValue;
            var control = (NumericUpDown)element;

            if (decimalPlaces < control.MinDecimalPlaces)
            {
                decimalPlaces = control.MinDecimalPlaces;
            }
            else if (decimalPlaces > control.MaxDecimalPlaces)
            {
                decimalPlaces = control.MaxDecimalPlaces;
            }

            return decimalPlaces;
        }

        protected readonly CultureInfo Culture;

        #endregion

        #region MaxDecimalPlacesProperty
        public static readonly DependencyProperty MaxDecimalPlacesProperty =
            DependencyProperty.Register("MaxDecimalPlaces", typeof(Int32), typeof(NumericUpDown),
                                new PropertyMetadata(28, OnMaxDecimalPlacesChanged,
                                                        CoerceMaxDecimalPlaces));

        public Int32 MaxDecimalPlaces
        {
            get { return (Int32)GetValue(MaxDecimalPlacesProperty); }
            set { SetValue(MaxDecimalPlacesProperty, value); }
        }

        private static void OnMaxDecimalPlacesChanged(DependencyObject element,
                                                DependencyPropertyChangedEventArgs e)
        {
            var control = (NumericUpDown)element;

            control.InvalidateProperty(DecimalPlacesProperty);
        }

        private static object CoerceMaxDecimalPlaces(DependencyObject element, Object baseValue)
        {
            var maxDecimalPlaces = (Int32)baseValue;

            var control = (NumericUpDown)element;

            if (maxDecimalPlaces > 28)
            {
                maxDecimalPlaces = 28;
            }
            else if (maxDecimalPlaces < 0)
            {
                maxDecimalPlaces = 0;
            }
            else if (maxDecimalPlaces < control.MinDecimalPlaces)
            {
                control.MinDecimalPlaces = maxDecimalPlaces;
            }

            return maxDecimalPlaces;
        }
        #endregion

        #region MinDecimalPlacesProperty 
        public static readonly DependencyProperty MinDecimalPlacesProperty =
            DependencyProperty.Register("MinDecimalPlaces", typeof(Int32), typeof(NumericUpDown),
                                new PropertyMetadata(0, OnMinDecimalPlacesChanged,
                                                        CoerceMinDecimalPlaces));

        public Int32 MinDecimalPlaces
        {
            get { return (Int32)GetValue(MinDecimalPlacesProperty); }
            set { SetValue(MinDecimalPlacesProperty, value); }
        }

        private static void OnMinDecimalPlacesChanged(DependencyObject element,
                                                DependencyPropertyChangedEventArgs e)
        {
            var control = (NumericUpDown)element;

            control.InvalidateProperty(DecimalPlacesProperty);
        }

        private static object CoerceMinDecimalPlaces(DependencyObject element, Object baseValue)
        {
            var minDecimalPlaces = (Int32)baseValue;

            var control = (NumericUpDown)element;

            if (minDecimalPlaces < 0)
            {
                minDecimalPlaces = 0;
            }
            else if (minDecimalPlaces > 28)
            {
                minDecimalPlaces = 28;
            }
            else if (minDecimalPlaces > control.MaxDecimalPlaces)
            {
                control.MaxDecimalPlaces = minDecimalPlaces;
            }

            return minDecimalPlaces;
        }
        #endregion

        #region Truncation of overflowing
        public Int32 GetDecimalPlacesCount(String valueString)
        {
            return valueString.SkipWhile(c => c.ToString(Culture)
                    != Culture.NumberFormat.NumberDecimalSeparator).Skip(1).Count();
        }

        private Decimal TruncateValue(String valueString, Int32 decimalPlaces)
        {
            var endPoint = valueString.Length - (decimalPlaces - DecimalPlaces);
            var tempValueString = valueString.Substring(0, endPoint);

            return Decimal.Parse(tempValueString, Culture);
        }
        #endregion
        #region Dynamic decimal point
        public static readonly DependencyProperty IsDecimalPointDynamicProperty =
            DependencyProperty.Register("IsDecimalPointDynamic", typeof(Boolean), typeof(NumericUpDown),
                                new PropertyMetadata(false));

        public Boolean IsDecimalPointDynamic
        {
            get { return (Boolean)GetValue(IsDecimalPointDynamicProperty); }
            set { SetValue(IsDecimalPointDynamicProperty, value); }
        }
        #endregion

        #region Adding the thousand separator
        public static readonly DependencyProperty IsThousandSeparatorVisibleProperty =
            DependencyProperty.Register("IsThousandSeparatorVisible", typeof(Boolean), typeof(NumericUpDown),
                                new PropertyMetadata(false, OnIsThousandSeparatorVisibleChanged));

        public Boolean IsThousandSeparatorVisible
        {
            get { return (Boolean)GetValue(IsThousandSeparatorVisibleProperty); }
            set { SetValue(IsThousandSeparatorVisibleProperty, value); }
        }

        private static void OnIsThousandSeparatorVisibleChanged(DependencyObject element,
                                                DependencyPropertyChangedEventArgs e)
        {
            var control = (NumericUpDown)element;

            control.InvalidateProperty(ValueProperty);
        }
        #endregion

        #region  Adding minor and major deltas

        public static readonly DependencyProperty MinorDeltaProperty =
            DependencyProperty.Register("MinorDelta", typeof(Decimal), typeof(NumericUpDown),
                                new PropertyMetadata(1m, OnMinorDeltaChanged,
                                                        CoerceMinorDelta));

        public Decimal MinorDelta
        {
            get { return (Decimal)GetValue(MinorDeltaProperty); }
            set { SetValue(MinorDeltaProperty, value); }
        }

        private static void OnMinorDeltaChanged(DependencyObject element,
                                                DependencyPropertyChangedEventArgs e)
        {
            var minorDelta = (Decimal)e.NewValue;
            var control = (NumericUpDown)element;

            if (minorDelta > control.MajorDelta)
            {
                control.MajorDelta = minorDelta;
            }
        }

        private static object CoerceMinorDelta(DependencyObject element, Object baseValue)
        {
            var minorDelta = (Decimal)baseValue;

            return minorDelta;
        }

        public static readonly DependencyProperty MajorDeltaProperty =
            DependencyProperty.Register("MajorDelta", typeof(Decimal), typeof(NumericUpDown),
                                new PropertyMetadata(10m, OnMajorDeltaChanged,
                                                        CoerceMajorDelta));

        public Decimal MajorDelta
        {
            get { return (Decimal)GetValue(MajorDeltaProperty); }
            set { SetValue(MajorDeltaProperty, value); }
        }

        private static void OnMajorDeltaChanged(DependencyObject element,
                                                DependencyPropertyChangedEventArgs e)
        {
            var majorDelta = (Decimal)e.NewValue;
            var control = (NumericUpDown)element;

            if (majorDelta < control.MinorDelta)
            {
                control.MinorDelta = majorDelta;
            }
        }

        private static object CoerceMajorDelta(DependencyObject element, Object baseValue)
        {
            var majorDelta = (Decimal)baseValue;

            return majorDelta;
        }
        #endregion

        #region Allowing auto selection
        public static readonly DependencyProperty IsAutoSelectionActiveProperty =
            DependencyProperty.Register("IsAutoSelectionActive", typeof(Boolean), typeof(NumericUpDown),
                                new PropertyMetadata(false));

        public Boolean IsAutoSelectionActive
        {
            get { return (Boolean)GetValue(IsAutoSelectionActiveProperty); }
            set { SetValue(IsAutoSelectionActiveProperty, value); }
        }
        #endregion

        #region Value wrap-around
        public static readonly DependencyProperty IsValueWrapAllowedProperty =
            DependencyProperty.Register("IsValueWrapAllowed", typeof(Boolean), typeof(NumericUpDown),
                                new PropertyMetadata(false));

        public Boolean IsValueWrapAllowed
        {
            get { return (Boolean)GetValue(IsValueWrapAllowedProperty); }
            set { SetValue(IsValueWrapAllowedProperty, value); }
        }
        #endregion



        #region Minor Delta Cast String
        public static readonly DependencyProperty MinorDecreaseDeltaFormulaProperty =
            DependencyProperty.Register("MinorDecreaseDeltaFormula", typeof(string), typeof(NumericUpDown),
                                new PropertyMetadata("", OnMinorDecreaseDeltaFormulaChanged,
                                                        CoerceMinorDecreaseFormulaDelta));

        public string MinorDecreaseDeltaFormula
        {
            get { return (string)GetValue(MinorDecreaseDeltaFormulaProperty); }
            set { SetValue(MinorDecreaseDeltaFormulaProperty, value); }
        }

        private static void OnMinorDecreaseDeltaFormulaChanged(DependencyObject element,
                                                DependencyPropertyChangedEventArgs e)
        {
            var newValue = (string)e.NewValue;
            var control = (NumericUpDown)element;

            control.MinorDecreaseDeltaFormula = newValue;
        }

        private static object CoerceMinorDecreaseFormulaDelta(DependencyObject element, Object baseValue)
        {
            var delta = (string)baseValue;

            return delta;
        }
        public static readonly DependencyProperty MinorIncreaseDeltaFormulaProperty =
            DependencyProperty.Register("MinorIncreaseDeltaFormula", typeof(string), typeof(NumericUpDown),
                                new PropertyMetadata("", OnMinorIncreaseDeltaFormulaChanged,
                                                        CoerceMinorIncreaseFormulaDelta));

        public string MinorIncreaseDeltaFormula
        {
            get { return (string)GetValue(MinorIncreaseDeltaFormulaProperty); }
            set { SetValue(MinorIncreaseDeltaFormulaProperty, value); }
        }

        private static void OnMinorIncreaseDeltaFormulaChanged(DependencyObject element,
                                                DependencyPropertyChangedEventArgs e)
        {
            var newValue = (string)e.NewValue;
            var control = (NumericUpDown)element;

            control.MinorIncreaseDeltaFormula = newValue;
        }

        private static object CoerceMinorIncreaseFormulaDelta(DependencyObject element, Object baseValue)
        {
            var delta = (string)baseValue;

            return delta;
        }

        #endregion

        #region Custom Animation

        public bool IsCustomAnimationRunning
        {
            get { return (bool)GetValue(IsCustomAnimationRunningProperty); }
            set { SetValue(IsCustomAnimationRunningProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCustomAnimationRunning.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCustomAnimationRunningProperty =
            DependencyProperty.Register("IsCustomAnimationRunning", typeof(bool), typeof(NumericUpDown), 
                new PropertyMetadata(false, (elem, args) =>
                {
                    var control = (NumericUpDown)elem;
                    var newValue = (bool)args.NewValue;
                    control.ActionCustomAnimation(newValue);
                } ));

        #endregion

        #region AnimationOnChangeValue

        protected static bool IsSystemChangedValue = false;

        public bool IsAnimChangedValue
        {
            get { return (bool)GetValue(IsAnimChangedValueProperty); }
            set { SetValue(IsAnimChangedValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAnimChangedValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsAnimChangedValueProperty =
            DependencyProperty.Register("IsAnimChangedValue", typeof(bool), typeof(NumericUpDown), 
                new PropertyMetadata(false, (elem, e)=> {
                    var control = (NumericUpDown)elem;
                    var newValue = (bool)e.NewValue;
                    if (!newValue && control.IsStartAnimChangedValue)
                        control.ActionAnimChangedValue(false);
                }));

        /// <summary>
        /// For Mvvm - stop the animation of control after assuming value (ViewModel) by enter button's click
        /// </summary>
        public bool IsStartAnimChangedValue
        {
            get { return (bool)GetValue(IsStartAnimChangedValueProperty); }
            set { SetValue(IsStartAnimChangedValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsAnimChangedValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsStartAnimChangedValueProperty =
            DependencyProperty.Register("IsStartAnimChangedValue", typeof(bool), typeof(NumericUpDown),
                new PropertyMetadata(false,
                    (element, args) => {
                        var control = (NumericUpDown)element;
                        var newValue = (bool)args.NewValue;
                        if (control.IsAnimChangedValue)
                            control.ActionAnimChangedValue(newValue);
                    }));

        #endregion

    }



}
