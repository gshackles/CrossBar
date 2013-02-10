using System;
using System.Collections.Generic;
using System.Linq;
using TinyMessenger;

namespace CrossBar.Platform.Messaging.Messages
{
    public class ErrorMessage : TinyMessageBase
    {
        public string Message { get; private set; }

        public ErrorMessage(object sender, string message)
            : base(sender)
        {
            Message = message;
        }
    }
}
