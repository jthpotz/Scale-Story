namespace Framework {

    /// <summary>
    /// Load data.
    /// </summary>
    /// <typeparam name="T">Type of data to load.</typeparam>
    public abstract class DataLoader<T> where T : Data {

        /// <summary>
        /// Check ensure needed data is present.
        /// </summary>
        /// <returns>True if needed data is present; false otherwise.</returns>
        public abstract bool Guards ();

        /// <summary>
        /// Load data.
        /// </summary>
        /// <returns>Loaded data.</returns>
        public abstract T LoadData ();

    }
}