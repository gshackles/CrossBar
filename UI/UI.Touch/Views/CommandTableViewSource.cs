using System;
using System.Drawing;
using Cirrious.MvvmCross.Views;
using CrossBar.Platform.ViewModels;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using CrossBar.UI.Touch.Controllers;
using CrossBar.UI.Touch.Extensions;
using Cirrious.MvvmCross.Binding.Touch.ExtensionMethods;
using System.Collections.Generic;
using Cirrious.MvvmCross.Binding.Touch.Views;
using System.Linq;
using System.Windows.Input;

namespace CrossBar.UI.Views
{
	public class CommandTableViewSource : MvxSimpleBindableTableViewSource
	{
		public CommandTableViewSource(UITableView tableView, string cellIdentifier, string bindingText, ICommand command)
			: base(tableView, UITableViewCellStyle.Default, new NSString(cellIdentifier), bindingText, UITableViewCellAccessory.DisclosureIndicator)
		{
			SelectionChanged += (sender, e) =>
			{
				command.Execute(e.AddedItems[0]);
				tableView.DeselectRow(tableView.IndexPathForSelectedRow, true);
			};
		}
	}
	
}
