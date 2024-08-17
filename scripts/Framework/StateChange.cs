using Framework.Enumerations;

namespace Framework {

    /// <summary>
    /// Enum for state changes.
    /// </summary>
    public abstract class StateChange : Enumeration {

        /// <inheritdoc />
        protected StateChange (int value, string name) : base(value, name) { }

    }
}