namespace HealthCenter.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Services;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class MyDatesItemViewModel : Appointments
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Constructors
        public MyDatesItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        public ICommand CancelAppointmentCommand
        {

            get
            {
                return new RelayCommand(CancelAppointment);
            }
        }

        private async void CancelAppointment()
        {
            var mainviewModel = MainViewModel.GetInstance();

            var resc = await Application.Current.MainPage.DisplayAlert(
                   Languages.ConfirmLabel,
                   "Are you sure you want to cancel this appointment scheduled for day " + this.DateSchedule_ + " at " + StartHour_ + "?",
                   Languages.Accept,
                   Languages.Cancel);

            if (!resc)
            {
                return;
            }

            if (AgendaId == 0)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.DateValidator,
                    Languages.Accept);
                return;
            }

            UserLocal _user = new UserLocal();
            _user.Email = mainviewModel.User.Email;
            _user.Password = mainviewModel.User.Password;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {                
                await Application.Current.MainPage.DisplayAlert(
                         Languages.Error,
                         Languages.ErrorConnection,
                         Languages.Accept);
                return;
            }

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var token = await this.apiService.GetToken(
                apiSecurity,
                _user.Email,
                _user.Password
                );

            if (token == null)
            {                
                await Application.Current.MainPage.DisplayAlert(
                  Languages.Error,
                  Languages.ErrorToken,
                  Languages.Accept);
                return;
            }

            var response = await this.apiService.Delete(
                apiSecurity,
                "/api",
                "/UserSchedules",                
                this.AgendaId);

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.ErrorAppointment,
                    Languages.Accept);
                return;
            }
            else
            {
                return;
            }
        }
    }
}
