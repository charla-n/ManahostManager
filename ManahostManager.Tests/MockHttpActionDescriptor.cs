using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace ManahostManager.Tests
{
    public class MockHttpActionDescriptor : HttpActionDescriptor
    {
        public override Task<object> ExecuteAsync(HttpControllerContext controllerContext, IDictionary<string, object> arguments, System.Threading.CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override System.Collections.ObjectModel.Collection<HttpParameterDescriptor> GetParameters()
        {
            throw new NotImplementedException();
        }

        public override Type ReturnType
        {
            get { throw new NotImplementedException(); }
        }

        public override string ActionName
        {
            get { throw new NotImplementedException(); }
        }
    }
}