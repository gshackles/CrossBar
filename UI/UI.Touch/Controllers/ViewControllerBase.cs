using System;
using CrossBar.Platform.ViewModels;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Dialog.Touch;
using Cirrious.MvvmCross.Views;
using Cirrious.MvvmCross.Binding.Touch.Views;
using MonoTouch.Foundation;
using CrossBar.UI.Touch.Extensions;

namespace CrossBar.UI.Touch.Controllers
{
	public abstract class ViewControllerBase<TViewModel> : MvxBindingTouchViewController<TViewModel>, IMvxServiceConsumer
		where TViewModel : ViewModelBase
	{
		protected ViewControllerBase(MvxShowViewModelRequest request, string nibName, NSBundle bundle) 
			: base(request, nibName, bundle)
		{
		}
		
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear (animated);
			
			this.OnViewWillAppear(ViewModel, OnLoadingComplete);
		}
		
		protected virtual void OnLoadingComplete() { }
	}
}
