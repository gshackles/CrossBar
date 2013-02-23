using System;
using CrossBar.Platform.Messaging;
using MonoTouch.UIKit;
using CrossBar.UI.Touch.Extensions;

namespace CrossBar.UI.Touch
{
	public class ErrorReporter : IErrorReporter
	{
		public void ReportError (string message)
		{
			UIApplication.SharedApplication.InvokeOnMainThread(() =>
				UIApplication.SharedApplication.KeyWindow.ShowMessage (message));
		}
	}
}

