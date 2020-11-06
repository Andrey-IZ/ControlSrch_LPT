
namespace Common.Models.Interfaces
{
    using System.Collections.Generic;

    public interface ISynthesizerRepository
    {
        ISynthesizer GetSynthesizerById(int id);

        IRebuildStep RebuildStep { get; }

        IEnumerable<ISynthesizer> GetSynthesizers();

    }
}