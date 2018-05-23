
namespace HealthCenter.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using Helpers;
    using Views;
    using Xamarin.Forms;

    public class MenuItemViewModel : BaseViewModel
    {
        #region Properties
        private string filter;

        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);

            }
        }

        public string Icon
        {
            get;
            set;
        }


        public string Title
        {
            get;
            set;
        }


        public string PageName
        {
            get;
            set;
        }
        #endregion

        #region Commands
        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        }

        private void Navigate()
        {
            App.Master.IsPresented = false;
            var mainViewModel = MainViewModel.GetInstance();

            if (this.PageName == "LoginPage")
            {
                Settings.IsRememberme = "false";
                mainViewModel.Token = null;
                mainViewModel.User = null;
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }

            if (this.PageName == "MyDatesPage")
            {
                mainViewModel.MyDates = new MyDatesViewModel();
                App.Navigator.PushAsync(new MyDatesPage());
            }

            if (this.PageName == "MyProfilePage")
            {
                mainViewModel.MyProfile = new MyProfileViewModel();
                App.Navigator.PushAsync(new MyProfilePage());
            }

            if (this.PageName == "ChangePasswordPage")
            {
                mainViewModel.ChangePassword = new ChangePasswordViewModel();
                App.Navigator.PushAsync(new ChangePasswordPage());
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        private async void Search()
        {

            await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
                this.Filter,
                Languages.Accept);
            return;

            //Application.Current.MainPage = new FilterPage();

        }
        #endregion
    }
}
