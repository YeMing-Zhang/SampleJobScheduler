using Quartz;

namespace SampleJobScheduler
{
    public class SchedulerService: ISchedulerService
    {
        private readonly IScheduler _scheduler;

        public SchedulerService(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public void Start()
        {
            _scheduler.Start();
        }
        
        public void Shutdown()
        {
            _scheduler.Shutdown();
        }
    }
}