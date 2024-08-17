using Godot;

namespace Framework {

    /// <summary>
    /// Represents visual information.
    /// </summary>
    public abstract partial class Visual : Node {

        /// <summary>
        /// State the visual is associated with.
        /// </summary>
        protected State state;

        /// <summary>
        /// State the visual is associated with.
        /// </summary>
        public abstract State State { get; }

        /// <summary>
        /// Initialize the visual with a state.
        /// </summary>
        /// <param name="state">State the visual is associated with.</param>
        public virtual void Initialize (State state) {
            this.state = state;

            SetName();
        }

        /// <summary>
        /// Set the name of the visual.
        /// </summary>
        protected abstract void SetName ();
    }
}