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
    using Domain;

    public partial class App : Application
	{
        #region Properties
        public static NavigationPage Navigator
        {
            get;
            internal set;
        }
        public static MasterPage Master

        {
            get;
            internal set;
        }
        #endregion

        public App ()
		{
			InitializeComponent();
            InitializeComponent();
            var mainViewModel = MainViewModel.GetInstance();

            if (Settings.IsRememberme == "true")
            {
                var dataService = new DataService();
                var user = dataService.First<UserLocal>(false);
                var token = dataService.First<TokenResponse>(false);

                if (token != null && token.Expires > DateTime.Now)
                {
                    mainViewModel.Token = token;
                    mainViewModel.User = user;
                    mainViewModel.Doctor = new DoctorViewModel();
                    mainViewModel.Menu = new MenuItemViewModel();
                    MainPage = new MasterPage();
                }
                else
                {
                    MainPage = new NavigationPage(new LoginPage());
                }
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }

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

        public static void Navigate_ToProfile<T>(T profile, string socialNetwork)
        {
            switch (socialNetwork)
            {
                case "Twitter":
                    TwitterResponse twitterResponse = profile as TwitterResponse;                                       
                    break;
            }            
        }

        public static async void __NavigateToProfile(LinkedInResponse profile)
        {            
             
            if (profile == null)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }

            var apiService = new ApiService();
            var dataService = new DataService();

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var token = await apiService.LoginLinkedIn(
                apiSecurity,
                "/api",
                "/Users/LoginLinkedIn",
                profile);

            if (token == null)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }
            toProcessUser(token);
        }        

        public static async Task NavigateToProfile_(InstagramResponse ResponseSocialNetwork)
        {    
            if (ResponseSocialNetwork == null)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }
            
            var apiService = new ApiService();
            var dataService = new DataService();

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var token = await apiService.LoginInstagram(
                apiSecurity,
                "/api",
                "/Users/LoginInstagram",
                ResponseSocialNetwork);

            if (token == null)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
                return;
            }
            toProcessUser(token);   
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
            toProcessUser(token);            
        }

        private static async void toProcessUser(TokenResponse tokenResponse)
        {
            var apiService = new ApiService();
            var dataService = new DataService();

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var user = await apiService.GetUserByEmail(
               apiSecurity,
               "/api",
               "/Users/GetUserByEmail",
               tokenResponse.TokenType,
               tokenResponse.AccessToken,
               tokenResponse.UserName);

            UserLocal userLocal = null;
            if (user != null)
            {
                userLocal = Converter.ToUserLocal(user);
                dataService.DeleteAllAndInsert(userLocal);
                dataService.DeleteAllAndInsert(tokenResponse);
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Token = tokenResponse;
            mainViewModel.User = userLocal;
            Settings.IsRememberme = "true";
            mainViewModel.Doctor = new DoctorViewModel();
            Application.Current.MainPage = new MasterPage();
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
