using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.MvvmCross.Plugins.WebBrowser;

namespace CrossBar.Platform.Tests.Mocks
{
    public class MockMvxWebBrowserTask : IMvxWebBrowserTask
    {
        public IList<string> UrlRequests = new List<string>();

        public void ShowWebPage(string url)
        {
            UrlRequests.Add(url);
        }
    }
}
