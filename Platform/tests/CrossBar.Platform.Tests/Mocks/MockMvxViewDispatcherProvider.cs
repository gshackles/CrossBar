using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.MvvmCross.Interfaces.Views;

namespace CrossBar.Platform.Tests.Mocks
{
    public class MockMvxViewDispatcherProvider : IMvxViewDispatcherProvider
    {
        public IMvxViewDispatcher Dispatcher { get; set; }
    }
}
