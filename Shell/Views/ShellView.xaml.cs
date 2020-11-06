
namespace Shell.Views
{
    using Catel.Windows;

    public partial class ShellView : DataWindow
    {
        public ShellView(): base(DataWindowMode.Custom)
        {
            InitializeComponent();
        }

        private void DataWindow_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
