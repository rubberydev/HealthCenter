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

    public class MyDatesViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region atributtes
        private ObservableCollection<MyDatesItemViewModel> myDates;
        private bool isRefreshing;
        private string filter;
        #endregion

        #region Properties

        public ObservableCollection<MyDatesItemViewModel> MyDates_
        {
            get { return this.myDates; }
            set { SetValue(ref this.myDates, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);
                //this.Search();
            }
        }
        #endregion

        #region Constructors
        public MyDatesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadMyDates();
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadMyDates);
            }
        } 
        #endregion

        #region Methods
        public async void LoadMyDates()
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
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var mainviewModel = MainViewModel.GetInstance();
            var userScheduler = new UserSchedule();
            userScheduler.UserId = mainviewModel.User.UserId;
            

            var apiHealth = Application.Current.Resources["APISecurity"].ToString();
            var response = await this.apiService.GetList<Appointments>(
                apiHealth,
                "/api",
                "/UserSchedules",
                string.Format("/" + userScheduler.UserId));

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            MainViewModel.GetInstance().AppointmentList = (List<Appointments>)response.Result;
            this.MyDates_ = new ObservableCollection<MyDatesItemViewModel>(this.ToAppointmentItemViewModel());
            this.IsRefreshing = false;
        }

        private IEnumerable<MyDatesItemViewModel> ToAppointmentItemViewModel()
        {
            return MainViewModel.GetInstance().AppointmentList.Select(a => new MyDatesItemViewModel
            {
                AgendaId = a.AgendaId,
                DateSchedule_ = a.DateSchedule.ToShortDateString(),
                StartHour_ = a.StartHour.ToShortTimeString(),
                NameDay = a.DateSchedule.ToString("dddd",
                        new CultureInfo("en-US"))
            });
        }
        #endregion
    }
}
