using System.Diagnostics;

namespace CoxExercise
{
    public interface ITimeTracker
    {
        public double TotalSeconds { get; }

        /// <summary>
        /// Starts the timer
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the timer
        /// </summary>
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
