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

    public class DoctorViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        #endregion

        #region atributtes
        private ObservableCollection<DoctorItemViewModel> doctor;
        private bool isRefreshing;
        private string filter;
        #endregion
        #region Properties

        public ObservableCollection<DoctorItemViewModel> Doctors
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
                this.Search();
            }
        }

        #endregion

        #region Constructors
        public DoctorViewModel()
        {
            this.apiService = new ApiService();
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
                if(Application.Current.MainPage == null)
                {
                    return;
                }

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
                return;
            }

            MainViewModel.GetInstance().DoctorList = (List<Doctor>)response.Result;
            this.Doctors = new ObservableCollection<DoctorItemViewModel>(
            this.ToDoctorItemViewModel());
            this.IsRefreshing = false;
        }
        
        private IEnumerable<DoctorItemViewModel> ToDoctorItemViewModel()
        {
            return MainViewModel.GetInstance().DoctorList.Select(l => new DoctorItemViewModel
            {
                Id = l.Id,
                DocumentNumber = l.DocumentNumber,
                Claims = l.Claims,
                Email = l.Email,
                EmailConfirmed = l.EmailConfirmed,
                FirstName = l.FirstName,
                LastName = l.LastName,
                AccessFailedCount = l.AccessFailedCount,
                LockoutEnabled = l.LockoutEnabled,
                LockoutEndDateUtc = l.LockoutEndDateUtc,
                Logins = l.Logins,
                PasswordHash = l.PasswordHash,
                PhoneNumber = l.PhoneNumber,
                PhoneNumberConfirmed = l.PhoneNumberConfirmed,
                Roles = l.Roles,
                SecurityStamp = l.SecurityStamp,
                Speciality = l.Speciality,
                Telephone = l.Telephone,
                TwoFactorEnabled = l.TwoFactorEnabled,
                UserName = l.UserName,
        });
        }

        #endregion
        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadDoctors);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Doctors = new ObservableCollection<DoctorItemViewModel>(
                    this.ToDoctorItemViewModel());
            }
            else
            {
                this.Doctors = new ObservableCollection<DoctorItemViewModel>(
                    this.ToDoctorItemViewModel().Where(
                        l => l.FirstName.ToLower().Contains(this.Filter.ToLower()) ||
                             l.Speciality.ToLower().Contains(this.Filter.ToLower())));
            }
        }
        #endregion
    }
}
