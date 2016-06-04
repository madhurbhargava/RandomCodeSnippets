//
//  SomePageRenderer.cs
//
//  Author:
//       Madhur Bhargava <mbhargava@slb.com>
//
using System;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using TTM;
using TTM.iOS;
using UIKit;
using CoreGraphics;

[assembly: ExportRenderer (typeof (SomePage), typeof (SomePageRenderer))]

namespace None
{
	/// <summary>
	/// Handles native rendering of a page.
	/// </summary>
	public class SomePageRenderer : PageRenderer
	{
		public SomePageRenderer ()
		{

			 
		}

		private bool updatedButtons = false;
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear (animated);

			if (!updatedButtons) {
				this.SqueezeBarButtonSpace ();
			}

			if(((SomePage)Element).BtnProjectActivities != null)
			{
				(((SomePage)Element).SomeBtn).Clicked += HandleTouchUpInside;
			}

				

			MessagingCenter.Subscribe<string> (this, Strings.FLIP_ANIMATE, (text) => {
				doFlip();
			});
		}


		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			MessagingCenter.Unsubscribe<string> (this, Strings.FLIP_ANIMATE);
		}

		void HandleTouchUpInside (object sender, EventArgs ea) {
			doFlip();	
		}

		private void doFlip()
		{
			//  added flag in app model to control flip for Results view button 
			//  solves issue where project details button edges above and below call the flip animation.
			if (SomeCondition) {
				UIView.BeginAnimations (null);
				UIView.SetAnimationDuration (0.4);
				UIView.SetAnimationTransition (UIViewAnimationTransition.FlipFromRight, NavigationController.View.Subviews [0], true);//NavigationController.View,true);
				UIView.CommitAnimations ();
			}	
		}

		/// <summary>
		/// Squeezes the bar button space on the navigation bar for the tile details page.
		/// Also adds the backbutton item to navigation bar.
		/// </summary>
		protected void SqueezeBarButtonSpace()
		{
			
			if (NavigationController.TopViewController.NavigationItem.RightBarButtonItems.Length > 2) 
			{
				if(SomeOtherCondition)
				{
					UIBarButtonItem[] arr = NavigationController.TopViewController.NavigationItem.RightBarButtonItems;
					UIBarButtonItem[] newArr = {arr[0], arr[2]};
					NavigationController.TopViewController.NavigationItem.RightBarButtonItems = newArr;
					UIBarButtonItem btnItem  = NavigationController.TopViewController.NavigationItem.RightBarButtonItems[0];
					btnItem.ImageInsets = new UIEdgeInsets(0, -30, 0, 10);
				}
				else
				{
					UIBarButtonItem btnItem  = NavigationController.TopViewController.NavigationItem.
						RightBarButtonItems[0];
					btnItem.ImageInsets = new UIEdgeInsets(0, -30, 0, 10);
					btnItem  = NavigationController.TopViewController.NavigationItem.RightBarButtonItems[1];
					btnItem  = NavigationController.TopViewController.NavigationItem.RightBarButtonItems[2];
					btnItem.ImageInsets = new UIEdgeInsets(0, 10, 0, -30);
				}

				this.updatedButtons = true;
			}

			//if the back button isnt hidden, we change the image related to it
			if (!NavigationController.TopViewController.NavigationItem.HidesBackButton) {
				NavigationController.TopViewController.NavigationItem.SetLeftBarButtonItem (new UIBarButtonItem(
					UIImage.FromFile("Back_White.png"), UIBarButtonItemStyle.Plain, (sender, args) => {
						MessagingCenter.Send<string>("", Strings.BACK_MESSAGE);
					}), true);
				
			}
		}

		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null || Element == null) {
				return;
			}
		}

	}
}

