
namespace Common.Modularity
{
    using System;
    using Catel;
    using Catel.IoC;
    using Catel.Modules;
    using Catel.Windows.Controls;

    public class TabModuleBase : ModuleBase
    {
        protected virtual void InitTabRegion(string tabRegion, Type viewType, Type viewModelType)
        {
            var view = Container.ResolveType(viewType);
            // To show a header of TabItem
            var vm = Container.ResolveType(viewModelType);

            var userControl = view as UserControl;
            if (userControl != null) userControl.DataContext = vm;

            // Adds local instance of region into RegionManager
            RegionManager.Regions[tabRegion].Add(view, null, true);
        }

        #region Constructor

        public TabModuleBase(string moduleName, IModuleTracker moduleTracker = null, IServiceLocator container = null)
            : base(moduleName, moduleTracker, container)
        {
        }

        public TabModuleBase(string moduleName) : base(moduleName)
        {
        }

        #endregion
    }
}