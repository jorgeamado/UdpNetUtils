// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace IPhone_utility
{
	[Register ("IPhone_utilityViewController")]
	partial class IPhone_utilityViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel LocalIPLabel { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField PackageCount { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextView ReceivedTextView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField ServerIPTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField ServerPortTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIView ServerView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton StartButton { get; set; }

		[Action ("OnClearDataClick:")]
		partial void OnClearDataClick (MonoTouch.Foundation.NSObject sender);

		[Action ("OnSendPackages:")]
		partial void OnSendPackages (MonoTouch.Foundation.NSObject sender);

		[Action ("OnStartButtonClick:")]
		partial void OnStartButtonClick (MonoTouch.Foundation.NSObject sender);

		[Action ("OnUpdateLatecyClick:")]
		partial void OnUpdateLatecyClick (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (LocalIPLabel != null) {
				LocalIPLabel.Dispose ();
				LocalIPLabel = null;
			}

			if (PackageCount != null) {
				PackageCount.Dispose ();
				PackageCount = null;
			}

			if (ReceivedTextView != null) {
				ReceivedTextView.Dispose ();
				ReceivedTextView = null;
			}

			if (ServerIPTextField != null) {
				ServerIPTextField.Dispose ();
				ServerIPTextField = null;
			}

			if (ServerPortTextField != null) {
				ServerPortTextField.Dispose ();
				ServerPortTextField = null;
			}

			if (ServerView != null) {
				ServerView.Dispose ();
				ServerView = null;
			}

			if (StartButton != null) {
				StartButton.Dispose ();
				StartButton = null;
			}
		}
	}
}
