using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;

namespace Beeble.Api.Scheduler
{
    public class TimerScheduler
    {
        private Timer Timer { get; set; }

        public TimerScheduler(long interval, Action action)
        {
            Timer = new Timer(interval);
            Timer.Elapsed += (t, e) => OnElapsed(action);
            Timer.Start();
        }

        private void OnElapsed(Action action)
        {
            Timer.Stop();
            try
            {
                action();
            }
            finally
            {
                Timer.Start();
            }
        }
    }
}