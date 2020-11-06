namespace Common.Models.Interfaces
{
    public interface IFrequencyModulation
    {
        bool IsOnUk1 { get; set; }
        bool IsOnUk2 { get; set; }
        uint AmplitudeCode { get; set; }
        int MinAmplitudeCode { get; set; }
        int MaxAmplitudeCode { get; set; }
        bool IsNoise { get; set; }
        uint AmplitudeCodeMseq { get; set; }
        int MinorDeltaAmplitudeCode { get; set; }
        int MinAmplitudeCodeMseq { get; set; }
        int MaxAmplitudeCodeMseq { get; set; }
        bool IsMseq { get; set; }
    }
}