
namespace Common.Models
{
    using Interfaces;

    public class ControlFapchRepository : IControlFapchRepository
    {
        private readonly IControlFapch _controlFapch;

        public ControlFapchRepository()
        {
            _controlFapch = new ControlFapch
            {
                IsDivisorInputSignal = false,
                IsDivisorSupportingFreq = false,
                IsPhaseDetectorAnalog = false,
                IsPhaseDetectorDigital = false,
            };
        }

        #region Implementation of IControlFapchRepository

        public IControlFapch GetControlFapch()
        {
            return _controlFapch;
        }

        #endregion
    }
}