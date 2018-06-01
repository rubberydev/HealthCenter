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
	public partial class GaugeWatchPage : ContentPage
	{
		public GaugeWatchPage ()
		{
			InitializeComponent ();

            //Padding = 15;

            //LabelAnnotation1 = new Label { Text = "", FontSize = 14, HeightRequest = 25, WidthRequest = 75, TextColor = Color.Black, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };

            //LabelAnnotation1 = new Label { Text = "", FontSize = 12, HeightRequest = 20, WidthRequest = 35, TextColor = Color.Black, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };

            //LabelAnnotation1 = new Label { Text = "", FontSize = 12, HeightRequest = 20, WidthRequest = 35, TextColor = Color.Black, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center };

            //Annotation1 = new SyncForms.SfCircularGauge
            //{
            //    HeightRequest = Device.OnPlatform(70, 80, 100),
            //    WidthRequest = Device.OnPlatform(70, 80, 100),
            //    Annotations = new SyncForms.CircularGaugeAnnotationCollection
            //    {
            //        new SyncForms.GaugeAnnotation{View = LabelAnnotation2, Angle = 270, Offset
            //         = .5}
            //    },
            //    Scales = new ObservableCollection<SyncForms.Scale>
            //    {
            //        new SyncForms.Scale
            //        {
            //            StartAngle = 270,
            //            SweepAngle = 360,
            //            ShowLabels = false,
            //            StartValue = 0,
            //            EndValue = 60,
            //            Interval = 5,
            //            RimColor = Color.FromRgb(237, 238, 239),
            //            Ranges = new ObservableCollection<Syncfusion.SfGauge.XForms.Range>
            //            {
            //                new Syncfusion.SfGauge.XForms.Range{StartValue=0, EndValue = 30, Color = Color.Gray, InnerStartOffset = 0.925, OuterStartOffset = 1, InnerEndOffset = 0.925, OuterEndOffset = 1},

            //            },
            //            MajorTickSettings = new SyncForms.TickSettings{Color = Color.Black, StartOffset = 1, EndOffset = .85, Thickness = 2},
            //            MinorTickSettings = new SyncForms.TickSettings{Color = Color.Black, StartOffset = 1, EndOffset = .90, Thickness = 0.5},
            //            Pointers =  new ObservableCollection<SyncForms.Pointer>
            //            {
            //                new SyncForms.NeedlePointer
            //                {
            //                    Type = SyncForms.PointerType.Triangle, KnobRadius = 4, Thickness = 3, EnableAnimation = false, KnobColor = Color.Black, Color = Color.Black
            //                }
            //            }
            //        }

            //    }
            //};
            //if (Device.Idiom == TargetIdiom.Tablet)
            //{
            //    Annotation2.HeightRequest = 170;
            //    Annotation2.WidthRequest = 170;
            //}

            //Annotation2.Parent = this;

            //Gauge = new SyncForms.SfCircularGauge
            //{

            //    Annotations = new SyncForms.CircularGaugeAnnotationCollection
            //    {
            //        new SyncForms.GaugeAnnotation{ View = Annotation1, Angle = 90, Offset = Device.OnPlatform(.5,.5,.6)},
            //        new SyncForms.GaugeAnnotation{ View = LabelAnnotation1, Angle = 00, Offset = .5},
            //        new SyncForms.GaugeAnnotation{ View = Annotation2, Angle = 180, Offset = Device.OnPlatform(.5,.5,.6)},
            //    },
            //    Scales = new ObservableCollection<SyncForms.Scale>
            //    {
            //        new SyncForms.Scale
            //        {
            //            StartValue = 0,
            //            EndValue = 12,
            //            Interval = 1,
            //            MinorTicksPerInterval = 4,
            //            RimColor = Color.FromRgb(237, 238, 239),
            //            LabelColor = Color.Gray,
            //            LabelOffset = Device.OnPlatform(.8, .8, .875),
            //            ScaleEndOffset = .925,
            //            StartAngle = 270,
            //            SweepAngle = 360,
            //            LabelFontSize = 14,
            //            ShowFirstLabel = false,
            //            MinorTickSettings = new SyncForms.TickSettings{Color = Color.Black, StartOffset = 1, EndOffset = .95, Thickness = 1},
            //            MajorTickSettings= new SyncForms.TickSettings{Color = Color.Black, StartOffset = 1, EndOffset = .9, Thickness = 3},
            //            Ranges = new ObservableCollection<SyncForms.Range>
            //            {
            //                new SyncForms.Range {StartValue = 0, EndValue = 3, Color = Color.Gray, InnerStartOffset =0.925, OuterStartOffset =1, InnerEndOffset = 0.925, OuterEndOffset = 1}
            //            },
            //            Pointers = new ObservableCollection<SyncForms.Pointer>
            //            {
            //                new SyncForms.NeedlePointer {EnableAnimation = false, KnobRadius = 6, LengthFactor = 0.75, KnobColor = Color.White, Color = Color.Black, Thickness = 3.5, KnobStrokeColor = Color.Black, KnobStrokeWidth = 5, TailLengthFactor = 0.25, TailColor = Color.Black},
            //                new SyncForms.NeedlePointer {EnableAnimation = false, KnobRadius = 6, LengthFactor = 0.4, KnobColor = Color.White, Color = Color.Black, Thickness = 5, Type = SyncForms.PointerType.Triangle},
            //                new SyncForms.NeedlePointer {EnableAnimation = false, KnobRadius = 6, LengthFactor = 0.65, KnobColor = Color.White, Color = Color.Black, Thickness = 5, Type = SyncForms.PointerType.Triangle},
            //            }
            //        }
            //    }
            //};

            //DynamicUpdate();

            //Content = Gauge;
        }

        //private async void DynamicUpdate()
        //{
        //    while (true)
        //    {
        //        var dataTime = DateTime.Now;
        //        var hour = (double)dataTime.Hour;
        //        hour = hour > 12 ? hour % 12 : hour;

        //        var min = (double)dataTime.Minute;
        //        var sec = (double)dataTime.Second;

        //        Gauge.Scales[0].Pointers[0].Value = sec / 5;

        //        Gauge.Scales[0].Pointers[1].Value = hour + min / 60;

        //        Gauge.Scales[0].Pointers[2].Value = min / 5 + (sec / 60 * .2);

        //        Annotation1.Scales[0].Pointers[0].Value = sec;

        //        Annotation2.Scales[0].Pointers[0].Value = min + sec / 60;

        //        var meridem = dataTime.Hour > 12 ? "PM" : "AM";
        //        LabelAnnotation1.Text = hour.ToString() + ":" + min.ToString() + "" + meridem;

        //        LabelAnnotation2.Text = sec.ToString() + "S";

        //        LabelAnnotation3.Text = min.ToString() + "M";

        //        await Task.Delay(1000);

        //    }
        //}
    }
}