using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ExifLib;
using System.IO;
using Xamarin;
using System.Reflection;


namespace XamarinFormsMapsDemo
{
	public partial class ImagePage : ContentPage
	{
		double lat;
		double lang;
		public ImagePage ()
		{
			InitializeComponent ();

			//ExifInterface exif = new ExifInterface(filepath);
			//ImageSource sample = SampleImage.Source;

			//byte[] data =  File.ReadAll("image.png");
			//System.IO.Stream image = new StreamImageSource("sample.jpg");
			//ExifReader.ReadJpeg(stream);
			MapButton.Clicked +=  OnClicked;
		}

		private void OnClicked(object sender, EventArgs e)
		{
			
			var assembly = typeof(ImagePage).GetTypeInfo().Assembly;
			Stream stream = assembly.GetManifestResourceStream("XamarinFormsMapsDemo.sample.jpg");
			var JpegInfo = ExifReader.ReadJpeg (stream);


			lat = JpegInfo.GpsLatitude[0] + JpegInfo.GpsLatitude[1] / 60 + JpegInfo.GpsLatitude[2] / 3600;//JpegInfo.GpsLatitude[0];
			lang =  JpegInfo.GpsLongitude[0] + JpegInfo.GpsLongitude[1] / 60 + JpegInfo.GpsLongitude[2] / 3600;//JpegInfo.GpsLongitude[0];
			ExifGpsLatitudeRef latRef = JpegInfo.GpsLatitudeRef;
			ExifGpsLongitudeRef longRef = JpegInfo.GpsLongitudeRef;
			if (latRef == ExifGpsLatitudeRef.South) {
				lat = lat * -1;
			}

			if (longRef == ExifGpsLongitudeRef.West) {
				lang = lang * -1;
			}

			Navigation.PushModalAsync (new MapPage (lat, lang));
		}


	}
}

