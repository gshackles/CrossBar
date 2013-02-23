using System;
using MonoTouch.UIKit;
using CrossBar.Platform.ViewModels;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using MBProgressHUD;
using System.Threading;

namespace CrossBar.UI.Touch.Extensions
{
	public static class ViewControllerExtensions
	{
		public static void ShowMessage(this UIViewController controller, string message) 
		{
			controller.View.ShowMessage (message);
		}

		public static void OnViewWillAppear<TController> (this TController controller, ViewModelBase viewModel, Action loadedCallback)
			where TController : UIViewController, IMvxServiceConsumer
		{
			if (!viewModel.IsLoading) 
			{
				controller.InvokeOnMainThread (() => loadedCallback ());
			} 
			else 
			{
				viewModel.BindLoadingMessage (controller.View, model => model.IsLoading, "Loading...");
				viewModel.LoadingComplete += (s, e) => controller.InvokeOnMainThread (() => loadedCallback ());
			}
		}
	}
}

