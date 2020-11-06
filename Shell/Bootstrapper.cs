
namespace Shell
{
    using Catel;
    using Catel.IoC;
    using Common;
    using Common.Models;
    using Common.Models.Interfaces;
    using Common.Services;
    using Common.Services.Interfaces;
    using Microsoft.Practices.Unity;
    using Modules.Forms.CaptureFapch;
    using Modules.Forms.ControlFapch;
    using Modules.Forms.ControlSrch;
    using Modules.Forms.FormDebug;
    using Modules.ManipulationsCode;
    using Modules.RangeReproductionF;
    using Modules.FrequencyModulation;
    using Modules.RebuildFrequency;
    using Modules.Synthesizers;
    using Modules.ControlFapch;
    using Modules.CaptureFapch;
    using Modules.StatusBar;
    using Prism.Modularity;
    using Prism.Regions;
    using Views;

    /// <summary>
    /// The bootstrapper.
    /// </summary>
    public class Bootstrapper : BootstrapperBase<ShellView, ConfigurationModuleCatalog>
    {
        #region Method Overrides

        /// <summary>
        /// Configures the <see cref="IUnityContainer"/>. May be overwritten in a derived class to add specific
        /// type mappings required by the application.
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.CanResolveNonAbstractTypesWithoutRegistration = true;

            // TODO: Register types of repositories or ?services? !
            Container.RegisterType<IRemoteControlService, RemoteControlService>();

            Container.RegisterType<IStatusBarRepository, StatusBarRepository>();

            Container.RegisterType<ISynthesizerRepository, SynthesizerRepository>();
            Container.RegisterType<ICodeManipulationRepository, CodeManipulationRepository>();
            Container.RegisterType<IRangeReproductionFRepository, RangeReproductionFRepository>();
            Container.RegisterType<IFrequencyModulationRepository, FrequencyModulationRepository>();
            Container.RegisterType<IRebuildFrequencyRepository, RebuildFrequencyRepository>();
            Container.RegisterType<IControlFapchRepository, ControlFapchRepository>();
            Container.RegisterType<ICaptureFapchRepository, CaptureFapchRepository>();
            Container.RegisterType<IDebugModelRepository, DebugModelRepository>();
        }

        /// <summary>
        /// Configures the <see cref="T:Microsoft.Practices.Prism.Modularity.IModuleCatalog"/> used by Prism.
        /// </summary>
        protected override void ConfigureModuleCatalog()
        {
            ModuleCatalog.Initialize();

            // Tabs for main shell view
            ModuleCatalog.AddModule(typeof (FormControlSrchModule));
            ModuleCatalog.AddModule(typeof (FormCaptureFapchModule));
            ModuleCatalog.AddModule(typeof (FormControlFapchModule));
            ModuleCatalog.AddModule(typeof (FormDebugModule));
            // Status bar
            ModuleCatalog.AddModule(typeof (StatusBarModule));

            //TODO: add list loaded modules
            // Block of modules
            ModuleCatalog.AddModule(typeof (SynthesizersModule));
            ModuleCatalog.AddModule(typeof (CodeManipulationModule));
            ModuleCatalog.AddModule(typeof (RangeReproductionFModule));
            ModuleCatalog.AddModule(typeof (FrequencyModulationModule));
            ModuleCatalog.AddModule(typeof (RebuildFrequencyModule));
            ModuleCatalog.AddModule(typeof (ControlFapchModule));
            ModuleCatalog.AddModule(typeof (CaptureFapchModule));
        }

        /// <summary>
        /// Configures the default region adapter mappings to use in the application, in order 
        /// to adapt UI controls defined in XAML to use a region and register it automatically.
        /// </summary>
        /// <returns>
        /// The RegionAdapterMappings instance containing all the mappings.
        /// </returns>
        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            // Call base method
            var mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(System.Windows.Controls.TabControl), Container.ResolveType<TabControlAdapter>());
            return mappings;
        }
        #endregion
    }
}