using Microsoft.Practices.Unity;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

/// <summary>
/// Unity controller activator.
/// </summary>
public class UnityControllerActivator : IHttpControllerActivator
{
    /// <summary>
    /// Creates an UnityControllerActivator instance.
    /// </summary>
    /// <param name="activator">Base activator.</param>
    public UnityControllerActivator(IHttpControllerActivator activator)
    {
        if (activator == null)
        {
            throw new ArgumentException("activator");
        }

        this.activator = activator;
    }

    /// <summary>
    /// Creates a controller wrapper.
    /// </summary>
    /// <param name="request">A http request.</param>
    /// <param name="controllerDescriptor">Controller descriptor.</param>
    /// <param name="controllerType">Controller type.</param>
    /// <returns>A controller wrapper.</returns>
    public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
    {
        return new Controller
        {
            activator = activator,
            controllerType = controllerType
        };
    }

    /// <summary>
    /// Base controller activator.
    /// </summary>
    private readonly IHttpControllerActivator activator;

    /// <summary>
    /// A controller wrapper.
    /// </summary>
    private class Controller : IHttpController, IDisposable
    {
        /// <summary>
        /// Base controller activator.
        /// </summary>
        public IHttpControllerActivator activator;

        /// <summary>
        /// Controller type.
        /// </summary>
        public Type controllerType;

        /// <summary>
        /// A controller instance.
        /// </summary>
        public IHttpController controller;

        /// <summary>
        /// Disposes controller.
        /// </summary>
        public void Dispose()
        {
            var disposable = controller as IDisposable;

            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        /// <summary>
        /// Executes an action.
        /// </summary>
        /// <param name="controllerContext">Controller context.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Response message.</returns>
        public Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            if (controller == null)
            {
                var request = controllerContext.Request;
                var container = request.GetDependencyScope().GetService(typeof(IUnityContainer)) as IUnityContainer;

                if (container != null)
                {
                    container.RegisterInstance<HttpControllerContext>(controllerContext);
                    container.RegisterInstance<HttpRequestMessage>(request);
                    container.RegisterInstance<CancellationToken>(cancellationToken);
                }

                controller = activator.Create(
                  request,
                  controllerContext.ControllerDescriptor,
                  controllerType);
            }

            controllerContext.Controller = controller;

            return controller.ExecuteAsync(controllerContext, cancellationToken);
        }
    }
}