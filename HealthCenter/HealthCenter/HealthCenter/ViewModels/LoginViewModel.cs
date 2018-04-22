namespace HealthCenter.ViewModels
{
    using Views;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Helpers;
    using Domain;

    public class LoginViewModel : BaseViewModel
    {

        #region Services
        private ApiService apiService;
        private DataService dataService;
        #endregion

        #region Attributes
        private string email;
        private string password;
        private bool isRunning;
        private bool isEnabled;

        #endregion

        #region Properties
        public string Email
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string Password
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }


        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        public bool IsRememberme
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.IsRememberme = true;
            this.IsEnabled = true;
            this.Email = "juanb19@hotmail.es";
            this.Password = "Developer123.";
        }
        #endregion

        #region Commands
        public ICommand LoginFacebookComand
        {
            get
            {
                return new RelayCommand(LoginFacebook);
            }
        }

        private async void LoginFacebook()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginFacebookPage());

        }

        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                  Languages.Error,
                  Languages.EmailValidation,
                  Languages.Accept);
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                      Languages.Error,
                      Languages.PasswordValidator,
                      Languages.Accept);
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {

                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                         Languages.Error,
                         Languages.ErrorConnection,
                         Languages.Accept);
                return;
            }

            //var token = await this.apiService.GetToken(
            //    "http://healthcenterapi.azurewebsites.net",
            //    this.Email,
            //    this.Password
            //    );

            //if (token == null)
            //{
            //    this.IsRunning = false;
            //    this.IsEnabled = true;
            //    await Application.Current.MainPage.DisplayAlert(
            //      Languages.Error,
            //      Languages.ErrorToken,
            //      Languages.Accept);
            //    return;
            //}

            //if (string.IsNullOrEmpty(token.AccessToken))
            //{
            //    this.IsRunning = false;
            //    this.IsEnabled = true;
            //    await Application.Current.MainPage.DisplayAlert(
            //        Languages.Error,
            //        Languages.ErrorPassword,
            //        Languages.Accept);
            //    this.Password = string.Empty;
            //    return;
            //}

            //var user = await this.apiService.GetUserByEmail(
            //    "http://healthcenterapi.azurewebsites.net",
            //    "/api",
            //    "/Users/GetUserByEmail",
            //    token.TokenType,
            //    token.AccessToken,
            //    this.Email);

            //var userLocal = Converter.ToUserLocal(user);
            //userLocal.Password = this.Password;

            //var mainViewModel = MainViewModel.GetInstance();

            //mainViewModel.Dates = new DatesViewModel();
            //mainViewModel.Token = token;
            //mainViewModel.User = userLocal;

            if (this.IsRememberme)
            {

                Settings.IsRememberme = "true";
            }
            else
            {
                Settings.IsRememberme = "false";
            }

            //this.dataService.DeleteAllAndInsert(userLocal);
            //this.dataService.DeleteAllAndInsert(token);


            //await Application.Current.MainPage.Navigation.PushAsync(new MasterPage());
            Application.Current.MainPage = new MasterPage();


            this.IsRunning = false;
            this.IsEnabled = true;

            this.Email = string.Empty;
            this.Password = string.Empty;
        }

        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }
        }

        private async void Register()
        {
            MainViewModel.GetInstance().Register = new RegisterViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }


        #endregion
    }
}
