using System;
using System.Collections.Generic;
using Godot;

namespace Timers {

    /// <summary>
    /// Timekeeper to check the times on timers. Persists between scenes.
    /// </summary>
    public partial class Timekeeper : Node {

        private struct TimerInfo {
            public bool Loop;
            public bool UseUnscaledTime;
            public Action OnFinish;
        }

        /// <summary>
        /// Pairs timers with associated info about them.
        /// </summary>
        private static readonly Dictionary<Timer, TimerInfo> _timerInfoByTimer = new();

        /// <summary>
        /// Are timers currently being checked? (Cannot remove timers while this is happening)
        /// </summary>
        private static bool checkingTime = false;

        /// <summary>
        /// Timers to remove when no longer checking time.
        /// </summary>
        private static readonly List<Timer> timersToRemove = new();

        /// <summary>
        /// Timers to add when no longer checking time.
        /// </summary>
        private static readonly Dictionary<Timer, TimerInfo> timersToAdd = new();

        /// <summary>
        /// Static reference to the single timekeeper instance.
        /// </summary>
        private static Timekeeper instance = null;

        /// <summary>
        /// Add a timer to the timekeeper.
        /// </summary>
        /// <param name="duration">Duration for the timer.</param>
        /// <param name="onFinish">Action to perform when the timer finishes.</param>
        /// <param name="loop">Should the timer loop.</param>
        /// <param name="useUnscaledTime">Should the timer use scaled or unscaled time.</param>
        /// <returns>The timer.</returns>
        public static Timer AddTimer (float duration, Action onFinish, bool loop = false, bool useUnscaledTime = false) {
            if (instance == null) {
                Initialize();
            }

            Timer timer = new(duration);

            if (!checkingTime) {
                _timerInfoByTimer.Add(timer, new TimerInfo() { Loop = loop, UseUnscaledTime = useUnscaledTime, OnFinish = onFinish });
            } else {
                timersToAdd.Add(timer, new TimerInfo() { Loop = loop, UseUnscaledTime = useUnscaledTime, OnFinish = onFinish });
            }
            return timer;
        }

        /// <summary>
        /// Start the <paramref name="timer" />.
        /// </summary>
        /// <param name="timer">Timer to start.</param>
        public static void StartTimer (Timer timer) {
            timer.Start(GetTimeType(timer));
        }

        /// <summary>
        /// Stop the <paramref name="timer" />. If this is called during the OnFinish action of a timer where Loop == true, it will prevent the timer from restarting.
        /// </summary>
        /// <param name="timer">Timer to stop.</param>
        public static void StopTimer (Timer timer) {
            timer.Stop();
        }

        /// <summary>
        /// Restart the <paramref name="timer" />.
        /// </summary>
        /// <param name="timer">Timer to restart.</param>
        public static void RestartTimer (Timer timer) {
            timer.Restart(GetTimeType(timer));
        }

        /// <summary>
        /// Pause the <paramref name="timer" />.
        /// </summary>
        /// <param name="timer">Timer to pause.</param>
        public static void PauseTimer (Timer timer) {
            timer.Pause(GetTimeType(timer));
        }

        /// <summary>
        /// Unpause the <paramref name="timer" />.
        /// </summary>
        /// <param name="timer">Timer to unpause.</param>
        public static void UnpauseTimer (Timer timer) {
            timer.Unpause(GetTimeType(timer));
        }

        /// <summary>
        /// Change the duration of the timer to <paramref name="newDuration" />.
        /// </summary>
        /// 
        /// <param name="newDuration">New duration for the timer.</param>
        public static void ChangeDuration (Timer timer, float newDuration) {
            timer.ChangeDuration(newDuration);
        }

        /// <summary>
        /// Change the duration of the timer to <paramref name="newDuration" /> but keep the current percentage complete.
        /// </summary>
        /// <param name="timer">Timer to change the duration for.</param>
        /// <param name="newDuration">New duration for the timer.</param>
        public static void ChangeDurationKeepPercentage (Timer timer, float newDuration) {
            timer.ChangeDurationKeepPercentage(GetTimeType(timer), newDuration);
        }

        /// <summary>
        /// Get the percentage complete for the timer. <paramref name="timer" />.
        /// </summary>
        /// <param name="timer">Timer to get the percent complete for.</param>
        /// <returns>The percentage complete for the timer as a float between 0 and 1 inclusive.</returns>
        public static float GetPercentComplete (Timer timer) {
            return timer.PercentComplete(GetTimeType(timer));
        }

        /// <summary>
        /// Get either the time or the unscaled time based on the <paramref name="timer" />'s TimerInfo.
        /// </summary>
        /// <param name="timer">The timer to get the time for.</param>
        /// <returns>Either the time or unscaled time based on <paramref name="timer" />.</returns>
        private static float GetTimeType (Timer timer) {
            return (float)(Time.GetTicksMsec() / 1000f / ((_timerInfoByTimer.ContainsKey(timer) ? _timerInfoByTimer[timer] : timersToAdd[timer]).UseUnscaledTime ? Engine.TimeScale : 1f));
        }

        /// <summary>
        /// Remove a timer from the timekeeper. If this is called during an OnFinish action, the timer will be queued to be removed once all time checks have completed.
        /// </summary>
        /// <param name="timerToRemove">Timer to remove from the timekeeper.</param>
        public static void RemoveTimer (Timer timerToRemove) {
            if (!checkingTime) {
                _timerInfoByTimer.Remove(timerToRemove);
            } else {
                timersToRemove.Add(timerToRemove);
            }
        }

        /// <summary>
        /// Create an instance of the timekeeper.
        /// </summary>
        public static void Initialize () {
            if (instance != null) return;
            PackedScene timekeeperScene = GD.Load<PackedScene>("res://scenes/Timers/Timekeeper.tscn");

            instance = timekeeperScene.Instantiate<Timekeeper>();
            ((SceneTree)Engine.GetMainLoop()).Root.CallDeferred(MethodName.AddChild, instance);
        }

        /// <summary>
        /// Remove timers that were queued to be removed.
        /// </summary>
        private static void RemoveQueuedTimers () {
            foreach (Timer t in timersToRemove) RemoveTimer(t);

            timersToRemove.Clear();
        }

        /// <summary>
        /// Add timers that were queued to be added.
        /// </summary>
        private static void AddQueuedTimers () {
            foreach (KeyValuePair<Timer, TimerInfo> t in timersToAdd) _timerInfoByTimer.Add(t.Key, t.Value);

            timersToAdd.Clear();
        }

        /// <summary>
        /// Called each frame. Checks the time on all the timers. Timers are checked in the order they have been added.
        /// </summary>
        public override void _Process (double delta) {
            checkingTime = true;
            foreach (KeyValuePair<Timer, TimerInfo> timer in _timerInfoByTimer) {
                if (timer.Key.IsComplete(GetTimeType(timer.Key))) {
                    if (timer.Value.Loop) {
                        timer.Key.Restart(GetTimeType(timer.Key));
                    } else {
                        timer.Key.Stop();
                    }

                    timer.Value.OnFinish();
                }
            }
            checkingTime = false;
            AddQueuedTimers();
            RemoveQueuedTimers();
        }
    }
}
