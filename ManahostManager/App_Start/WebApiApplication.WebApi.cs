using ManahostManager.Unity;
using ManahostManager.Utils;
using ManahostManager.Utils.Formatters;
using Owin;
using Swashbuckle.Application;
using System.Web.Http;
using System.Web.Http.Batch;
using System.Web.Http.ExceptionHandling;

namespace ManahostManager.App_Start
{
    public partial class WebApiApplication
    {
        private void SetupWebApi(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            HttpServer server = new HttpServerResolver(config);

            config.EnableCors();
            config.MapHttpAttributeRoutes();

            config.Formatters.Clear();
            config.Formatters.Add(new JilFormatter());
            config.Formatters.Add(new MsgPackFormatter());
            //config.Formatters.Add(new XmlMediaTypeFormatter());

            config.Services.Replace(typeof(IExceptionHandler), new ExceptionManahostHandler());
            config.Services.Replace(typeof(IExceptionLogger), new ExceptionManahostLogger());

            config.Filters.Add(new ExceptionValidationHandler());

            #region Unity

            config.DependencyResolver = new UnityResolver(WebApiApplication.container);

            #endregion Unity

            config.Routes.MapHttpBatchRoute(
                routeName: "WebApiBatch",
                routeTemplate: "api/batch",
                batchHandler: new DefaultHttpBatchHandler(server));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("Alpha", "Manahost is Swag").Description(
                    "<b>Permission</b><br/><br/>" +
                    "REGISTERED Users are able to GET, export data and delete account, nothing more.<br/>" +
                    "VIP Users are able to do<br/>" +
                    "anything(GET, POST, PUT, DELETE).<br/>" +
                    "DISABLED Users can't log in.<br/>" +
                    "ADMIN is only for administrating the website(beta keys, discount keys, prices, templates, ...).<br/><br/>" +
                    "<b>JSON or MsgPack ?</b><br/><br/>" +
                    "The API by default sends you back JSON but you can specify in Accept and Content - Type JSON or MsgPack.<br/>" +
                    "Note that you should always send an Accept Request for each Request<br/>" +
                    "Type for JSON is : application / json<br/>" +
                    "Type for MsgPack is : application / x - msgpack<br/><br/>" +
                    "<b>Internal Server Errors and Performance issues</b><br/><br/>" +
                    "You've an access on the log of the API (C:\\APILog\\ with the RemoteDesktop), please go see if you think something goes wrong and create a Jira Task or talk on Slack !<br/><br/>" +
                    "<b>Models (DTOs)</b><br/><br/>" +
                    "Take a look on stash in the API repository inside ManahostManager.Domain.DTOs.*. If you have any questions, please ask on confluence or Slack.<br/><br/>" +
                    "<b>Localization</b><br/><br/>" +
                    "On the subscription the client needs to send the default locale of the system(without asking to the manager, it should be done in the background) according to the list of available cultures(see https://confluence.manahost.fr/display/MAP/2015/01/28/Cultures).<br/>" +
                    "The default locale is set by the API at the French(fr - FR) locale.<br/><br/>" +
                    "<b>Batch Request</b><br/><br/>" +
                    "Batch Request feature is enabled on the API side. Please follow : http://blogs.msdn.com/b/webdev/archive/2013/11/01/introducing-batch-support-in-web-api-and-web-api-odata.aspx<br/><br/>" +
                    "<b>Request Limitation</b><br/><br/>" +
                    "The API limits the number of requests per IP.<br/>" +
                    "By default an unauthenticated user can make 600 Requests per hour.<br/>" +
                    "Once authenticated you have a limit of 4000 Requests per hour<br/>" +
                    "If the quota is reached you'll receive a 429 http code (Too Many Requests).<br/><br/>" +
                    "<b>Timezones</b><br/><br/>" +
                    "See https://confluence.manahost.fr/display/MAP/2015/01/28/Timezone<br/><br/>"
                    )
                    ;
                c.IncludeXmlComments(System.String.Format(@"{0}\bin\ManahostManager.xml", System.AppDomain.CurrentDomain.BaseDirectory));
            }).EnableSwaggerUi();

            app.UseWebApi(server);
        }
    }
}