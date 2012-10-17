using System;
using System.Web.Mvc;
using Exceptional.Core;

namespace Exceptional.Web.Mvc
{
    public class Exceptional
    {
        protected readonly ExceptionalClient Client;

        public Exceptional(ExceptionalClient client)
        {
            Client = client;
        }

        public void Handle(ExceptionContext context, Action<string> debug = null)
        {
            var alert = CreateAlert(context);
            Client.Send(alert, debug);
        }

        public Alert CreateAlert(ExceptionContext context)
        {
            var alert = new Alert(context.Exception);
            alert.Environment = Mapper.CreateEnvironmentSummary(context);
            alert.Request = Mapper.CreateHttpRequestSummary(context);
            return alert;
        }
    }
}
