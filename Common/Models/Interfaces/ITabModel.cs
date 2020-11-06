namespace Common.Models.Interfaces
{
    public interface ITabModel
    {
        string Title { get; }

        bool CanClose { get; set; }

        bool IsModified { get; }
    }
}