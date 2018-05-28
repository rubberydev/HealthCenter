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
        private ObservableCollection<DatesItemViewModel> scheduler;
        public int userId;
        #endregion

        #region Properties
        public string NamePatient
        {
            get { return this.namePatient; }
            set { SetValue(ref this.namePatient, value); }
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

        public ObservableCollection<DatesItemViewModel> Schedulers
        {
            get { return this.scheduler; }
            set { SetValue(ref this.scheduler, value); }
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

        public ICommand RefreshCommand
        {

            get
            {
                return new RelayCommand(LoadSchedulers);
            }
        }

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
            this.Schedulers = new ObservableCollection<DatesItemViewModel>(this.ToScheduleItemViewModel());
            this.IsRefreshing = false;
            this.IsEnabled = true;

        }

        private IEnumerable<DatesItemViewModel> ToScheduleItemViewModel()
        {
            return MainViewModel.GetInstance().SchedulerList.Select(a => new DatesItemViewModel
            {
                AgendaId = a.AgendaId,
                DateSchedule_ = a.DateSchedule.ToShortDateString(),
                startHour_ = a.startHour.ToShortTimeString(),
                NameDay = a.DateSchedule.ToString("dddd",
                        new CultureInfo("en-US"))
            });
        }
        #endregion
    }
}
