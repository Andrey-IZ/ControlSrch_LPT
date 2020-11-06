namespace Common.Models.Interfaces
{
    public interface IExitsAk
    {
        bool IsCheckedExit1 { get; set; }
        bool IsCheckedExit2 { get; set; }
        bool IsHighThresholdExit1 { get; set; }
        bool IsLowThresholdExit1 { get; set; }
        bool IsHighThresholdExit2 { get; set; }
        bool IsLowThresholdExit2 { get; set; }
        bool IsOnAk1 { get; set; }
        bool IsOnAk2 { get; set; }
    }
}