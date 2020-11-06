using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace WpfCustomControlLibraryVS2015
{

    [TemplatePart(Name = "PART_LeftButton", Type = typeof(RepeatButton))]
    [TemplatePart(Name = "PART_RightButton", Type = typeof(RepeatButton))]
    public class NumericUpDownLeftRight : NumericUpDown
    {
        static NumericUpDownLeftRight()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDownLeftRight),
                new FrameworkPropertyMetadata(typeof(NumericUpDownLeftRight)));
        }

        #region Overrides of NumericUpDown

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            AttachToVisualTree();
            AttachCommands();
        }

        private void AttachCommands()
        {
            CommandBindings.Add(new CommandBinding(_leftArrowCommand, (a, b) =>
            {
                LeftHltCommand.Execute(a);

                if (LeftCommand != null) LeftCommand.Execute(a);
                else DecreaseValue(false);
            }));
            CommandBindings.Add(new CommandBinding(LeftHltCommand, (a, b) =>
            {
                var sb = this.TryFindResource("StoryboardRepeatButtonLeft") as Storyboard;
                sb?.Begin(IncreaseButton);
            }));
            CommandBindings.Add(new CommandBinding(_rightArrowCommand, (a, b) =>
            {
                RightHltCommand.Execute(a);

                if (RightButton != null) RightCommand.Execute(a);
                else IncreaseValue(false);
            }));
            CommandBindings.Add(new CommandBinding(RightHltCommand, (a, b) =>
            {
                var sb = this.TryFindResource("StoryboardRepeatButtonRight") as Storyboard;
                sb?.Begin(IncreaseButton);
            }));

            TextBox.InputBindings.Add(new KeyBinding(_leftArrowCommand, new KeyGesture(Key.Left)));
            TextBox.InputBindings.Add(new KeyBinding(_rightArrowCommand, new KeyGesture(Key.Right)));
        }

        #endregion
        private void AttachToVisualTree()
        {
            AttachLeftButton();
            AttachRightButton();
        }

        protected RepeatButton RightButton;

        private void AttachRightButton()
        {
            var rightButton = GetTemplateChild("PART_RightButton") as RepeatButton;
            if (rightButton != null)
            {
                RightButton = rightButton;
                RightButton.Focusable = false;
                RightButton.Command = _rightArrowCommand;
                RightButton.PreviewMouseLeftButtonDown += (sender, args) => RemoveFocus();
                RightButton.PreviewMouseRightButtonDown += ButtonOnPreviewMouseRightButtonDown; ;
            }
        }

        protected RepeatButton LeftButton;

        private void AttachLeftButton()
        {
            var leftButton = GetTemplateChild("PART_LeftButton") as RepeatButton;
            if (leftButton != null)
            {
                LeftButton = leftButton;
                LeftButton.Focusable = false;
                LeftButton.Command = _leftArrowCommand;
                LeftButton.PreviewMouseLeftButtonDown += (sender, args) => RemoveFocus();
                LeftButton.PreviewMouseRightButtonDown += ButtonOnPreviewMouseRightButtonDown; ;
            }
        }
        #region commands

        private readonly RoutedUICommand _leftArrowCommand =
            new RoutedUICommand("LeftArrowCommand", "LeftArrowCommand", typeof(NumericUpDownLeftRight));

        private readonly RoutedUICommand _rightArrowCommand =
                    new RoutedUICommand("RightArrowCommand", "RightArrowCommand", typeof(NumericUpDownLeftRight));

        #endregion

        #region Public Commands
        public ICommand LeftCommand
        {
            get { return (ICommand)GetValue(LeftCommandProperty); }
            set { SetValue(LeftCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IncreaseCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftCommandProperty =
            DependencyProperty.Register("LeftCommand", typeof(ICommand), typeof(NumericUpDownLeftRight),
                new UIPropertyMetadata(null));



        public ICommand LeftHltCommand
        {
            get { return (ICommand)GetValue(LeftHltCommandProperty); }
            set { SetValue(LeftHltCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LeftHltCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LeftHltCommandProperty =
            DependencyProperty.Register("LeftHltCommand", typeof(ICommand), typeof(NumericUpDownLeftRight), 
                new PropertyMetadata(new RoutedUICommand("LeftHltCommand", "LeftHltCommand", typeof(NumericUpDown))));


        public ICommand RightCommand
        {
            get { return (ICommand)GetValue(RightCommandProperty); }
            set { SetValue(RightCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DecreaseCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightCommandProperty =
            DependencyProperty.Register("RightCommand", typeof(ICommand), typeof(NumericUpDownLeftRight),
                new UIPropertyMetadata(null));

        public ICommand RightHltCommand
        {
            get { return (ICommand)GetValue(RightHltCommandProperty); }
            set { SetValue(RightHltCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RightHltCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RightHltCommandProperty =
            DependencyProperty.Register("RightHltCommand", typeof(ICommand), typeof(NumericUpDownLeftRight),
                new PropertyMetadata(new RoutedUICommand("RightHltCommand", "RightHltCommand", typeof(NumericUpDown))));

        #endregion
    }
}