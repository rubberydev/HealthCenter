﻿

namespace HealthCenter
{
    using Xamarin.Forms;
    using Views;
    using Helpers;
    using ViewModels;
    using Services;
    using Models;
    using System;
    using System.Threading.Tasks;


    public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
		}

        #region Methods
        public static Action HideLoginView
        {
            get
            {
                return new Action(() => Application.Current.MainPage =
                                  new NavigationPage(new LoginPage()));
            }
        }

        public static async Task NavigateToProfile(FacebookResponse profile)
        {
            if (profile == null)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }

            var apiService = new ApiService();
            var dataService = new DataService();

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var token = await apiService.LoginFacebook(
                apiSecurity,
                "/api",
                "/Users/LoginFacebook",
                profile);

            if (token == null)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }

            //var user = await apiService.GetUserByEmail(
            //    apiSecurity,
            //    "/api",
            //    "/Users/GetUserByEmail",
            //    token.TokenType,
            //    token.AccessToken,
            //    token.UserName);

        //    UserLocal userLocal = null;
        //    if (user != null)
        //    {
        //        userLocal = Converter.ToUserLocal(user);
        //        dataService.DeleteAllAndInsert(userLocal);
        //        dataService.DeleteAllAndInsert(token);
        //    }

        //    var mainViewModel = MainViewModel.GetInstance();
        //    mainViewModel.Token = token;
        //    mainViewModel.User = userLocal;
        //    Settings.IsRememberme = "true";
        //    mainViewModel.Bibles = new BiblesViewModel();
        //    Application.Current.MainPage = new MasterPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        #endregion
    }
}
