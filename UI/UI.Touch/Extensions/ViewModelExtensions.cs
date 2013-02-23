using System;
using CrossBar.Platform.ViewModels;
using MonoTouch.UIKit;
using System.Linq.Expressions;
using System.Reflection;
using MBProgressHUD;

namespace CrossBar.UI.Touch.Extensions
{
	public static class ViewModelExtensions
	{
		public static void BindLoadingMessage<TViewModel>(this TViewModel viewModel, UIView view, Expression<Func<TViewModel, bool>> property, string message)
			where TViewModel : ViewModelBase
		{
			var expression = property.Body as MemberExpression;
			
			if (expression == null) return;
			
			var propertyInfo = expression.Member as PropertyInfo;
			
			if (propertyInfo == null) return;
			
			var hud = new MTMBProgressHUD (view) 
			{
				LabelText = message,
				//RemoveFromSuperViewOnHide = true,
				DimBackground = true,
				AnimationType = MBProgressHUDAnimation.MBProgressHUDAnimationZoomIn,
				Mode = MBProgressHUDMode.Indeterminate,
				MinShowTime = 0
			};
			view.AddSubview (hud);
			
			Action<bool> setMessageVisibility = visible =>
			{
				if (visible)
				{
					hud.Show(animated: true);
				}
				else
				{
					hud.Hide(animated: true);
				}
			};
			
			setMessageVisibility((bool)propertyInfo.GetValue(viewModel, null));
			
			viewModel.PropertyChanged += (sender, e) => 
			{
				if (e.PropertyName == propertyInfo.Name)
					setMessageVisibility((bool)propertyInfo.GetValue(viewModel, null));
			};
		}
	}
}

