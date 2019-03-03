using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;
using Topshelf.Autofac;
using Autofac;

namespace SampleJobScheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            IContainer container = DependencyInjection.Build();


            HostFactory.Run(hostConfigurator => 
            {
                // Set windows service properties
                hostConfigurator.SetServiceName("SampleSchedulerService");
                hostConfigurator.SetDisplayName("Sample Service");
                hostConfigurator.SetDescription("Executes Job.");

                hostConfigurator.RunAsLocalSystem();
                // Configure Log4Net with Topself
                hostConfigurator.UseLog4Net();
                hostConfigurator.UseAutofacContainer(container);
                hostConfigurator.Service<SchedulerService>(serviceConfigurator => 
                {
                    serviceConfigurator.ConstructUsing(hostSettings => container.Resolve<SchedulerService>());
                    serviceConfigurator.WhenStarted(s => s.Start());
                    serviceConfigurator.WhenStopped(s => s.Shutdown());
                });
            });            
        }
    }
}
