using System;
using System.Web.Mvc;
using Exceptional.Core;

namespace Exceptional.Web.Mvc
{
    public class Exceptional
    {
        protected readonly ExceptionalClient Client;

        public Exceptional()
            : this(new ExceptionalClient())
        {
            
        }

        public Exceptional(string apiKey)
            : this(new ExceptionalClient(apiKey))
        {
        }

        public Exceptional(ExceptionalClient client)
        {
            Client = client;
        }

        public void Report(ExceptionContext context, Action<string> debug = null)
        {
            var alert = Mapper.CreateAlert(context);
            Client.Send(alert, debug);
        }
    }
}
