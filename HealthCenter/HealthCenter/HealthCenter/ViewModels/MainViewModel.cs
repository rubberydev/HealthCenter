namespace HealthCenter.ViewModels
{
    using Helpers;
    using Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    public class MainViewModel : BaseViewModel
    {
        private UserLocal user;

        #region Properties
        public List<Doctor> DoctorList
        {
            get;
            set;
        }

        public List<Scheduler> SchedulerList
        {
            get;
            set;
        }

        public List<Appointments> AppointmentList
        {
            get;
            set;
        }


        #endregion
        #region ViewModels
        public LoginViewModel Login
        {
            get;
            set;
        }

        public RegisterViewModel Register
        {
            get;
            set;
        }

        public ChangePasswordViewModel ChangePassword
        {
            get;
            set;
        }

        public MyProfileViewModel MyProfile
        {
            get;
            set;
        }

        public MyDatesViewModel MyDates
        {
            get;
            set;
        }

        public DoctorViewModel Doctor
        {
            get;
            set;
        }

        public DatesViewModel Dates
        {
            get;
            set;
        }

        public TokenResponse Token
        {
            get;
            set;
        }

        public ObservableCollection<MenuItemViewModel> Menus
        {
            get;
            set;
        }

        public UserLocal User
        {
            get { return this.user; }
            set { SetValue(ref this.user, value); }
        }

        public MenuItemViewModel Menu
        {
            get;
            set;
        }


        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            this.LoadMenu();
        }
        #endregion

        #region Syngelton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
        #endregion

        #region Methods
        private void LoadMenu()
        {
            this.Menus = new ObservableCollection<MenuItemViewModel>();
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_MyDates",
                PageName = "MyDatesPage",
                Title = Languages.MyDates,
            });

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_settings",
                PageName = "MyProfilePage",
                Title = Languages.MyProfile,
            });

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_password",
                PageName = "ChangePasswordPage",
                Title = Languages.ChangePassword,
            });

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "ic_lock_open",
                PageName = "LoginPage",
                Title = Languages.LogOut,
            });


        }
        #endregion


    }
}
