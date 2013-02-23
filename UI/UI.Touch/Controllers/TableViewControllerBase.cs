using System;
using CrossBar.Platform.ViewModels;
using Cirrious.MvvmCross.Interfaces.ServiceProvider;
using Cirrious.MvvmCross.Dialog.Touch;
using Cirrious.MvvmCross.Views;
using Cirrious.MvvmCross.Binding.Touch.Views;
using CrossBar.UI.Touch.Extensions;

namespace CrossBar.UI.Touch.Controllers
{
	public abstract class TableViewControllerBase<TViewModel> : MvxBindingTouchTableViewController<TViewModel>, IMvxServiceConsumer
		where TViewModel : ViewModelBase
	{
		protected TableViewControllerBase(MvxShowViewModelRequest request) : base(request)
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