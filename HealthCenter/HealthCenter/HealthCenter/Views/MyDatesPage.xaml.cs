using Syncfusion.SfGauge.XForms;
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
	public partial class MyDatesPage : ContentPage
	{
		public MyDatesPage ()
		{
			InitializeComponent ();
            
            SfDigitalGauge digital = new SfDigitalGauge();
            digital.Value = "1 2 3 4";
            digital.CharacterHeight = 25;
            digital.CharacterWidth = 10;
            digital.SegmentStrokeWidth = 3;
            digital.CharacterType = CharacterType.SegmentSeven;
            digital.DisabledSegmentAlpha = 30;
            digital.BackgroundColor = Color.FromRgb(235, 235, 235);
            digital.CharacterStrokeColor = Color.FromRgb(20, 108, 237);
            digital.DisabledSegmentColor = Color.FromRgb(20, 108, 237);
        }
	}
}