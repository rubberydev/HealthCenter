

namespace HealthCenter.ViewModels
{
    using Helpers;
    using Models;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;

    public class DatesViewModel: BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private string namePatient;
        private string nameDoctor;
        private string documentNumber;
        private bool isRefreshing;
        private bool isEnabled;
        private ObservableCollection<Scheduler> scheduler;
        private ObservableCollection<WorkDayList> workday;
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

        public string DocumentNumber
        {
            get { return this.documentNumber; }
            set { SetValue(ref this.documentNumber, value); }
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

        public ObservableCollection<WorkDayList> WorkDay
        {
            get { return this.workday; }
            set { SetValue(ref this.workday, value); }
        }
        #endregion
        #region Constructors
        public DatesViewModel(Doctor doctor)
        {
            this.apiService = new ApiService();
            this.LoadSchedulers();
            this.NameDoctor = doctor.FullName;
            this.DocumentNumber = doctor.DocumentNumber;
            this.NamePatient = MainViewModel.GetInstance().User.FullName;
            
        }

        #endregion

        #region Methods
        public async void LoadSchedulers()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
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
                "/Schedulers");

            var apiHealthWorkDays = Application.Current.Resources["APISecurity"].ToString();
            var responseWorkDays = await this.apiService.GetList<WorkDayList>(
                apiHealthWorkDays,
                "/api",
                "/WorkDays");

            if (!response.IsSuccess || !responseWorkDays.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            MainViewModel.GetInstance().SchedulerList = (List<Scheduler>)response.Result;
            MainViewModel.GetInstance().WorkDayList = (List<WorkDayList>)responseWorkDays.Result;
            this.Schedulers = new ObservableCollection<Scheduler>(MainViewModel.GetInstance().SchedulerList);
           // this.Schedulers = new ObservableCollection<Scheduler>(MainViewModel.GetInstance().SchedulerList.Where(l => l.ApplicationUser_Id.Contains(this.DocumentNumber)));
            this.WorkDay = new ObservableCollection<WorkDayList>(MainViewModel.GetInstance().WorkDayList);
            this.IsRefreshing = false;

        }

        //private IEnumerable<WorkDay> ToWorkday()
        //{
        //    return MainViewModel.GetInstance().WorkDayList.Select(l => new WorkDay
        //    {
        //        startDayHour = l.startDayHour,
        //        endDayHour = l.endDayHour,
        //        DateToday = l.DateToday,
        //        durationCite = l.durationCite,

        //    });
        //}
        #endregion
    }
}
