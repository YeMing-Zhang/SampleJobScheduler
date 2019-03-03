using System;
using System.Collections.Specialized;
using System.Configuration;
using Autofac;
using Autofac.Extras.Quartz;
using log4net;

namespace SampleJobScheduler
{
    public class DependencyInjection
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<SchedulerService>().AsSelf().InstancePerLifetimeScope();                       

            // Register Quartz with Autofac
            builder.RegisterModule(new QuartzAutofacFactoryModule
            {
                ConfigurationProvider = context => (NameValueCollection)ConfigurationManager.GetSection("quartz")
            });

            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(SampleJob).Assembly));

            builder.Register(c => LogManager.GetLogger(typeof(Object))).As<ILog>();            

            return builder.Build();
        }
    }
}