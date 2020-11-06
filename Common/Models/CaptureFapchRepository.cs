namespace Common.Models
{
    using Interfaces;

    public class CaptureFapchRepository:ICaptureFapchRepository
    {
        private readonly ICaptureFapch _captureFapch;

        public CaptureFapchRepository()
        {
            _captureFapch = new CaptureFapch
            {
                IsCaptureFapch1 = false,
                IsCaptureFapch2 = false,
            };
        }

        #region Implementation of ICaptureFapchRepository

        public ICaptureFapch GetCaptureFapch()
        {
            return _captureFapch;
        }

        #endregion
    }
}