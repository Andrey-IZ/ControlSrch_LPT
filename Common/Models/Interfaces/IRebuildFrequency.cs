namespace Common.Models.Interfaces
{
    public interface IRebuildFrequency
    {
        bool IsManual { get; set; } 
        bool IsAuto { get; set; } 
        bool IsStop { get; set; } 
    }
}