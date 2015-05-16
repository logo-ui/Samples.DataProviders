using System;
using Caliburn.Micro;
using LogoFX.UI.Commanding;
using LogoFX.UI.Navigation;
using Solid.Practices.Scheduling;

namespace LogoUI.Samples.Client.Gui.Shared.ViewModels
{    
    public abstract class TileViewModelBase<T> : Conductor<T>
        where T : class
    {        
        private readonly Func<T> _loadFunc;

        protected TileViewModelBase(
            string title, 
            INavigationService navigationService, 
            Func<T> loadFunc, 
            NavigationParameter parameter)
        {
            _title = title;
            _loadFunc = loadFunc;
            NavigationService = navigationService;
            NavigationParameter = parameter;
        }

        private string _title = String.Empty;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value)
                {
                    return;
                }

                _title = value;
                NotifyOfPropertyChange();
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            private set
            {
                if (_isBusy == value)
                {
                    return;
                }

                _isBusy = value;
                NotifyOfPropertyChange();
            }
        }

        public INavigationService NavigationService { get; set; }
        public NavigationParameter NavigationParameter { get; private set; }

        private bool _isErrorTextVisible;
        public bool IsErrorTextVisible
        {
            get { return _isErrorTextVisible; }
            private set
            {
                if (_isErrorTextVisible == value)
                {
                    return;
                }

                _isErrorTextVisible = value;
                NotifyOfPropertyChange();
            }
        }

        private string _errorText;
        public string ErrorText
        {
            get { return _errorText; }
            private set
            {
                if (_errorText == value)
                {
                    return;
                }

                _errorText = value;
                NotifyOfPropertyChange();
                IsErrorTextVisible = !String.IsNullOrEmpty(_errorText);
            }
        }

        private async void StartLoad()
        {
            ActivateItem(null);
            IsBusy = true;
            var taskFactory = TaskFactoryFactory.CreateTaskFactory();

            try
            {
                T result;

                try
                {
                    result = await taskFactory.StartNew(_loadFunc);                    
                }

                catch (Exception err)
                {
                    ErrorText = err.Message;
                    return;
                }

                var subtitleViewModel = result as ISubtitleViewModel;
                if (subtitleViewModel != null)
                {
                    Title = subtitleViewModel.Title;
                    subtitleViewModel.NotifyOn("Title", (o, o1) => Title = (string)o);
                }
                ActivateItem(result);
            }

            finally
            {
                IsBusy = false;
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            StartLoad();
        }        
    }

    public abstract class TileViewModelBase : TileViewModelBase<object>
    {
        protected TileViewModelBase(
            string title, 
            INavigationService navigationService, 
            Func<object> loadFunc, 
            NavigationParameter parameter)
            : base(title,navigationService, loadFunc, parameter)
        {
        }
    }

    public interface ISubtitleViewModel
    {
        string Title { get; }
    }
}