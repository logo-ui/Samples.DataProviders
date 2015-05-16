using System;
using Caliburn.Micro;
using LogoFX.UI.Bootstrapping.SimpleContainer;
using LogoFX.UI.Navigation;
using LogoUI.Samples.Client.Gui.Modularity.Contracts;
using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace LogoUI.Samples.Client.Gui.Modularity
{
    public abstract class LogoUiModule<TViewModel> : ILogoUiModule, ICompositionModule<ExtendedSimpleIocContainer>
        where TViewModel : class, IScreen
    {
        private TViewModel _rootViewModel;
        private IIocContainerResolver _iocContainer;

        protected virtual void RegisterModuleOverrides(ExtendedSimpleIocContainer container, Func<object> lifetimeScopeAccess)
        {
            if (container.HasHandler(typeof(ILogoUiModule), Name) == false)
            {
                container.RegisterHandler(typeof(ILogoUiModule), null, c => this);
            }
            else
            {
                _rootViewModel = null;
            }
        }

        protected virtual TViewModel GetRootViewModel()
        {
            return _rootViewModel ?? (_rootViewModel = _iocContainer.Resolve<TViewModel>());
        }        

        public Type RootModelType
        {
            get { return typeof (TViewModel); }
        }

        protected abstract string GetName();

        protected abstract int GetOrder();

        protected virtual bool GetIsGroup()
        {
            return false;
        }

        public void RegisterModule(ExtendedSimpleIocContainer container)
        {
            _iocContainer = container;
            RegisterModuleOverrides(container, null);
        }

        public IScreen RootViewModel
        {
            get { return GetRootViewModel(); }
        }

        public string Name
        {
            get { return GetName(); }
        }

        public int Order
        {
            get { return GetOrder(); }
        }

        public bool IsGroup
        {
            get { return GetIsGroup(); }
        }

        public INavigationService NavigationService { get; set; }
    }
}