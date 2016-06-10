// DurationPicker.cs
//
//  Author:
//       Madhur Bhargava
//
using System;
using Xamarin.Forms;

namespace CustomPickerView
{
	public class DurationPicker : Label
	{
		public Action ShowAction;

		public void Show(){
			if(ShowAction != null)
				ShowAction ();
		}

		public DurationPicker() {
			var gesture = new TapGestureRecognizer();
			gesture.Tapped += (sender, e) => Show ();
			GestureRecognizers.Add (gesture);
		}

		// Time - Send minuts to this property
		public static readonly BindableProperty TimeProperty =
			BindableProperty.Create<DurationPicker, double> (p => p.Time, 1);

		public double Time {
			get { return (double)GetValue (TimeProperty); }
			set { SetValue (TimeProperty, value); }
		}

	}
}

