using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.ExtensionMethods;
using CrossBar.Platform.ViewModels.Parameters;
using Cirrious.MvvmCross.Plugins.Json;
using TinyMessenger;

namespace CrossBar.Platform.ViewModels
{
    public abstract class ViewModelBase<TParameters> : ViewModelBase where TParameters : ParametersBase
    {
        protected ViewModelBase(ITinyMessengerHub messengerHub)
            : base(messengerHub)
        {
        }

        protected bool Navigate<TViewModel, TViewModelParameters>(TViewModelParameters parameters)
            where TViewModel : ViewModelBase<TViewModelParameters>
            where TViewModelParameters : ParametersBase
        {
            var converter = this.GetService<IMvxJsonConverter>();
            var json = converter.SerializeObject(parameters);

            return RequestNavigate<TViewModel>(new Dictionary<string, object> { { ParametersBase.Key, json } });
        }

        protected internal virtual void Initialize(TParameters parameters) { }
    }
}
