using HealthCenter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthCenter.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DatesPage : ContentPage
	{
		public DatesPage ()
		{
			InitializeComponent();
		}

        void getDate_DateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
        {
            var date = e.NewDate;
            var mainViewModel =  MainViewModel.GetInstance().Dates;
            mainViewModel.Filter = date.ToShortDateString();
        }
    }
}