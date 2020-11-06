
namespace Common
{
    using System.Windows.Controls;
    using System.Windows.Data;
    using ViewModel;
    using Prism.Regions;
    using System.Windows;
    using TabControl = System.Windows.Controls.TabControl;

    public class TabControlAdapter: RegionAdapterBase<TabControl>
    {
        public TabControlAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        #region Overrides of RegionAdapterBase<TabControl>

        private static int _i = 0;
        protected override void Adapt(IRegion region, TabControl regionTarget)
        {
            region.ActiveViews.CollectionChanged += (sender, args) =>
            {
                switch (args.Action)
                {
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                        
                        foreach (UserControl view in args.NewItems)
                        {
                            if (view != null)
                            {
                                TabViewModel viewModel = view.DataContext as TabViewModel;
                                TabItem tab = new TabItem
                                {
                                    DataContext = view.DataContext,
                                    Content = view,
                                    Margin = new Thickness(_i*20,-1,_i*-23,-1),
                                };
                                ++_i;
                                if (viewModel != null)
                                {
                                    tab.SetBinding(HeaderedContentControl.HeaderProperty, new Binding());
                                    tab.Style = regionTarget.ItemContainerStyle;
                                    tab.Header = viewModel.TabModel?.Title;
                                }
                                regionTarget.Items.Add(tab);
                            }
                        }
                        break;
                    case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                        //foreach (UserControl view in args.OldItems)
                        //{
                        //    TabItem viewTab = regionTarget.Items.Cast<TabItem>().Single(o => o.DataContext == view.DataContext);
                        //    regionTarget.Items.Remove(viewTab);
                        //}
                        break;
                }
            };
        }

        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }

        #endregion
    }
}