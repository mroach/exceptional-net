using System.Web.Mvc;

namespace Exceptional.Web.Mvc
{
    public class ExceptionalExceptionFilter : IExceptionFilter
    {
        protected readonly Exceptional Exceptional;

        public ExceptionalExceptionFilter()
        {
            Exceptional = new Exceptional();
        }

        public ExceptionalExceptionFilter(string apiKey)
        {
            Exceptional = new Exceptional(apiKey);
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            Exceptional.Report(filterContext);
        }
    }
}
