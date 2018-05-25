namespace HealthCenter.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;

    public class DatesViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private string namePatient;
        private string nameDoctor;
        private string idDoctor;
        private bool isRefreshing;
        private bool isEnabled;
        private Scheduler schedulerId;
        private ObservableCollection<Scheduler> scheduler;
        private ObservableCollection<Days> days;
        public int userId;
        #endregion

        #region Properties
        public string NamePatient
        {
            get { return this.namePatient; }
            set { SetValue(ref this.namePatient, value); }
        }

        public Scheduler SchedulerId
        {
            get { return this.schedulerId; }
            set { SetValue(ref this.schedulerId, value); }
        }

        public string NameDoctor
        {
            get { return this.nameDoctor; }
            set { SetValue(ref this.nameDoctor, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public string IdDoctor
        {
            get { return this.idDoctor; }
            set { SetValue(ref this.idDoctor, value); }
        }
        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public ObservableCollection<Scheduler> Schedulers
        {
            get { return this.scheduler; }
            set { SetValue(ref this.scheduler, value); }
        }

        public ObservableCollection<Days> Days
        {
            get { return this.days; }
            set { SetValue(ref this.days, value); }
        }


        #endregion
        #region Constructors
        public DatesViewModel(Doctor doctor)
        {
            this.apiService = new ApiService();
            this.LoadSchedulers();
            this.NameDoctor = doctor.FullName;
            this.IdDoctor = doctor.Id;
            this.NamePatient = MainViewModel.GetInstance().User.FullName;
            this.userId = MainViewModel.GetInstance().User.UserId;
        }

        #endregion

        #region Methods
        public async void LoadSchedulers()
        {
            this.IsRefreshing = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    connection.Message,
                    Languages.Accept);
                return;
            }

            var apiHealth = Application.Current.Resources["APISecurity"].ToString();
            var response = await this.apiService.GetList<Scheduler>(
                apiHealth,
                "/api",
                "/Schedulers",
                string.Format("/" + this.IdDoctor));

            MainViewModel.GetInstance().SchedulerList = (List<Scheduler>)response.Result;
            //this.Schedulers = new ObservableCollection<Scheduler>(MainViewModel.GetInstance().SchedulerList);
            this.Days = new ObservableCollection<Days>(this.ToDayItem());
            this.IsRefreshing = false;
            this.IsEnabled = true;

        }

        private IEnumerable<Days> ToDayItem()
        {
            return MainViewModel.GetInstance().SchedulerList.Select(a => new Days
            {
                id = a.idWorkDay,
                DateSchedule = a.DateSchedule.ToShortDateString(),
                startHour = a.startHour.ToShortTimeString(),
                NameDay = a.DateSchedule.ToString("dddd",
                        new CultureInfo("en-US"))
            });
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
            this.IsRefreshing = true;
            this.IsEnabled = false;

            if (SchedulerId == null || SchedulerId.AgendaId == 0)
            {
                this.IsRefreshing = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    Languages.DateValidator,
                    Languages.Accept);
                return;
            }

            var userScheduler = new UserSchedule();
            userScheduler.UserId = userId;
            userScheduler.AgendaId = SchedulerId.AgendaId;

            var apiSecurity = Application.Current.Resources["APISecurity"].ToString();
            var response = await this.apiService.Post(
                apiSecurity,
                "/api",
                "/UserSchedules",
                userScheduler);

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                this.IsEnabled = true;

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

                //it should redirect to MainPage after user take an appointment
                //Application.Current.MainPage = new MasterPage();
                this.LoadSchedulers();
            }
        }


        #endregion
    }
}
