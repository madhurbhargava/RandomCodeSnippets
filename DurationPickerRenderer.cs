// DurationPickerRenderer.cs
//
//  Author:
//       Madhur Bhargava
//

using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using CustomPickerView;
using CustomPickerView.Droid;
using Android.Widget;
using Android.App;

[assembly: ExportRenderer(typeof(DurationPicker), typeof(DurationPickerRenderer))]
namespace CustomPickerView.Droid
{
	public class DurationPickerRenderer : LabelRenderer
	{

		DurationPicker picker;
//		double TimeInMins;

		NumberPicker HoursPicker;
		NumberPicker MinsPicker;

		AlertDialog.Builder alert;

		protected override void OnElementChanged (ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged (e);

			if(e.NewElement == null) return;

//			var element = this.Element as DurationPicker;

			if (Element != null) {
				picker = (DurationPicker)Element;
//				TimeInMins = element.Time;
				picker.ShowAction = new Action (Show);
			}
		}

		void Show() { 

			LinearLayout layout = new LinearLayout (Context);
			layout.Orientation = Orientation.Horizontal;

			LinearLayout.LayoutParams param = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, 250);
			layout.WeightSum = 1f;
			layout.LayoutParameters = param;
			layout.SetGravity (Android.Views.GravityFlags.Center);

			LinearLayout.LayoutParams HoursPickerParam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent, 0.5f);

			HoursPicker = new NumberPicker (Context);
			HoursPicker.MaxValue = 24;
			HoursPicker.MinValue = 1;
			HoursPicker.LayoutParameters = HoursPickerParam;

			TextView HoursLbl = new TextView (Context);
			HoursLbl.Text = "Hours";

			LinearLayout.LayoutParams MinsPickerParam = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent, 0.5f);

			MinsPicker = new NumberPicker (Context);
			MinsPicker.LayoutParameters = MinsPickerParam;
			MinsPicker.MaxValue = 60;
			MinsPicker.MinValue = 1;

			TextView MinLbl = new TextView (Context);
			MinLbl.Text = "Mins";

			layout.AddView (HoursPicker);
			layout.AddView (HoursLbl);
			layout.AddView (MinsPicker);
			layout.AddView (MinLbl);

			alert = new AlertDialog.Builder (Context);
			alert.SetView (layout);

			alert.SetPositiveButton("OK", delegate { Finish(); });
			alert.SetNegativeButton("Cancel", delegate { Finish(); });

			alert.Show ();
		}

		public void Finish() {
			alert.Dispose ();

			var str = HoursPicker.Value;
			var str2 = MinsPicker.Value;
		}

		private String[] GetHours () {
			
			String[] hoursArray = new String[24];

			for (int i = 0; i < hoursArray.Length; i++) {
				hoursArray[i] = i.ToString();
			}

			return hoursArray;
		}

		private String[] GetMins () {

			String[] hoursMins = new String[60];

			for (int i = 0; i < hoursMins.Length; i++) {
				hoursMins[i] = i.ToString();
			}

			return hoursMins;
		}

	}
}

