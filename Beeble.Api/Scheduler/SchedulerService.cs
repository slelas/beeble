using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Beeble.Api.Scheduler
{
    public static class SchedulerService
    {
        public static List<TimerScheduler> TimerSchedulers { get; set; } = new List<TimerScheduler>();
        public static void StartAction(long interval, Action action)
        {
            TimerSchedulers.Add(new TimerScheduler(interval, action));
        }
    }
}