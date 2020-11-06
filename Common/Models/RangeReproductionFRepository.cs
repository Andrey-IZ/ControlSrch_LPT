
namespace Common.Models
{
    using Interfaces;

    public class RangeReproductionFRepository : IRangeReproductionFRepository
    {
        public RangeReproductionFRepository()
        {
            _exitsAk = new ExitsAk
            {
                IsCheckedExit1 = false,
                IsHighThresholdExit1 = false,
                IsLowThresholdExit1 = false,
                IsOnAk1 = false,

                IsCheckedExit2 = false,
                IsHighThresholdExit2 = false,
                IsLowThresholdExit2 = false,
                IsOnAk2 = false,
            };
        }

        private readonly IExitsAk _exitsAk;

        #region Implementation of IRangeReproductionFRepository

        public IExitsAk GetExitsAk()
        {
            return _exitsAk;
        }

        #endregion
    }
}