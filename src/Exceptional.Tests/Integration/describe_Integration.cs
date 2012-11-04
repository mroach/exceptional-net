using System;
using System.Collections.Generic;
using System.Configuration;
using Exceptional.Core;
using NSpec;

namespace Exceptional.Tests.Integration
{
    public class describe_Integration : nspec
    {
        public string ApiKey()
        {
            var key = ConfigurationManager.AppSettings["Exceptional:ApiKey"];

            if (string.IsNullOrEmpty(key) || key.Length != 40)
                throw new Exception("Invalid key. API key should be 40 characters long.");

            return key;
        }

        public void when_connecting_to_exceptional()
        {
            var client = new ExceptionalClient(ApiKey());
            var request = new HttpRequestSummary
                              {
                                  Action = "Test",
                                  Url = "http://localhost/test",
                                  RequestMethod = "GET",
                                  Controller = "TestController",
                                  RemoteIp = "192.168.255.1",
                                  Headers = new Dictionary<string, string>
                                                {
                                                    {"Version", "HTTP/1.1"},
                                                    {"User-Agent", "Test"}
                                                },
                                  Parameters = new Dictionary<string, object>
                                                {
                                                    {"Action", "Test"},
                                                    {"Controller", "TestController"}
                                                },
                                  Session = new Dictionary<string, object>
                                                {
                                                    {"UserID", 123}
                                                }
                              };
            var alert = new Alert(CreateException());
            alert.Request = request;
            it["should report"] = () => client.Send(alert);
        }

        protected Exception CreateException()
        {
            try
            {
                throw new ExceptionalValidationException("Test exception");
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
