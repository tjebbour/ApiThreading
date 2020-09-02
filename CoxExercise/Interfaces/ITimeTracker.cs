using System.Diagnostics;

namespace CoxExercise
{
    public interface ITimeTracker
    {
        public double TotalSeconds { get; }
        void Start();
        void Stop();
    }

    public class TimeTracker : ITimeTracker
    {

        private Stopwatch stopWatch;
        public TimeTracker()
        {
             stopWatch = new Stopwatch();
        }

        public double TotalSeconds { get { return stopWatch.Elapsed.TotalSeconds; } }

        public void Start()
        {
            stopWatch.Start();
        }

        public void Stop()
        {
            stopWatch.Stop();
        }
    }
}
