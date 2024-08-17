namespace Timers {

    /// <summary>
    /// Timer class.
    /// </summary>
    public class Timer {

        private float _duration;
        private float _completeTime;
        private float _pausedTime;
        private bool _started = false;
        private bool _paused = false;

        /// <summary>
        /// Create a new timer with the given <paramref name="duration" />.
        /// </summary>
        /// <param name="duration">Duration for the timer.</param>
        public Timer (float duration) {
            _duration = duration;
        }

        /// <summary>
        /// Has the timer run for it's duration at the <paramref name="currentTime" />.
        /// </summary>
        /// <param name="currentTime">Time to check the timer at.</param>
        /// <returns>True if the timer is complete at the <paramref name="currentTime" /> and false otherwise.</returns>
        public bool IsComplete (float currentTime) {
            return _started && !_paused && currentTime >= _completeTime;
        }

        /// <summary>
        /// Is the timer currently running.
        /// </summary>
        /// <returns>True if the timer is started and not paused.</returns>
        public bool IsRunning () {
            return _started && !_paused;
        }

        /// <summary>
        /// Is the timer started.
        /// </summary>
        /// <returns>True if the timer is started.</returns>
        public bool IsStarted () {
            return _started;
        }

        /// <summary>
        /// Is the timer paused.
        /// </summary>
        /// <returns>True if the timer is paused.</returns>
        public bool IsPaused () {
            return _paused;
        }

        /// <summary>
        /// Start the timer at the <paramref name="currentTime" />.
        /// </summary>
        /// <param name="currentTime">Time to start the timer at.</param>
        public void Start (float currentTime) {
            if (_started) return;

            _started = true;
            _completeTime = currentTime + _duration;
        }

        /// <summary>
        /// Stop the timer.
        /// </summary>
        public void Stop () {
            _started = false;
            _paused = false;
        }

        /// <summary>
        /// Restart the timer at the <paramref name="currentTime" />.
        /// </summary>
        /// <param name="currentTime">Time to restart the timer at.</param>
        public void Restart (float currentTime) {
            Stop();
            Start(currentTime);
        }

        /// <summary>
        /// Pause the timer at the <paramref name="currentTime" />
        /// </summary>
        /// <param name="currentTime">Time to pause the timer at.</param>
        public void Pause (float currentTime) {
            if (_paused) return;

            _paused = true;
            _pausedTime = currentTime;
        }

        /// <summary>
        /// Unpause the timer at the <paramref name="currentTime" />.
        /// </summary>
        /// <param name="currentTime">Time to unpause the timer at.</param>
        public void Unpause (float currentTime) {
            if (!_paused) return;

            _paused = false;
            float durationPaused = currentTime - _pausedTime;
            _completeTime += durationPaused;
        }

        /// <summary>
        /// Change the duration of the timer to <paramref name="newDuration" />.
        /// </summary>
        /// <param name="newDuration">New duration for the timer.</param>
        public void ChangeDuration (float newDuration) {
            if (_started) _completeTime += newDuration - _duration;
            _duration = newDuration;
        }

        /// <summary>
        /// Change the duration of the timer to <paramref name="newDuration" /> but keep the current percentage complete.
        /// </summary>
        /// <param name="currentTime">Time to change the duration at.</param>
        /// <param name="newDuration">New duration for the timer.</param>
        public void ChangeDurationKeepPercentage (float currentTime, float newDuration) {
            if (_started) {
                _completeTime = currentTime + (1 - PercentComplete(currentTime)) * newDuration;
            }
            _duration = newDuration;
        }

        /// <summary>
        /// Get the percentage complete for the timer at the <paramref name="currentTime" /> as a float between 0 and 1.
        /// </summary>
        /// <param name="currentTime">The time to get the percentage complete at.</param>
        /// <returns>A float between 0 and 1 representing the percentage complete </returns>
        public float PercentComplete (float currentTime) {
            if (!_started) return 0f;
            if (_paused) return (_duration - (_completeTime - _pausedTime)) / _duration;

            return (_duration - (_completeTime - currentTime)) / _duration;
        }
    }
}