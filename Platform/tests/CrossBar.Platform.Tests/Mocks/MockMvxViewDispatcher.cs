using System;
using System.Collections.Generic;
using Cirrious.MvvmCross.Interfaces.ViewModels;
using Cirrious.MvvmCross.Interfaces.Views;
using Cirrious.MvvmCross.Views;

namespace CrossBar.Platform.Tests.Mocks
{
    public class MockMvxViewDispatcher : IMvxViewDispatcher
    {
        public IList<IMvxViewModel> CloseRequests = new List<IMvxViewModel>();
        public IList<MvxShowViewModelRequest> NavigateRequests = new List<MvxShowViewModelRequest>();

        public bool RequestNavigate(MvxShowViewModelRequest request)
        {
            NavigateRequests.Add(request);
            return true;
        }

        public bool RequestClose(IMvxViewModel whichViewModel)
        {
            CloseRequests.Add(whichViewModel);
            return true;
        }

        public bool RequestRemoveBackStep()
        {
            throw new NotImplementedException();
        }

        public bool RequestMainThreadAction(Action action)
        {
            action();
            return true;
        }
    }
}