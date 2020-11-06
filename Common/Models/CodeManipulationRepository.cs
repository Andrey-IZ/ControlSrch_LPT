
namespace Common.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;

    public class CodeManipulationRepository: ICodeManipulationRepository
    {
        public CodeManipulationRepository()
        {
            RebuildStep = new RebuildStep
            {
                CurrentStepValue = 1,
                MaxStepValue = 2,
                MinStepValue = 1,
                MinorDelta = 1,
            };
            GunsList = new List<IGun>
            {
                new Gun    // ГУН1
                {
                    Id = 1,
                    Header = "ГУН1",
                    CurrentGunValue = 0,
                    IsActiveGunState = false,
                    MaxGunValue = 1,
                    MinGunValue = 0,
                    DeltaMinor = 1,
                    DecimalPlaces = 0,
                },
                new Gun    // ГУН2
                {
                    Id = 2,
                    Header = "ГУН2",
                    CurrentGunValue = 0,
                    IsActiveGunState = false,
                    MaxGunValue = 1,
                    MinGunValue = 0,
                    DeltaMinor = 1,
                    DecimalPlaces = 0,
                }
            };
        }

        public List<IGun> GunsList { get; set; }

        #region Implementation of ICodeManipulationRepository

        public IGun GetGunById(int id)
        {
            lock (GunsList)
            {
                return (from gun in GunsList
                        where gun.Id == id
                        select gun).FirstOrDefault();
            }
        }

        public IRebuildStep RebuildStep { get; }

        public IEnumerable<IGun> GetGuns()
        {
            lock (GunsList)
            {
                return from gun in GunsList
                       select gun;
            }
        }

        #endregion
    }
}