﻿using Caliburn.Micro;
using LogoFX.UI.ViewModels;
using LogoFX.UI.ViewModels.Interfaces;

namespace LogoUI.Samples.Client.Gui.Modules.Compliance.ViewModels
{
    public enum SecurityConsoleViewType
    {
        Grid,
        Cards
    }

    public sealed class ConsoleViewModel : Conductor<ComplianceListViewModel>, ICanBeBusy
    {        
        public ConsoleViewModel(ComplianceListViewModel listViewModel)
        {
            ListViewModel = listViewModel;
            ViewTypes = new EnumViewModel<SecurityConsoleViewType>();
            ActivateItem(ListViewModel);
        }

        private SecurityConsoleViewType _viewType = SecurityConsoleViewType.Grid;
        public SecurityConsoleViewType ViewType
        {
            get { return _viewType; }
            set
            {
                if (_viewType == value)
                {
                    return;
                }

                _viewType = value;
                NotifyOfPropertyChange();
            }
        }

        private bool _isBusy;        
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy == value)
                {
                    return;
                }

                _isBusy = value;
                NotifyOfPropertyChange();
            }
        }

        public override string DisplayName
        {
            get { return "Security & Compliance Console"; }
            set { }
        }

        public ComplianceListViewModel ListViewModel { get; private set; }

        public IHierarchicalViewModel ViewTypes { get; private set; }        

        private ComplianceRecordsFiltersViewModel _filters;
        public ComplianceRecordsFiltersViewModel Filters
        {
            get { return _filters; }
            private set
            {
                if (_filters != null)
                {
                    ScreenExtensions.TryDeactivate(_filters, false);
                }

                _filters = value;

                if (_filters != null)
                {
                    ScreenExtensions.TryActivate(_filters);
                }
            }
        }        

        private async void Update()
        {
            if (IsBusy)
            {
                return;
            }

            var filter = Filters.ActiveItem.Model;
            IsBusy = true;
            try
            {
                await ActiveItem.Update(filter);
            }

            finally
            {
                IsBusy = false;
            }
        }        

        protected override void OnActivate()
        {
            base.OnActivate();
            Filters = new ComplianceRecordsFiltersViewModel();
            Update();
        }
    }
}