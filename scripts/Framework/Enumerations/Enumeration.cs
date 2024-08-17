namespace Framework.Enumerations {

    /// <summary>
    /// Class based enumeration.
    /// </summary>
    public abstract class Enumeration {

        /// <summary>
        /// Name of the enum.
        /// </summary>
        private readonly string name;

        /// <summary>
        /// Name of the enum.
        /// </summary>
        public string Name {
            get {
                return name;
            }
        }

        /// <summary>
        /// Value of the enum.
        /// </summary>
        private readonly int value;

        /// <summary>
        /// Value of the enum.
        /// </summary>
        public int Value {
            get {
                return value;
            }
        }

        /// <summary>
        /// Create a new instance of the enum.
        /// </summary>
        /// <param name="value">Value for the enum.</param>
        /// <param name="name">Name of the enum.</param>
        protected Enumeration (int value, string name) {
            this.value = value;
            this.name = name;
        }
    }
}
