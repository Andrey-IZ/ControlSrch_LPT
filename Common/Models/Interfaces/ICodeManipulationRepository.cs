
namespace Common.Models.Interfaces
{
    using System.Collections.Generic;

    public interface ICodeManipulationRepository
    {
        IGun GetGunById(int id);

        IRebuildStep RebuildStep { get; }

        IEnumerable<IGun> GetGuns();
    }
}