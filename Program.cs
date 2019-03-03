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

            HostFactory.Run(configurator => 
            {
                // Set windows service properties
                configurator.SetServiceName("SampleSchedulerService");
                configurator.SetDisplayName("Sample Service");
                configurator.SetDescription("Executes Job.");
                                
                configurator.RunAsLocalSystem();

                configurator.UseLog4Net();
                configurator.UseAutofacContainer(container);
                configurator.Service<SchedulerService>(hostConfigurator => 
                {
                    hostConfigurator.ConstructUsing(hostSettings => container.Resolve<SchedulerService>());                    
                    hostConfigurator.WhenStarted(s => s.Start());
                    hostConfigurator.WhenStopped(s => s.Shutdown());
                });
            });

            Console.ReadLine();
        }
    }
}
