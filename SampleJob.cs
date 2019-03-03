using System.Threading.Tasks;
using log4net;
using Quartz;

namespace SampleJobScheduler
{
    public class SampleJob : IJob
    {
        private readonly ILog _log;

        public SampleJob(ILog log)
        {
            _log = log;
        }
        
        async Task IJob.Execute(IJobExecutionContext context)
        {
            _log.Info("Execute Job is working");

            await Task.FromResult(0);
        }
    }
}
