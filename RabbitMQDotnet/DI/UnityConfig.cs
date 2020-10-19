using Microsoft.Practices.Unity;
using RabbitMQApp.RabbitMQHelper;

namespace RabbitMQApp.DI
{
    public static class UnityConfig
    {
        public static UnityContainer GetMainContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IRabbitMQHelper, RabbitMqHelper>();

            return container;
        }
    }
}