using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ExifLib;
using System.IO;
using Xamarin;
using System.Reflection;
using Plugin.Media;
using PCLStorage;
using Plugin.Geolocator;
using System.Diagnostics;

namespace XamarinFormsMapsDemo
{
	public partial class ImagePage : ContentPage
	{
		double lat;
		double lang;

		public ImagePage ()
		{
			InitializeComponent ();
			MapButton.Clicked +=  OnClicked;
			PhotoButton.Clicked += OnPhotoClick;
			GalleryButton.Clicked += OnPhotoPickup;
		}

		/// <summary>
		/// Picks up the default bundled photo, extracts location and plots this on map.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
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

		/// <summary>
		/// This will launch photo gallery. Extracts location from photo and plots those on the map.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private async void OnPhotoPickup(object sender, EventArgs e)
		{
			if (!CrossMedia.Current.IsPickPhotoSupported) {
				//this.ShowUnsupportedMediaAlert ();
				return;
			}

			var ImageFromLibrary = await CrossMedia.Current.PickPhotoAsync ();

			if (ImageFromLibrary != null) {

				IFile ImageFile = await FileSystem.Current.GetFileFromPathAsync (ImageFromLibrary.Path);

				Stream stream = await ImageFile.OpenAsync (FileAccess.Read);

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

				await Navigation.PushModalAsync (new MapPage (lat, lang));
			}




		}

		/// <summary>
		/// Raises the photo click event. This will launch camera for photo click, extracts location and plots those on the map.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private async void OnPhotoClick(object sender, EventArgs e)
		{
			if (!CrossMedia.Current.IsCameraAvailable) {
				//this.ShowUnsupportedMediaAlert ();
				return;
			}

			var locator = CrossGeolocator.Current;
			locator.DesiredAccuracy = 50;

			var position = await locator.GetPositionAsync (timeoutMilliseconds: 10000);

			var ImageFilename = string.Format ("{0}.jpg", DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);

			var image = await CrossMedia.Current.TakePhotoAsync (new Plugin.Media.Abstractions.StoreCameraMediaOptions {
				Directory = "../Library",
				Name = ImageFilename
			});
			if (image != null) 
			{

				Stream stream = image.GetStream ();

				IFile ImageFile = await FileSystem.Current.GetFileFromPathAsync (image.Path);

				byte[] bytes;
				using (var ms = new MemoryStream())
				{
					stream.CopyTo(ms);
					bytes = ms.ToArray();
				}



				await Navigation.PushModalAsync (new MapPage (position.Latitude, position.Longitude));
			}
		}


	}
}

