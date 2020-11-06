namespace Common.Models.Interfaces
{
    public interface ISynthesizer
    {
        int Id { get; set; }

        string Header { get; set; }
        bool IsActiveState { get; set; }
        decimal CurrentF { get; set; }
        float MinF { get; set; }
        float MaxF { get; set; }
        decimal DeltaMinor { get; set; }
        int DecimalPlaces { get; set; }
    }
}