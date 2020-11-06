
namespace Common.ViewModel
{
    using Catel.MVVM;
    using Models.Interfaces;
    using Interfaces;

    public class TabViewModel : ViewModelBase, ITabViewModel
    {
        #region Implementation of ITabViewModel

        public ITabModel TabModel { get; set; }

        #endregion
    }
}