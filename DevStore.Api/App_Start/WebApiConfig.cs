using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace DevStore.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Serviços e configuração da API da Web
            //Seta o objeto formatters
            var formatters = GlobalConfiguration.Configuration.Formatters;
            //Pega os ojetos do JSON
            var jsonFormatter = formatters.JsonFormatter;
            //Pega as configurações do formato JSON
            var settings = jsonFormatter.SerializerSettings;

            //Para não resolver a refencia (em casos de propriedades complexas, propriedade q recebe um determinado objeto) e traba o objeto completo, ou seja, não "Resumido"
            jsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            //Remove a configuração de xml
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            settings.Formatting = Formatting.Indented;
            //Deixa o retorno com a caixa baixa (LowerCase)
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //Habilita o CORS - 
            config.EnableCors();

            // Rotas da API da Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
