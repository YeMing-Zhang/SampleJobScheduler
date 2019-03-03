using System;
using System.Collections.Specialized;
using System.Configuration;
using Autofac;
using Autofac.Extras.Quartz;
using log4net;

namespace SampleJobScheduler
{
    /// <summary>
    /// Bootstraps the Dependency Injection using Autofac
    /// </summary>
    public class DependencyInjection
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();

            // Registers SchedulerService with Autofac
            builder.RegisterType<SchedulerService>().AsSelf().InstancePerLifetimeScope();                       

            // Register Quartz with Autofac and reads quartz section in App.config. 
            builder.RegisterModule(new QuartzAutofacFactoryModule
            {
                ConfigurationProvider = context => (NameValueCollection)ConfigurationManager.GetSection("quartz")
            });

            // This line registers all Jobs in the current executing assembly
            builder.RegisterModule(new QuartzAutofacJobsModule(typeof(SampleJob).Assembly));

            // As per recent update GetLogger might have some performance issues. 
            builder.Register(c => LogManager.GetLogger(typeof(Object))).As<ILog>();            

            return builder.Build();
        }
    }
}