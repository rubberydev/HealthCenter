

namespace HealthCenter.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Models;
    using Helpers;

    public class DatesViewModel: BaseViewModel
    {
        #region Attributes
        private string namePatient;
        private string nameDoctor;
        private string documentNumber;
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

        public string DocumentNumber
        {
            get { return this.documentNumber; }
            set { SetValue(ref this.documentNumber, value); }
        }
        #endregion
        #region Constructors
        public DatesViewModel(Doctor doctor)
        {
            this.NameDoctor = doctor.FullName;
            this.DocumentNumber = doctor.DocumentNumber;
            this.NamePatient = MainViewModel.GetInstance().User.FullName;
        }
        #endregion

        #region Methods

        #endregion
    }
}
