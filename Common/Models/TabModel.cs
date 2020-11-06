
namespace Common.Models
{
    using Catel.Data;
    using Interfaces;

    public class TabModel: ModelBase, ITabModel
    {
        public TabModel(string title, bool canClose = false, bool isModified = false)
        {
            IsModified = isModified;
            Title = title;
            CanClose = canClose;
        }

        #region Implementation of ITabModel
        
        public string Title { get; set; }
        public bool CanClose { get; set; }
        public bool IsModified { get; }

        #endregion
    }
}