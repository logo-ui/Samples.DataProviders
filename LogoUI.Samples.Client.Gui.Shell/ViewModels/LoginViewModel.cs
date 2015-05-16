﻿using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Principal;
using System.Windows.Input;
using Caliburn.Micro;
using LogoFX.Practices.IoC;
using LogoFX.UI.Commanding;
using LogoFX.UI.Navigation;
using LogoUI.Samples.Client.Gui.Shared.UiServices;
using LogoUI.Samples.Client.Gui.Shared.ViewModels;
using LogoUI.Samples.Client.Gui.Shell.Properties;
using LogoUI.Samples.Client.Model.Contracts;

namespace LogoUI.Samples.Client.Gui.Shell.ViewModels
{
    [Singleton]
    [NavigationViewModel(ConductorType = typeof(ShellViewModel), IsSingleton = true)]
    public sealed class LoginViewModel : Screen
    {
        private readonly ILoginService _loginService;
        private readonly INavigationService _navigationService;
        private readonly IShellCloseService _shellCloseService;

        public LoginViewModel(
            ILoginService loginService,
            INavigationService navigationService,
            IShellCloseService shellCloseService)
        {
            _loginService = loginService;
            _navigationService = navigationService;
            _shellCloseService = shellCloseService;
            _loginOptions = new ObservableCollection<string>
            {
                Resources.Windows_Authentication, 
                Resources.Network_Authentication
            };
        }

        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ??
                    (_loginCommand = ActionCommand
                    .When(() => !String.IsNullOrWhiteSpace(UserName))
                    .Do(async () =>
                    {
                        LoginFailureCause = null;
                        string password = Password;
                        Password = String.Empty;

                        IsBusy = true;
                        Exception error = null;
                        try
                        {
                            await _loginService.Login(UserName, password);
                        }

                        catch (Exception err)
                        {
                            error = err;
                        }

                        finally
                        {
                            IsBusy = false;
                        }

                        if (error != null)
                        {
                            LoginFailureCause = error.Message;
                            return;
                        }

                        OnLoginSuccess();
                    })
                    .RequeryOnPropertyChanged(this, () => UserName)
                    .RequeryOnPropertyChanged(this, () => Password));
            }
        }

        private readonly ObservableCollection<string> _loginOptions;
        public IEnumerable LoginOptions
        {
            get { return _loginOptions; }
        }

        private string _selectedLogin;
        public string SelectedLogin
        {
            get
            {
                if (string.IsNullOrEmpty(_selectedLogin))
                {
                    _selectedLogin = _loginOptions.First();
                    UserName = GetUserAuthenticated();
                }

                return _selectedLogin;
            }
            set
            {
                UserName = value == Resources.Network_Authentication ? string.Empty : GetUserAuthenticated();
                _selectedLogin = value;
                NotifyOfPropertyChange(() => SelectedLogin);
                NotifyOfPropertyChange(() => IsNetLogin);

            }
        }

        public bool IsNetLogin
        {
            get
            {
                return SelectedLogin == Resources.Network_Authentication;
            }
        }

        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ??
                       (_cancelCommand = ActionCommand
                           .Do(() => _shellCloseService.Close()));
            }
        }

        private string _userName = string.Empty;
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName == value)
                    return;

                _userName = value;
                NotifyOfPropertyChange(() => UserName);
            }
        }

        private string _password = string.Empty;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password == value)
                    return;

                _password = value;
                NotifyOfPropertyChange(() => Password);
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy == value)
                    return;

                _isBusy = value;
                NotifyOfPropertyChange(() => IsBusy);
            }
        }

        #region LoginFailureCause property

        /// <summary>
        /// LoginFailureCause property
        /// </summary>		
        public string LoginFailureCause
        {
            get { return _loginFailureCause; }
            set
            {
                if (_loginFailureCause == value)
                    return;

                _loginFailureCause = value;
                NotifyOfPropertyChange(() => LoginFailureCause);
                NotifyOfPropertyChange(() => IsLoginFailureTextVisible);
            }
        }

        private string _loginFailureCause;


        #endregion

        public bool IsLoginFailureTextVisible
        {
            get { return string.IsNullOrWhiteSpace(LoginFailureCause) == false; }
        }


        // ReSharper disable RedundantOverridenMember
        protected override void OnActivate()
        {
            base.OnActivate();

#if SKIPLOGIN
            UserName = "a";
            Password = "a";
            LoginCommand.Execute(null);
#endif
        }
        // ReSharper restore RedundantOverridenMember

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            _navigationService.ClearHistory(false);
        }

        private void OnLoginSuccess()
        {
            _navigationService.Navigate<IMainViewModel>();
        }

        //Checks if the user Authentication 
        private string GetUserAuthenticated()
        {
            var accountToken = WindowsIdentity.GetCurrent();
            if (accountToken != null && accountToken.IsAuthenticated)
                return accountToken.Name;

            LoginFailureCause = "UnAuthorized User";
            return "UnAuthorized User";
        }
    }
}