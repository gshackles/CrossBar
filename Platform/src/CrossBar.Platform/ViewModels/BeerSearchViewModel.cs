using CrossBar.Platform.ViewModels.Parameters;
using TinyMessenger;

namespace CrossBar.Platform.ViewModels
{
    public class BeerSearchViewModel : ViewModelBase<EmptyParameters>
    {
        public BeerSearchViewModel(ITinyMessengerHub messengerHub) 
            : base(messengerHub)
        {
        }

        protected internal override void Initialize(EmptyParameters parameters)
        {
        }
    }
}