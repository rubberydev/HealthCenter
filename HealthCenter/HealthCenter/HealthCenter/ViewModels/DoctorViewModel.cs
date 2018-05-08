namespace HealthCenter.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models;
    using Helpers;
    using Services;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;
    using System.Linq;

    public class DoctorViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region atributtes
        private ObservableCollection<Doctor> doctor;
        private bool isRefreshing;
        private string filter;
        private string nombre;
        #endregion
        #region Properties


        public ObservableCollection<Doctor> Doctors
        {
            get { return this.doctor; }
            set { SetValue(ref this.doctor, value); }
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

        public string Nombre
        {
            get { return this.nombre; }
            set
            {
                SetValue(ref this.nombre, value);
                //this.Search();
            }
        }
        #endregion

        #region Constructors
        public DoctorViewModel()
        {
            this.apiService = new ApiService();
            this.Nombre = "Doctor Ejemplo";
            this.LoadDoctors();
        }
        #endregion

        #region Methods
        public async void LoadDoctors()
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
            var response = await this.apiService.GetList<Doctor>(
                apiHealth,
                "/api",
                "/Users");

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);
                //await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            MainViewModel.GetInstance().DoctorList = (List<Doctor>)response.Result;
            var list = (List<Doctor>)response.Result;
            this.Doctors = new ObservableCollection<Doctor>(list);
            //this.Doctor = new ObservableCollection<DoctorItemViewModel>(
                //this.ToDoctorItemViewModel());
            this.IsRefreshing = false;
        }
        #endregion

        #region Methods
        //private IEnumerable<DoctorItemViewModel> ToDoctorItemViewModel()
        //{
        //    return MainViewModel.GetInstance().DoctorList.Select(l => new DoctorItemViewModel
        //    {
        //        DocumentNumber = "";
                
        //    });
        //}

        #endregion
    }
}
