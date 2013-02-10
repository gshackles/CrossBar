using System;
using System.Collections.Generic;
using System.Linq;

namespace CrossBar.Platform.Messaging
{
    public interface IErrorReporter
    {
        void ReportError(string message);
    }
}
