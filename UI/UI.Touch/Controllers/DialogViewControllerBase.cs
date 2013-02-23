using System;
using CrossBar.Platform.ViewModels;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Dialog.Touch;
using Cirrious.MvvmCross.Views;
using CrossBar.UI.Touch.Extensions;

namespace CrossBar.UI.Touch.Controllers
{
	public abstract class DialogViewControllerBase<TViewModel> : MvxTouchDialogViewController<TViewModel>, IMvxServiceConsumer
		where TViewModel : ViewModelBase
	{
		protected DialogViewControllerBase(MvxShowViewModelRequest request) : base(request)
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