namespace HealthCenter.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Models;
    using Views;

    public class DoctorItemViewModel : Doctor
    {
        #region Commands
        public ICommand SelectDoctorCommand
        {
            get
            {
                return new RelayCommand(SelectDoctor);
            }
        }

        private async void SelectDoctor()
        {
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.Dates = new DatesViewModel(this);
            await App.Navigator.PushAsync(new DatesPage());

        }
        #endregion
    }
}
