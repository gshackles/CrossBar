using System;
using System.Net;
using System.Threading.Tasks;
using Amarillo.Network;
using CrossBar.Platform.Messaging.Messages;
using TinyMessenger;

namespace CrossBar.Platform.Services
{
    public abstract class ServiceBase
    {
        private readonly ITinyMessengerHub _messengerHub;
        private const string DefaultErrorMessage = "There was an error processing your request, please try again later.";

        protected ServiceBase(ITinyMessengerHub messengerHub)
        {
            _messengerHub = messengerHub;
        }

        protected Task<TPayload> MakeClientCall<TPayload>(Func<Task<Response<TPayload>>> clientCall)
            where TPayload : class
        {
            return clientCall
                .Invoke()
                .ContinueWith(response =>
                                  {
                                      if (response.Exception != null)
                                      {
                                          publishError(DefaultErrorMessage);

                                          return null;
                                      }

                                      if (response.Result.Status != HttpStatusCode.OK)
                                      {
                                          publishError(DefaultErrorMessage);

                                          return null;
                                      }

                                      return response.Result.Payload;
                                  });
        }

        private void publishError(string message)
        {
            _messengerHub.Publish(new ErrorMessage(this, message));
        }
    }
}