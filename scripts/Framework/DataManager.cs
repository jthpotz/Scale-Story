using System;
using Godot;


namespace FullTesting {
    public partial class DataManager : Node {

        private static event Action<DataManager> InitializeFinished;

        public override void _Ready () {
            InitializeFinished(this);
        }

        public static void RegisterInitializedFinishedAction (Action<DataManager> action) {
            InitializeFinished += action;
        }

        public static void UnRegisterInitializedFinishedAction (Action<DataManager> action) {
            InitializeFinished -= action;
        }
    }
}
