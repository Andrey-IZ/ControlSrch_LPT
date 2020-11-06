namespace Common.Models.Interfaces
{
    public interface IControlFapch
    {
        bool IsDivisorInputSignal { get; set; }
        bool IsDivisorSupportingFreq { get; set; }
        bool IsPhaseDetectorAnalog { get; set; }
        bool IsPhaseDetectorDigital { get; set; }
    }
}