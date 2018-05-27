
namespace HealthCenter.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Services;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class DatesItemViewModel: Scheduler
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Constructors
        public DatesItemViewModel()
        {
            this.apiService = new ApiService();
        }
        #endregion

        #region Commands

        public ICommand DateCommand
        {

            get
            {
                return new RelayCommand(ScheduleAppointment);
            }
        }

        private async void ScheduleAppointment()
        {
            var mainviewModel = MainViewModel.GetInstance();

            var resc = await Application.Current.MainPage.DisplayAlert(
                   Languages.ConfirmLabel,
                   Languages.ConfirmDate,
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

            var userScheduler = new UserSchedule();
            userScheduler.UserId = mainviewModel.User.UserId;
            userScheduler.AgendaId = this.AgendaId;

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var response = await this.apiService.Post(
                apiSecurity,
                "/api",
                "/UserSchedules",
                userScheduler);

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
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ConfirmLabel,
                    Languages.AppointmentSuccefully,
                    Languages.Accept);

                mainviewModel.Doctor = new DoctorViewModel();
                mainviewModel.Menu = new MenuItemViewModel();
                await App.Navigator.PopAsync();
            }
        }
        #endregion
    }
}
