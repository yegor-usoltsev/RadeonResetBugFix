namespace RadeonResetBugFixService.Tasks.ComplexTasks
{
    using BasicTasks;
    using Contracts;
    using System;

    class StartupTask : AbstractSequentialTask
    {
        public StartupTask(ServiceContext context)
        {
            this.Context = context;
        }

        private ServiceContext Context { get; }

        public override string TaskName => "Startup";

        protected override ITask[] Subtasks => new ITask[]
        {
            new EnableBasicDisplayStartupTask(),
            new SleepTask(TimeSpan.FromSeconds(8)),
            new EnableAmdVideoTask(this.Context.StartupDevicesStatus),
            new DisableVirtualVideoTask(this.Context.StartupDevicesStatus),
            new SleepTask(TimeSpan.FromSeconds(4)),
            new FixMonitorTask(),
            new DisableVirtualVideoTask(this.Context.StartupDevicesStatus),
            new FixMonitorTask()
        };
    }
}
