using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Beeble.Api.Scheduler
{
    public static class SchedulerService
    {
        public static List<TimerScheduler> TimerSchedulers { get; set; } = new List<TimerScheduler>();
        public static void StartAction(long intervalInHours, Action action)
        {
            var intervalInMiliseconds = intervalInHours * 60 * 60000;

            TimerSchedulers.Add(new TimerScheduler(intervalInMiliseconds, action));
        }
    }
}