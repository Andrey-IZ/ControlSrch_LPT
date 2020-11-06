namespace Common.Models.Interfaces
{
    public interface IGun
    {
        int Id { get; set; } 

        string Header { get; set; }
        bool IsActiveGunState { get; set; }
        int CurrentGunValue { get; set; }
        int MinGunValue { get; set; }
        int MaxGunValue { get; set; }
        decimal DeltaMinor { get; set; }
        int DecimalPlaces { get; set; }
    }
}