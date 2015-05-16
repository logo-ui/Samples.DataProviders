using System;
using LogoUI.Samples.Client.Model.Contracts.UserManagement;

namespace LogoUI.Samples.Client.Model.Shared
{    
    public class UserContext
    {
        private static IUser _currentUser;

        public static event EventHandler CurrentChanged = delegate { };

        public UserContext(IUser user)
        {
            Current = user;
        }

        public static IUser Current
        {
            get { return _currentUser; }
            set
            {
                if (_currentUser == value)
                {
                    return;
                }

                _currentUser = value;
                OnCurrentChanged();
            }
        }

        private static void OnCurrentChanged()
        {
            CurrentChanged(null, EventArgs.Empty);
        }
    }
}
