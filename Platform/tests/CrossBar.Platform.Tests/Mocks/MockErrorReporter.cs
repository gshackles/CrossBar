using System;
using System.Collections.Generic;
using System.Linq;
using CrossBar.Platform.Messaging;

namespace CrossBar.Platform.Tests.Mocks
{
    public class MockErrorReporter : IErrorReporter
    {
        public IList<string> ErrorMessages { get; private set; }

        public MockErrorReporter()
        {
            ErrorMessages = new List<string>();
        }

        public void ReportError(string message)
        {
            ErrorMessages.Add(message);
        }
    }
}
