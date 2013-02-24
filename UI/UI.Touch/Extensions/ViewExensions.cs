using System;
using MonoTouch.UIKit;
using CrossBar.Platform.ViewModels;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using MBProgressHUD;
using System.Threading;
using System.Windows.Input;

namespace CrossBar.UI.Touch.Extensions
{
	public static class ViewExensions
	{
		public static void ShowMessage(this UIView parentView, string message) 
		{
			var hud = new MTMBProgressHUD (parentView) 
			{
				DetailsLabelText = message,
				RemoveFromSuperViewOnHide = true,
				DimBackground = false,
				AnimationType = MBProgressHUDAnimation.MBProgressHUDAnimationZoomIn,
				Mode = MBProgressHUDMode.Text,
				UserInteractionEnabled = true
			};
			parentView.AddSubview (hud);
			
			hud.Show (true);
			hud.Hide (true, 1.5);
		}

		public static void SetReturnCommand(this UITextField textView, ICommand command)
		{
			textView.ShouldReturn += _ => 
			{
				textView.ResignFirstResponder();
				command.Execute(null);
				return true;
			};
		}
	}
}