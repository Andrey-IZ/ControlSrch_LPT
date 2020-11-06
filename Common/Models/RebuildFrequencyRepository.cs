namespace Common.Models
{
    using Interfaces;

    public class RebuildFrequencyRepository : IRebuildFrequencyRepository
    {
        private readonly IRebuildFrequency _rebuildFrequency;

        public RebuildFrequencyRepository()
        {
            _rebuildFrequency = new RebuildFrequency
            {
                IsAuto = false,
                IsManual = true,
                IsStop = false,
            };
        }

        #region Implementation of IRebuildFrequencyRepository

        public IRebuildFrequency GetRebuildFrequency()
        {
            return _rebuildFrequency;
        }

        #endregion
    }
}