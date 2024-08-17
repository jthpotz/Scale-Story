using System.Collections.Generic;
using Attributes;

namespace Framework {

    public abstract class State {

        /// <summary>
        /// Delegate for state changes.
        /// </summary>
        /// <param name="state">State that is changing.</param>
        public delegate void StateChangedAction (State state);

        /// <summary>
        /// Data associated with the state.
        /// </summary>
        protected readonly Data data;

        /// <summary>
        /// Data associated with the state.
        /// </summary>
        public abstract Data Data { get; }

        /// <summary>
        /// Visual associated with the state.
        /// </summary>
        protected readonly Visual visual;

        /// <summary>
        /// Visual associated with the state.
        /// </summary>
        public abstract Visual Visual { get; }

        /// <summary>
        /// ID for the state.
        /// </summary>
        protected readonly int id;

        /// <summary>
        /// ID for the state.
        /// </summary>
        public int ID {
            get {
                return id;
            }
        }

        /// <summary>
        /// Map of state changes to appropriate delegate.
        /// </summary>
        protected readonly Dictionary<StateChange, StateChangedAction> stateChanges = new();

        /// <summary>
        /// List of stat changes on the state.
        /// </summary>
        protected readonly List<AttributeData> statChanges = new();

        /// <summary>
        /// Create a new State.
        /// </summary>
        /// <param name="data">Data for the state.</param>
        /// <param name="visual">Visual for the state.</param>
        /// <param name="id">ID of the state.</param>
        public State (Data data, Visual visual, int id) {
            this.data = data;
            this.visual = visual;
            this.id = id;
        }

        /// <summary>
        /// Add a stat change to the state.
        /// </summary>
        /// <param name="statChange">Stat change to add.</param>
        public void AddStatChange (AttributeData statChange) {
            statChanges.Add(statChange);

            StatChangesChanged();
        }

        /// <summary>
        /// Remove a stat change from the state.
        /// </summary>
        /// <param name="statChange">Stat change to remove.</param>
        public void RemoveStatChange (AttributeData statChange) {
            statChanges.Remove(statChange);

            StatChangesChanged();
        }

        /// <summary>
        /// Update data after stat changes are changed.
        /// </summary>
        public void StatChangesChanged () {
            data.CombineData(statChanges);
        }

        /// <summary>
        /// Register an action for a state change.
        /// </summary>
        /// <param name="stateChange">State change to register an action for.</param>
        /// <param name="action">Action to register.</param>
        public void RegisterAction (StateChange stateChange, StateChangedAction action) {
            if (!stateChanges.ContainsKey(stateChange)) stateChanges[stateChange] = null;
            stateChanges[stateChange] += action;
        }

        /// <summary>
        /// Unregister an action for a state change.
        /// </summary>
        /// <param name="state">State change to unregister the action for.</param>
        /// <param name="action">Action to unregister.</param>
        public void UnRegisterAction (StateChange state, StateChangedAction action) {
            if (stateChanges.ContainsKey(state)) stateChanges[state] -= action;
        }

        /// <summary>
        /// Invoke an action for a state change.
        /// </summary>
        /// <param name="stateChange">State change to invoke.</param>
        protected void InvokeAction (StateChange stateChange) {
            if (stateChanges.TryGetValue(stateChange, out StateChangedAction value)) value?.Invoke(this);
        }
    }
}
