namespace Common.Models.Interfaces
{
    public interface IRebuildStep
    {
        decimal CurrentStepValue { get; set; }
        decimal MinStepValue { get; set; }
        decimal MaxStepValue { get; set; }
        decimal MinorDelta { get; set; }
    }
}