using CrossBar.Platform.ViewModels.Parameters;
using TinyMessenger;

namespace CrossBar.Platform.ViewModels
{
    public class BeerViewModel : ViewModelBase<BeerParameters>
    {
        public BeerViewModel(ITinyMessengerHub messengerHub) 
            : base(messengerHub)
        {
        }

        protected internal override void Initialize(BeerParameters parameters)
        {
            
        }
    }
}