namespace HealthCenter.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class MyDatesViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region atributtes
        private ObservableCollection<Scheduler> myDatesL;
        private bool isRefreshing;
        private string filter;
        #endregion

        #region Properties

        public ObservableCollection<Scheduler> MyDatesL
        {
            get { return this.myDatesL; }
            set { SetValue(ref this.myDatesL, value); }
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

            var apiHealth = Application.Current.Resources["APISecurity"].ToString();
            var response = await this.apiService.GetList<Scheduler>(
                apiHealth,
                "/api",
                "/Schedulers");

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                return;
            }

            MainViewModel.GetInstance().SchedulerList = (List<Scheduler>)response.Result;
            this.MyDatesL = new ObservableCollection<Scheduler>(MainViewModel.GetInstance().SchedulerList);
            this.IsRefreshing = false;
        }
        #endregion
    }
}
