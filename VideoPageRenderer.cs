//
//  VideoPageRenderer.cs
//
//  Author:
//       Madhur Bhargava
//
using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using HSEMobile;
using HSEMobile.iOS;
using UIKit;
using System.Collections.Generic;
using System.Drawing;

using UIKit;
using AVFoundation;
using Foundation;
using CoreAnimation;
using CoreGraphics;
using CoreMedia;

[assembly: ExportRenderer (typeof(PlayerPage), typeof(VideoPageRenderer))]

namespace SomePrject.iOS
{
	public class VideoPageRenderer : PageRenderer
	{
		AVPlayer _player;
		AVPlayerLayer _playerLayer;
		AVAsset _asset;
		AVPlayerItem _playerItem;
		nfloat w;
		nfloat h;

		UIButton CloseButton;
		UIButton PlayPauseButton;
		UIButton RefreshButton;
		UILabel label1;
		nfloat M_PI = 3.14159f;


		private string FileNameToPlay = "video.mp4";

		public VideoPageRenderer ()
		{
			
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			UIViewController controller = this.ViewController;
			initControls ();
			doPlayerSetup ();
			rotateAndPlay (controller);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		private void initControls()
		{
			UIImage image = new UIImage ("close_white.png");
			var frame = new CGRect(View.Frame.Width - 40, View.Frame.Height - 40, 30, 30);
			CloseButton = new UIButton (UIButtonType.RoundedRect);
			CloseButton.Frame = frame;
			CloseButton.SetImage (image, UIControlState.Normal);
		


			CloseButton.TouchUpInside += delegate {
				_player.Rate = 0.0f;
				_player.Dispose();
				_player = null;
				MessagingCenter.Send ("", PlayerPage.message_dismiss);

			};

			image = new UIImage("pause_white.png");
			frame = new CGRect(15, View.Frame.Height - 40, 30, 30);
			PlayPauseButton = new UIButton (UIButtonType.RoundedRect);
			PlayPauseButton.Frame = frame;
			PlayPauseButton.SetImage (image, UIControlState.Normal);
			PlayPauseButton.TouchUpInside += delegate {
				if(_player.Rate == 1.0f)
				{
					_player.Pause();
					PlayPauseButton.SetImage (new UIImage("play_white.png"), UIControlState.Normal);
				}
				else
				{
					_player.Play();
					PlayPauseButton.SetImage (new UIImage("pause_white.png"), UIControlState.Normal);
				}
			};
			PlayPauseButton.Transform = CGAffineTransform.MakeRotation(M_PI / (nfloat)2.0);

			image = UIImage.FromBundle ("restart_white.png");
			frame = new CGRect(55, View.Frame.Height - 40, 30, 30);
			RefreshButton = new UIButton (UIButtonType.RoundedRect);
			RefreshButton.Frame = frame;
			RefreshButton.SetImage (image, UIControlState.Normal);
			RefreshButton.TouchUpInside += delegate {
				CMTime time = new CoreMedia.CMTime();
				CoreMedia.CMTime.FromSeconds(10, 600);

				time.Value = 0;
				_player.CurrentItem.Seek(CoreMedia.CMTime.FromSeconds(0.5, 600));
			};


			View.Add (CloseButton);
			View.Add (PlayPauseButton);
			View.Add (RefreshButton);
		}

		/// <summary>
		/// Rotates ContentView and rotate it to landscape and play the video.
		/// </summary>
		private void rotateAndPlay(UIViewController controller)
		{
			
			if(_player.Rate == 0.0)
			{
				_playerLayer = AVPlayerLayer.FromPlayer (_player);
				controller.View.BackgroundColor = UIKit.UIColor.Black;//new CoreGraphics.CGColor (0f, 0f, 0f);

				CGRect vFrame = new CGRect ();
				vFrame.Width = View.Frame.Height;
				vFrame.Height = View.Frame.Width;

				_playerLayer.Frame = vFrame;
				_playerLayer.VideoGravity = AVLayerVideoGravity.ResizeAspectFill;

				controller.View.Layer.AddSublayer (_playerLayer);
				controller.View.Layer.AddSublayer(CloseButton.Layer);
				controller.View.Layer.AddSublayer(PlayPauseButton.Layer);
				controller.View.Layer.AddSublayer(RefreshButton.Layer);




				_playerLayer.Transform = CATransform3D.MakeRotation (90.0f / 180.0f * M_PI, 0.0f, 0.0f, 1.0f);

				vFrame = _playerLayer.Frame;
				vFrame.X = 0;
				vFrame.Y = 0;

				_playerLayer.Frame = vFrame;

				_player.Play ();
			}
			else
			{
				CMTime time = new CoreMedia.CMTime();
				CoreMedia.CMTime.FromSeconds(10, 600);

				time.Value = 0;
				_player.CurrentItem.Seek(CoreMedia.CMTime.FromSeconds(0.5, 600));
			}
		}

		private void doPlayerSetup()
		{
			_asset = AVAsset.FromUrl (NSUrl.FromFilename (this.FileNameToPlay));
			_playerItem = new AVPlayerItem (_asset);
			_player = new AVPlayer (_playerItem);

		}




	}
}

