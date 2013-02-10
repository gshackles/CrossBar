using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.ViewModels;
using TinyMessenger;

namespace CrossBar.Platform.ViewModels
{
    public abstract class ViewModelBase : MvxViewModel
    {
        protected ViewModelBase(ITinyMessengerHub messengerHub)
        {
            IsLoading = RequiresLoading;
            MessengerHub = messengerHub;
        }

        protected ITinyMessengerHub MessengerHub { get; private set; }

        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            private set { _isLoading = value; RaisePropertyChanged(() => IsLoading); }
        }

        protected virtual bool RequiresLoading { get { return true; } }

        public event EventHandler<EventArgs> LoadingComplete;

        protected void FinishedLoading()
        {
            IsLoading = false;

            if (LoadingComplete != null)
                LoadingComplete.Invoke(this, EventArgs.Empty);
        }
    }
}
