using System;
using System.Windows.Input;
using Cirrious.MvvmCross.Views;
using CrossBar.Platform.ViewModels;
using CrossUI.Touch.Dialog.Elements;
using MonoTouch.UIKit;
using CrossBar.UI.Touch.Controllers;
using CrossBar.UI.Touch.Extensions;

namespace CrossBar.UI.Touch.Views
{
	public class MainMenuView : DialogViewControllerBase<MainMenuViewModel>
	{
		public MainMenuView(MvxShowViewModelRequest request)
			: base(request)
		{
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();

			Func<string, ICommand, Element> createMenuItem =
				(text, command) => 
					new StyledStringElement(text) { 
						Accessory = UITableViewCellAccessory.DisclosureIndicator, 
						SelectedCommand = command };

			Root = new RootElement("CrossBar")
			{
				new Section("Search For")
				{
					createMenuItem("Beers", ViewModel.FindBeersCommand),
					createMenuItem("Breweries", ViewModel.FindBreweriesCommand)
				},
				new Section("Your Favorites")
				{
					createMenuItem("Beers", ViewModel.ViewFavoriteBeersCommand),
					createMenuItem("Breweries", ViewModel.ViewFavoriteBreweriesCommand)
				}
			};
		}
	}
}

