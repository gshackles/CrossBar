using System;
using CrossBar.Platform.Messaging;
using Xamarin.Controls;

namespace CrossBar.UI.Touch
{
	public class ErrorReporter : IErrorReporter
	{
		public void ReportError (string message)
		{
			AlertCenter.Default.PostMessage ("Error", message);
		}
	}
}

