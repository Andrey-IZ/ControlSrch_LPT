
namespace Common.Models.Interfaces
{
    using System.Collections.Generic;

    public interface IStatusBar
    {
        int CurrentLpt { get; set; }

        IEnumerable<int> LptAddressList { get; set; }

        bool IsKnp { get; set; }

        bool IsLinkOn { get; set; }
    }
}