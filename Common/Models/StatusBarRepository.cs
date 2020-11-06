
namespace Common.Models
{
    using System.Collections.Generic;
    using Common.Models.Interfaces;

    public class StatusBarRepository : IStatusBarRepository
    {
        private readonly IStatusBar _statusBar;
        public StatusBarRepository()
        {
            _statusBar = new StatusBar
            {
                CurrentLpt = 0,
                IsKnp = true,
                IsLinkOn = false,
                LptAddressList = null,
            };
        }

        #region Implementation of IStatusBarRepository

        public IStatusBar GetStatusBar()
        {
            return _statusBar;
        }

        #endregion
    }
}